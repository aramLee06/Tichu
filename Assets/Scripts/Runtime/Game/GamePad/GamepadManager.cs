using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public delegate void GamepadTimeout();
public delegate void MajongWishReceived(int value);

public class GamepadManager : Manager
{
	public static event GamepadTimeout timeoutEvent = delegate { };
	public static event MajongWishReceived majongReceivedEvent = delegate { };

	public int wishValue;

	[Header("General")]
	new public static GamepadManager main;
	public GamePadUIHandler ui;
	public bool ready = false;
	public int userIndex = 0;
	public GameObject cardPrefab;
	public Transform handArea, comboArea;

	public List<long> pings = new List<long>();

	public GamepadScoreboard scoreboard;
	public Image readyIndicator;

	public bool emptyConfirmed;

	public string nextScene;

	public Transform[] tradeArea = new Transform[3];
	public CardObject[] tradeSlot = new CardObject[3];
	public int[] targetPlayer = new int[3];

	public List<CardObject> playSlot = new List<CardObject>();

	public GameState currentState;

	public List<Card> hand = new List<Card>();

	public bool isMyTurn = false;

	public int lastCardAmount;

	public PlayKind playKind;

	public float lowestValue, highestValue;

	private bool screenReady = false;

	public override void Awake()
	{
		base.Awake();
		if (!main)
			main = this;
	}

	private void Start()
	{
		ui = (GamePadUIHandler) GamePadUIHandler.main;
		Debug.Log (playerNumber);

		GetUserIndex (playerNumber);

		CreateIDDeck();

		scoreboard.SetPlayerNumber(playerNumber);

		StartCoroutine(Ping());
	}

	private IEnumerator Ping()
	{
		yield return new WaitForSeconds(1f);
		if (screenReady)
		{
			long ts = (System.DateTime.Now - new System.DateTime(2016, 4, 28, 10, 30, 2)).Ticks;
			pings.Add(ts);
			string message = "!" + playerNumber.ToString() + ts.ToString();
			NetworkEmulator.main.SendDataToHost(message);

			if (pings.Count < 5)
			{
				foreach (long l in pings)
				{
					NetworkEmulator.main.SendDataToHost("!" + playerNumber.ToString() + l.ToString());
				}
				StartCoroutine(Ping());
			}
			else
				timeoutEvent();
		}
		else
		{
			NetworkEmulator.main.SendDataToHost("!"+playerNumber.ToString() + "0");
			StartCoroutine(Ping());
		}
	}

	public override void GetUserIndex (int index)
	{
		switch (index)
		{
			case 0:
				targetPlayer [0] = 1;
				targetPlayer [1] = 2;
				targetPlayer [2] = 3;
				break;
			case 1:
				targetPlayer [0] = 0;
				targetPlayer [1] = 3;
				targetPlayer [2] = 2;
				break;
			case 2:
				targetPlayer [0] = 1;
				targetPlayer [1] = 0;
				targetPlayer [2] = 3;
				break;
			case 3:
				targetPlayer [0] = 0;
				targetPlayer [1] = 1;
				targetPlayer [2] = 2;
				break;
			default:
				targetPlayer [0] = 0;
				targetPlayer [1] = 0;
				targetPlayer [2] = 0;
				break;
		}
	}

	private void Update()
	{
		switch(currentState)
		{
			case GameState.TRADING:
				ui.playButton.interactable = tradingSlotsFilled ();
				//Debug.Log (tradingSlotsFilled());
				break;
			case GameState.SCOREBOARD:
				ui.playButton.interactable = true;
				break;
			case GameState.ROUND_START:
				ui.playButton.interactable = true;
				break;
			case GameState.TRICK:
				if(hand.Count == 0 && !emptyConfirmed)
					NetworkEmulator.main.SendDataToHost("E" + playerNumber);
				break;
		}

		ui.passButton.interactable = /*is (true) when*/ (currentState == GameState.TRICK && lastPlayKind != PlayKind.NONE && isMyTurn);
		if (!isMyTurn && currentState == GameState.TRICK || currentState == GameState.DRAGON_CARD)
		{
			ui.DisplayNotYourTurn (true);
		}
		else
		{
			ui.DisplayNotYourTurn (false);
		}

		if (currentState == GameState.TRICK || currentState == GameState.DRAGON_CARD)
		{
			foreach(Button butt in ui.buttonsDisabledOnNotTurn)
			{
				butt.interactable = isMyTurn;
				ui.tichuButton.interactable = false;
			}

			ui.playButton.interactable = ValidateCardCombo();
			if(ui.playButton.interactable == false && ready)
			{
				ready = false;
				NetworkEmulator.main.SendDataToHost("R" + playerNumber + "-");
				ui.canCall = false;
			}

			if(isMyTurn && hand.Count == 0 && playSlot.Count == 0)
			{
				NetworkEmulator.main.SendDataToHost("E" + playerNumber);
				ready = true;
				NetworkEmulator.main.SendDataToHost("R" + playerNumber + "+");
				NetworkEmulator.main.SendDataToHost("P" + playerNumber + "+");
				ui.canCall = false;
			}
		}

		readyIndicator.gameObject.SetActive(ready);
	}

	public bool tradingSlotsFilled()
	{
		for(int i = 0; i < tradeSlot.Length; i++)
		{
			if (tradeSlot[i] == null)
				return false;
		}
		return true;
	}

	public bool tradingSlotContains ()
	{
		for(int i = 0; i < tradeSlot.Length; i++)
		{
			if (tradeSlot[i] != null)
				return true;
		}
		return false;
	}

	public void PassButton()
	{
		if (isMyTurn)
		{
			ready = true;
			isMyTurn = false;

			/*List<CardObject> tempSlot = new List<CardObject>();
			foreach (CardObject cardObj in playSlot)
			{
				tempSlot.Add(cardObj);
			}
			foreach (CardObject cardObj in tempSlot)
			{
				MoveCard(cardObj, -1);
			}*/

			NetworkEmulator.main.SendDataToHost("R" + playerNumber + "p");
			NetworkEmulator.main.SendDataToHost("P" + playerNumber + "+");
		}
	}

	public void DragonSelectPlayer(int targetPlayer) //Added as of 2016.03.30
	{
		NetworkEmulator.main.SendDataToHost("D" + playerNumber.ToString() + targetPlayer.ToString());
	}

	public PlayKind possiblePlayKind;

	public List<Card> orderedCards;

	public bool ValidateCardCombo()
	{
		List<Card> comboCards = new List<Card>();
		foreach(CardObject cObj in playSlot)
		{
			comboCards.Add(cObj.card);
		}
		possiblePlayKind = playKind;
		return CombinationHandler.CheckCombination(comboCards, ref possiblePlayKind, lastValue, lastPlayKind, out orderedCards,lastCardAmount,hand,highestValue,lowestValue); //Adjusted as of 2016.03.21
	}

	public void SetReady()
	{
		if (currentState == GameState.TRICK && isMyTurn)
		{
			playKind = possiblePlayKind; 

			ready = true;
			isMyTurn = false;

			List<Card> playedCards = new List<Card>();

			string msg = "c" + playerNumber.ToString() + ((int)playKind).ToString();

			for(int i = 0; i < orderedCards.Count; i++)
				msg += (char)(orderedCards[i].id); //Added as of 2016.03.21

			foreach (CardObject cObj in playSlot)//Adjusted as of 2016.03.21
			{
				playedCards.Add(cObj);
				Destroy(cObj.gameObject);
				hand.Remove(cObj.card);
			}

			CombinationHandler.PlayCards(playedCards);
			NetworkEmulator.main.SendDataToHost(msg);
			playSlot.Clear();
			NetworkEmulator.main.SendDataToHost("R" + playerNumber + "+");
			NetworkEmulator.main.SendDataToHost("P" + playerNumber + "c");

			if (hand.Count == 0)
			{
				emptyConfirmed = false;
				//Debug.Log("<color=#0f0>Hand Empty Message from "+playerNumber+"</color>");
				NetworkEmulator.main.SendDataToHost("E" + playerNumber);
			}
		}
		else
		{
			ready = !ready;
			NetworkEmulator.main.SendDataToHost("R" + playerNumber + "t");
		}
	}

	public void CallTichu()
	{
		NetworkEmulator.main.SendDataToHost("T" + playerNumber);
	}

	public void AddCard(int cardId)
	{
		hand.Add(deck[cardId]);
	}

	public void MoveCardToTradeSlot(SlotID slot)
	{
		if (selectedCardObject && tradeSlot[(int) slot] == null)
		{
			MoveCard(selectedCardObject,(int) slot);
			tradeSlot[(int) slot] = selectedCardObject;
		}
	}

	public void MoveCardToCombo()
	{
		if(selectedCardObject)
		{
			MoveCard(selectedCardObject, -2);
			playSlot.Add(selectedCardObject);
		}
	}

	public void MoveCardToHand()
	{
		if(selectedCardObject)
		{
			MoveCard(selectedCardObject,-1);
		}
	}

	private void MoveCard(CardObject cardObj,int newSlot)
	{
		if ((int) cardObj.slotId > -1)
		{
			tradeSlot[(int) cardObj.slotId] = null;
			if (currentState == GameState.TRADING)
			{
				ready = false;
				NetworkEmulator.main.SendDataToHost("R" + playerNumber + "-");
			}
		}
		else if((int) cardObj.slotId == -2)
		{
			ready = false;
			NetworkEmulator.main.SendDataToHost("R" + playerNumber + "-");

			playSlot.Remove(cardObj);
		}

		cardObj.slotId = (SlotID) newSlot;
	}

	public void SetState(GameState state)
	{
		//Debug.Log ("GamepadManager ~ SetState");

		currentState = state;
		ui.StateSwitch (currentState);
		switch(state)
		{
			case GameState.ROUND_START:
				scoreboard.SetActive(false);
				ui.tichuButton.interactable = true;
				break;
			case GameState.TRICK:
				ui.tichuButton.interactable = false;
				break;
			case GameState.GAME_RESULT:
				SceneManager.LoadScene(nextScene);
				break;
			case GameState.SCOREBOARD:
				scoreboard.SetActive(true);
				break;
		}
	}

	public void PerformTrade()
	{
		string message = "t" + playerNumber;

		//Debug.Log ("Performing Trades.");
		//Debug.LogWarning ("I have " + tradeSlot.Length + " slots for trading.");

		for(int i = 0; i < tradeSlot.Length; i++)
		{
			message += targetPlayer[i].ToString() + (char)(tradeSlot[i].card.id);
			//Debug.LogWarning ("Tradeslot "+i+" has " + tradeSlot[i].card.name + " ~ " + tradeSlot[i].gameObject.name);

			//Debug.Log ("Tradeslot: " + i);
			hand.Remove(tradeSlot[i].card);
			Destroy(tradeSlot[i].gameObject);
			tradeSlot[i] = null;
		}

		NetworkEmulator.main.SendDataToHost(message);
	}

	private void RemoveAllCards()
	{
		while(hand.Count > 0)
		{
			Destroy(hand[0].cardObject.gameObject);
			hand.RemoveAt(0);
		}
	}

	public override void ReceiveData(string data)
	{
		char[] bytes = data.ToCharArray();
		//int playerId = bytes[1];
		switch (bytes [0])
		{
			case 'C': //Card operation
				int cardId = ((int)bytes [3]);
				if (bytes [2] == '+') //Card Add
				{
					//Debug.Log ("CARDID TO ADD: " + cardId);
					AddCard (cardId);
				}
				else if (bytes [2] == 'c') //Cards reset
					RemoveAllCards ();
				break;
			case 'R': //Ready operation
				ready = false;
				break;
			case 'S': //State operation
				SetState ((GameState)int.Parse (bytes [2].ToString ()));
				break;
			case 's': //Scoreboard operation
				byte type = byte.Parse (bytes [2].ToString ());

				Debug.Log ("<color=#3a7>Received scoreboard message (" + data + ")</color>");

				Debug.Log ("<color=#3a7>Type = " + type + " (from " + bytes [2] + ")</color>");

				scoreboard.SetActive (true);
				switch (type)
				{
					case 0: //Placement
						int placement = byte.Parse (bytes [3].ToString ());
						Debug.Log ("<color=#3a7>Setting placement (" + bytes [3] + " --> " + placement + ")</color>");
						scoreboard.Placement = placement;
						break;
					case 1: //Round score
						int score = int.Parse (data.Substring (3));
						Debug.Log ("<color=#3a7>Setting round score</color>");
						scoreboard.RoundScore = score;
						break;
					case 2: //Total score
						int totalScore = int.Parse (data.Substring (3));
						Debug.Log ("<color=#3a7>Setting total score</color>");
						scoreboard.TotalScore = totalScore;
						break;
				}
				break;
			case 't': //Trade operation
				PerformTrade ();
				break;
			case 'r': //Turn operation
				isMyTurn = int.Parse (bytes [2].ToString ()) == playerNumber;
				lastPlayKind = (PlayKind)(int.Parse (bytes [3].ToString ()));
				lastValue = (int)bytes [4];
				lastCardAmount = (int)bytes [5];
				lowestValue = (int)bytes [6];
				highestValue = (int)bytes [7];
				break;
			case 'v': //Vibrate
				Handheld.Vibrate ();
				break;
			case 'W': //Value Wish operation
				if ((int)bytes[2] != 255)
					CombinationHandler.wishValue = (int)bytes[2];
				else
					CombinationHandler.wishValue = -1;
				wishValue = (int)bytes[2];
				majongReceivedEvent (((int)bytes [2]) == 255 ? -1 : wishValue);
				break;
			case 'O': //Options operation (Added as of 2016.03.22)
				{
					int musicVolume = (int)bytes [2];
					int soundVolume = (int)bytes [3];
					int gameLanguage = (int)bytes [4];

					GameOptions.musicVolume = ((float)musicVolume) / 255;
					GameOptions.soundVolume = ((float)soundVolume) / 255;
					GameOptions.language = (GameLanguage)gameLanguage;
					OptionsMenuHandler.main.UpdateOptionElements ();
				}
				break;
			case 'B': //Break/Pause operation (Added as of 2016.03.22)
				{
					if (bytes [2] == '+')
						GameOptions.paused = true;
					else if (bytes [2] == '-')
						GameOptions.paused = false;
				}
				break;
			case 'D': //Dragon operation //Added as of 2016.03.30
				ui.Dragon ();
				break;
			case 'M': //Mahjong operation //Added as of 2016.03.30
				ui.Mahjong ();
				break;
			case 'E': //Empty Confirmation
				emptyConfirmed = true;
				break;
			case '!': //Pong operation
				{
					long messageCode = long.Parse (data.Substring (2));
					if (messageCode == 0)
						screenReady = true;
					else
						pings.Remove (messageCode);
				}
				break;
			case '*': //Restart Game
				SceneManager.LoadScene (0);
				break;
		}

		ui.GetHand ();
	}
}