using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

/// <summary>
/// Event delegate for when the connection is broken
/// </summary>
public delegate void GamepadTimeout();
/// <summary>
/// Event delegate for when a mahjong has been played and a wish value has been set
/// </summary>
/// <param name="value">The set wish value</param>
public delegate void MajongWishReceived(int value);

/// <summary>
/// The Gamepad's manager
/// </summary>
public class GamepadManager : Manager
{
	/// <summary>
	/// Occurs when thereś a timeout.
	/// </summary>
	public static event GamepadTimeout timeoutEvent = delegate { };
	/// <summary>
	/// Occurs when the majong wishvalue received.
	/// </summary>
	public static event MajongWishReceived majongReceivedEvent = delegate { };

	/// <summary>
	/// The wish value.
	/// </summary>
	public int wishValue;

	[Header("General")]
	/// <summary>
	/// Singleton
	/// </summary>
	new public static GamepadManager main;
	/// <summary>
	/// The user interface.
	/// </summary>
	public GamePadUIHandler ui;
	/// <summary>
	/// Is the player ready?
	/// </summary>
	public bool ready = false;
	/// <summary>
	/// The index of the user.
	/// </summary>
	public int userIndex = 0;
	/// <summary>
	/// The card prefab.
	/// </summary>
	public GameObject cardPrefab;
	/// <summary>
	/// Various areas
	/// </summary>
	public Transform handArea, comboArea;

	/// <summary>
	/// Ping list for timeout
	/// </summary>
	public List<long> pings = new List<long>();

	/// <summary>
	/// The scoreboard.
	/// </summary>
	public GamepadScoreboard scoreboard;
	/// <summary>
	/// The ready indicator.
	/// </summary>
	public Image readyIndicator;

	/// <summary>
	/// If it got confirmation from the screen itś empty
	/// </summary>
	public bool emptyConfirmed;

	/// <summary>
	/// The next scene's name.
	/// </summary>
	public string nextScene;

	/// <summary>
	/// The trade areas.
	/// </summary>
	public Transform[] tradeArea = new Transform[3];
	/// <summary>
	/// The trade slots.
	/// </summary>
	public CardObject[] tradeSlot = new CardObject[3];
	/// <summary>
	/// The target players.
	/// </summary>
	public int[] targetPlayer = new int[3];

	/// <summary>
	/// The play slots.
	/// </summary>
	public List<CardObject> playSlot = new List<CardObject>();

	/// <summary>
	/// The current state.
	/// </summary>
	public GameState currentState;

	/// <summary>
	/// The hand.
	/// </summary>
	public List<Card> hand = new List<Card>();

	/// <summary>
	/// Is it my turn?
	/// </summary>
	public bool isMyTurn = false;

	/// <summary>
	/// The last amount of cards.
	/// </summary>
	public int lastCardAmount;

	/// <summary>
	/// The kind of the play.
	/// </summary>
	public PlayKind playKind;

	/// <summary>
	/// The lowest and highest values.
	/// </summary>
	public float lowestValue, highestValue;

	/// <summary>
	/// Is the screen ready to play?
	/// </summary>
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

    /// <summary>
    /// Sends a ping message to the Screen's GameManager to check whether there's still a connection
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Sets up the used indexes for the trade.
    /// </summary>
    /// <param name="index">Own player's index</param>
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

    /// <summary>
    /// Checks whether all trading slots have been filled.
    /// </summary>
    /// <returns>Are all slots filled?</returns>
	public bool tradingSlotsFilled()
	{
		for(int i = 0; i < tradeSlot.Length; i++)
		{
			if (tradeSlot[i] == null)
				return false;
		}
		return true;
	}

    /// <summary>
    /// Checks if the trading slots actually contain something
    /// </summary>
    /// <returns></returns>
	public bool tradingSlotContains ()
	{
		for(int i = 0; i < tradeSlot.Length; i++)
		{
			if (tradeSlot[i] != null)
				return true;
		}
		return false;
	}

    /// <summary>
    /// Skips the player's turn
    /// </summary>
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

    /// <summary>
    /// Selects the target player for the dragon event
    /// </summary>
    /// <param name="targetPlayer">Selected player</param>
	public void DragonSelectPlayer(int targetPlayer) //Added as of 2016.03.30
	{
		NetworkEmulator.main.SendDataToHost("D" + playerNumber.ToString() + targetPlayer.ToString());
	}

	/// <summary>
	/// The kinds of the possible plays.
	/// </summary>
	public PlayKind possiblePlayKind;

	/// <summary>
	/// The ordered cards.
	/// </summary>
	public List<Card> orderedCards;

    /// <summary>
    /// Validates whether the selected card combo is valid-to-play
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Sets player as ready.
    /// </summary>
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

    /// <summary>
    /// Calls Tichu
    /// </summary>
	public void CallTichu()
	{
		NetworkEmulator.main.SendDataToHost("T" + playerNumber);
	}

    /// <summary>
    /// Adds a card to the hand
    /// </summary>
    /// <param name="cardId">ID of the card to add</param>
	public void AddCard(int cardId)
	{
		hand.Add(deck[cardId]);
	}

    [System.Obsolete("Unused")]
	public void MoveCardToTradeSlot(SlotID slot)
	{
		if (selectedCardObject && tradeSlot[(int) slot] == null)
		{
			MoveCard(selectedCardObject,(int) slot);
			tradeSlot[(int) slot] = selectedCardObject;
		}
	}

    [System.Obsolete("Unused")]
    public void MoveCardToCombo()
	{
		if(selectedCardObject)
		{
			MoveCard(selectedCardObject, -2);
			playSlot.Add(selectedCardObject);
		}
	}

    [System.Obsolete("Unused")]
    public void MoveCardToHand()
	{
		if(selectedCardObject)
		{
			MoveCard(selectedCardObject,-1);
		}
	}

    /// <summary>
    /// Moves the selected card object to the specified slot (-2 being the play slot, -1 the hand and >0 the trading slots)
    /// </summary>
    /// <param name="cardObj">Specified card object to move</param>
    /// <param name="newSlot">The slot to move the card to (-2 = play, -1 = hand, 0,1,2 = trade slots)</param>
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

    /// <summary>
    /// Changes the game's current state
    /// </summary>
    /// <param name="state">The new state</param>
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

    /// <summary>
    /// Executes the trade phase.
    /// </summary>
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

    /// <summary>
    /// Removes all cards from hand
    /// </summary>
	private void RemoveAllCards()
	{
		while(hand.Count > 0)
		{
			Destroy(hand[0].cardObject.gameObject);
			hand.RemoveAt(0);
		}
	}

    /// <summary>
    /// Handles received data
    /// </summary>
    /// <param name="data">Received data string</param>
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