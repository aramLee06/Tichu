using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// Event delegate for when a player's turn changes
/// </summary>
/// <param name="currentPlayer">Current active player</param>
public delegate void TurnProgressHandler(int currentPlayer);

public class GameManager : Manager
{
	/// <summary>
	/// Turn progression event
	/// </summary>
	public static event TurnProgressHandler turnProgressEvent = delegate { };

	[Header("General")]
	/// <summary>
	/// Singleton
	/// </summary>
	new public static GameManager main;
	/// <summary>
	/// The user interface.
	/// </summary>
	public ScreenUIHandler ui;
	/// <summary>
	/// Current state
	/// </summary>
	public GameState _currentState;

	/// <summary>
	/// Various timers
	/// </summary>
	public Timer gameTimer, turnTimer;

	/// <summary>
	/// The info handler.
	/// </summary>
	public InfoHandler infoHandler;

	/// <summary>
	/// All cards.
	/// </summary>
	public static List<GameObject> allCards = new List<GameObject>();

	/// <summary>
	/// The human players.
	/// </summary>
	public PlayerHandler[] humanPlayers;
	/// <summary>
	/// The computer players.
	/// </summary>
	public ComputerPlayer[] computerPlayers;

	/// <summary>
	/// The scoreboard.
	/// </summary>
	public GameObject scoreboard;
	/// <summary>
	/// The game end board.
	/// </summary>
	public GameObject gameEndBoard;
	//public TurnIndicator turnIndicator;

	/// <summary>
	/// The out-of-cards FX.
	/// </summary>
	public GameObject[] outOfCardsFX;
	/// <summary>
	/// The fx positions.
	/// </summary>
	public Transform[] fxPosition;

	private bool undoneReadyOnStart = false;

	/// <summary>
	/// The individual scores.
	/// </summary>
	private int[] individualScores = new int[4];
	/// <summary>
	/// The individual total scores.
	/// </summary>
	private int[] individualScoresTotal = new int[4];
	/// <summary>
	/// The out order.
	/// </summary>
	public List<int> outOrder = new List<int>();

	/// <summary>
	/// The duration of the confetti effect.
	/// </summary>
	public float confettiEffectDuration = 6;

	/// <summary>
	/// Current state variable. The set manages some things for when the state changes.
	/// </summary>
	public GameState currentState
	{
		get
		{
			return _currentState;
		}
		set
		{
			bool trade = false;
			if (_currentState == GameState.TRADING)
				trade = true;

			if (value == GameState.ROUND_START)
			{
				passes = 0;
				SetPlayersUnready();

				NetworkEmulator.main.SendDataToHost("J0X");

				while (trickStash.Count > 0)
				{
					if (trickStash[0].cardObject != null)
						Destroy(trickStash[0].cardObject);
					trickStash.RemoveAt(0);
				}

				foreach(PlayerHandler player in players)
				{
					while(player.hand.Count > 0)
					{
						Destroy(player.hand[0].cardObject);
						player.hand.RemoveAt(0);
					}
				}

				while(lastPlay.Count > 0)
				{
					if (lastPlay[0] != null)
						Destroy(lastPlay[0]);
					lastPlay.RemoveAt(0);
				}

				while(allCards.Count > 0)
				{
					if(allCards[0] != null)
					{
						Destroy(allCards[0]);
					}
					allCards.RemoveAt(0);
				}

				scoreboard.SetActive(false);
				outOrder.Clear();
				for (int i = 0; i < individualScores.Length; i++)
					individualScores[i] = 0;
			}
			else if (value == GameState.SCOREBOARD)
			{
				scoreboard.SetActive(true);
				////Debug.Log("<color=#3a7>Scoreboard activate</color>");
				int[] placement = scoreboard.GetComponent<Scoreboard>().UpdateScoreboard(individualScores, outOrder.ToArray());
				for (int i = 0; i < 4; i++)
				{
					////Debug.Log("<color=#aa0>Telling player " + i + " that he is on place no. " + (placement[i] + 1) + "</color>");
					NetworkEmulator.main.SendDataTo(i, "s90" + (placement[i]));
					NetworkEmulator.main.SendDataTo(i, "s91" + individualScores[i]);
					NetworkEmulator.main.SendDataTo(i, "s92" + individualScoresTotal[i]);
				}
			}
			else if (value == GameState.ROUND_RESULT)
			{
				turnTimer.Stop();
				turnTimer.Reset();
			}

			if (value == GameState.TRICK)
				gameTimer.Play();
			else
				gameTimer.Stop();

			_currentState = value;
			////Debug.Log("Next state: " + value);
			NetworkEmulator.main.SendData("S9" + (int)_currentState);

			if (trade)
			{
				NetworkEmulator.main.SendData("t9");
				foreach (PlayerHandler player in players)
				{
					if (!player.isHumanPlayer)
					{
						((ComputerPlayer)player).PerformTrade();
					}
				}
			}

			if (_currentState == GameState.TRICK)
			{
				if (lastPlay.Count == 0 || lastPlay[0].value != 0)
				{
					NetworkEmulator.main.SendDataToHost("J9X"); // Reset Mahjong
					StartCoroutine(WaitForPlayersTradeConfirm());
				}
			}

			if (_currentState == GameState.GAME_RESULT)
			{
				gameEndBoard.SetActive(true);
				gameEndBoard.GetComponent<GameEndBoard>().UpdateScoreboard(winTeam == 1 ? 0 : 1);
			}

			infoHandler.ChangeState (_currentState);
		}
	}

	/// <summary>
	/// The next scene.
	/// </summary>
	public string nextScene;
	/// <summary>
	/// The players.
	/// </summary>
	public PlayerHandler[] players;
	/// <summary>
	/// The win score.
	/// </summary>
	public int winScore = 1000;
	/// <summary>
	/// The win team.
	/// </summary>
	public int winTeam = 0;

	[Header("Trick")]
	/// <summary>
	/// The current active player.
	/// </summary>
	public int currentActivePlayer = 0;
	/// <summary>
	/// The leader identifier.
	/// </summary>
	public int leaderId = 0;
	/// <summary>
	/// The current turn.
	/// </summary>
	public int _currentTurn = 0;
	public int currentTurn
	{
		get
		{
			return _currentTurn;
		}
		set
		{
			////Debug.Log("Got set to " + value + " @ " + Time.time + " (rt: " + Time.realtimeSinceStartup + " )");
			_currentTurn = value;
			turnTimer.Reset();
			if (value != 0)
				turnTimer.Play();
			else
				turnTimer.Stop();
		}
	}
	/// <summary>
	/// The current trick.
	/// </summary>
	public int currentTrick = 0;
	/// <summary>
	/// The players out.
	/// </summary>
	public bool[] playersOut;
	/// <summary>
	/// The number of passes.
	/// </summary>
	public int passes = 0;

	/// <summary>
	/// The last player.
	/// </summary>
	public int _lastPlayer = 0;
	/// <summary>
	/// The trick gain player.
	/// </summary>
	public int trickGainPlayer = 0;
	public int lastPlayer
	{
		get
		{
			return _lastPlayer;
		}
		set
		{
			_lastPlayer = value;
			trickGainPlayer = value;
		}
	}

	/// <summary>
	/// The card object prefab.
	/// </summary>
	public GameObject cardPrefab;
	/// <summary>
	/// The card parent.
	/// </summary>
	public Transform cardParent;
	//public List<CardObject> lastPlay = new List<CardObject>();
	/// <summary>
	/// The trick stash.
	/// </summary>
	public List<Card> trickStash = new List<Card>();

	/// <summary>
	/// The trick card destroy delay.
	/// </summary>
	public float trickCardDestroyDelay = .5f;

	[Header("Transition")]
	/// <summary>
	/// The transition end time.
	/// </summary>
	public float transitionEndTime;
	/// <summary>
	/// The next state after the transition
	/// </summary>
	public GameState transitionNextState;

	/// <summary>
	/// The identifier deck.
	/// </summary>
	public List<Card> idDeck = new List<Card>();

	public override void Awake()
	{
		base.Awake ();
		//Create singleton
		if (!main)
			main = this;
	}

	public void RestartGame()
	{
		NetworkEmulator.main.SendData("*9"); //Restart the game, gamepads!
		SceneManager.LoadScene(0);
	}

	/// <summary>
	/// Checks whether a player is human or computer controlled
	/// </summary>
	/// <param name="id">The player's ID</param>
	/// <returns></returns>
	public override bool IsHuman(int id)
	{
		return players[id].isHumanPlayer;
	}

	public override int GetWinPlayer() //Added as of 2016.03.21
	{
		return lastPlayer;
	}

	/// <summary>
	/// Handles the dragon card event
	/// </summary>
	public override void HandleDragon() //Added as of 2016.03.30
	{
		currentState = GameState.DRAGON_CARD;
		NetworkEmulator.main.SendDataTo(lastPlayer, "D9");

		if (!players[lastPlayer].isHumanPlayer)
		{
			int chosenPlayer = lastPlayer+1;
			if (Random.value >= .5f)
				chosenPlayer += 2;

			chosenPlayer %= 4;
			NetworkEmulator.main.SendDataToHost("D" + lastPlayer.ToString() + chosenPlayer.ToString());
		}
	}

	/// <summary>
	/// Handles the Mahjong card event
	/// </summary>
	public override void HandleMahjong() //Added as of 2016.03.30
	{
		currentState = GameState.DRAGON_CARD;
		NetworkEmulator.main.SendDataTo(lastPlayer, "M9");

		if (!players[lastPlayer].isHumanPlayer)
		{
			int chosenValue = (int)Mathf.Round(Random.value * 12+1);
			NetworkEmulator.main.SendDataToHost("J" + lastPlayer.ToString() + (char)chosenValue);
		}
	}

	private void Start()
	{
		gameTimer.timerEndEvent += OnRoundTimerEnd;
		turnTimer.timerEndEvent += OnTurnTimerEnd;

		SetPlayers();
		ShuffleCards();
		CreateTeams();
		playersOut = new bool[players.Length];
		StartCoroutine(Debuge());


		ui = (ScreenUIHandler) ScreenUIHandler.main;
	}

	/// <summary>
	/// Handles the game timer's event
	/// </summary>
	/// <param name="args">Arguments</param>
	private void OnRoundTimerEnd(System.EventArgs args)
	{
		currentState = GameState.GAME_RESULT;
	}

	/// <summary>
	/// Handles the turn timer's event
	/// </summary>
	/// <param name="args">Arguments</param>
	private void OnTurnTimerEnd(System.EventArgs args)
	{
		////Debug.Log("Turn Timer ended");
		int playerId = (leaderId + currentTurn) % players.Length;
		NetworkEmulator.main.SendDataToHost("R" + playerId + "+");
		NetworkEmulator.main.SendDataToHost("P" + playerId + "+");
	}

	/// <summary>
	/// Sets the active players to either human players or computer players, according to the set up settings.
	/// </summary>
	private void SetPlayers()
	{
		players [0] = humanPlayers [0];

		for (int i = 1; i < 4; i++)
		{
			players [i] = computerPlayers [i];
		}

		for (int i = 0; i < 4; i++)
		{
			if (GameSettings.enableAI[i])
				players[i] = computerPlayers[i];
			else
				players[i] = humanPlayers[i];
		}
	}

	/// <summary>
	/// Misnamed function. Checks whether there are any clone cards and removes them.
	/// </summary>
	/// <returns></returns>
	private IEnumerator Debuge()
	{
		yield return new WaitForSeconds(1);
		string msg = "";
		for(int i = 0; i < 14; i++)
		{
			int amount = 0;
			foreach(PlayerHandler player in players)
			{
				List<Card> removeCards = new List<Card>();

				foreach(Card card in player.hand)
				{
					if (card.value == i)
						amount++;
					if(amount > 4)
					{
						//player.hand.Remove(card);
						removeCards.Add(card);
					}
				}

				foreach (Card card in removeCards)
					player.hand.Remove(card);
			}

			msg += i.ToString() + "=" + amount.ToString() + " ";
		}
		StartCoroutine(Debuge());
	}

	/// <summary>
	/// Sets up the players' teams
	/// </summary>
	private void CreateTeams()
	{
		for (int i = 0; i < players.Length; i++)
		{
			players[i].team = ((i % 2) == 0) ? Team.team1 : Team.team2;
		}
	}

	/// <summary>
	/// Creates the decks.
	/// </summary>
	private void CreateDecks()
	{
		base.CreateIDDeck();
		idDeck = new List<Card>(deck);
	}

	/// <summary>
	/// Shuffles the cards.
	/// </summary>
	/// <param name="attempt">Current attempt.</param>
	private void ShuffleCards(int attempt = 0)
	{
		//////Debug.Log("Shuffling Cards...");
		CreateDecks();

		//Randomize cards
		int unshuffledCards = deck.Count;
		while(unshuffledCards > 1)
		{
			unshuffledCards--;
			int randomChosenID = Random.Range(0, unshuffledCards);
			Card shuffledCard = deck[randomChosenID];
			deck[randomChosenID] = deck[unshuffledCards];
			deck[unshuffledCards] = shuffledCard;
		}

		bool failure = false;

		for (int i = 0; i < 14; i++)
		{
			int amount = 0;
			foreach (Card card in deck)
			{
				if (card.value == i)
					amount++;
				if(amount > 4)
					failure = true;

				if (failure) break;
			}
			if (failure) break;
		}

		if (failure && attempt < 16)
		{
			deck.Clear();
			ShuffleCards(attempt+1);
		}
	}

	/// <summary>
	/// Checks if any human is ready.
	/// </summary>
	/// <returns><c>true</c>, if any human is ready, <c>false</c> otherwise.</returns>
	private bool AnyHumanReady()
	{
		bool ready = false;
		bool containsHuman = false;
		foreach(PlayerHandler player in players)
		{
			if(player.isHumanPlayer)
			{
				containsHuman = true;
				if (player.isReady)
					ready = true;
			}
		}

		return ready || !containsHuman;
	}
	/// <summary>
	/// State Machine.
	/// </summary>
	private void Update()
	{
		//////Debug.Log(currentState);
		switch(currentState)
		{
			case GameState.ROUND_START:
				RoundStart(); //Players can call Great Grand Tichu, and afterwards receive 8 cards.
				break;
			case GameState.SCOREBOARD:
				if (AnyHumanReady())
					currentState = GameState.ROUND_START;
				break;
			case GameState.FIRST_DEAL:
				FirstDeal(); //Players can call Grand Tichu, and afterwards receive 6 cards.
				break;
			case GameState.SECOND_DEAL:
				currentState = GameState.TRADING; //Skip this state, it's redundant. Clean this up later.
				break;
			case GameState.TRADING:
				WaitForPlayersReady(GameState.TRICK); //The players have to pass along 1 card to each player (so 3 in total) and receive cards back.
				break;
			case GameState.TRANSITION:
				Transition();
				break;
			case GameState.TRICK:
				//////Debug.Log("Turn: " + (leaderId + currentTurn) % 4);
				if (playersOut[(leaderId + currentTurn) % 4])
					ProgressTurn();

				for(int i = 0; i < players.Length; i++)
				{
					if (players[i].hand.Count == 0)
						NetworkEmulator.main.SendDataToHost("E" + i);

					if((leaderId+currentTurn)%4 == i && !players[i].isHumanPlayer)
					{
						//////Debug.Log("Da " + i);
						((ComputerPlayer)players[i]).DoTrick();
					}
				}
				break;
		}
	}

	/// <summary>
	/// Starts the Trick
	/// </summary>
	private void TrickStart()
	{
		DetermineLead();
		currentTurn = 0;
		lastPlayKind = PlayKind.NONE;
		lastValue = 0;
		passes = 0;
		NetworkEmulator.main.SendData("r9" + leaderId.ToString() + ((int)PlayKind.NONE).ToString() + (char)0 + (char)0 + (char)0 + (char)0);

		//turnIndicator.IndicateActivePlayer ((leaderId + currentTurn) % players.Length);
		turnProgressEvent((leaderId + currentTurn) % players.Length);
	}

	/// <summary>
	/// Determines the lead player.
	/// </summary>
	private void DetermineLead()
	{
		if (currentTrick == 0)
		{
			bool foundLead = false;
			for (int i = 0; i < players.Length; i++)
			{
				foreach (Card card in players[i].hand)
				{
					if (card.GetType () == typeof(CardMahJong))
					{
						leaderId = i;
						foundLead = true;
						break;
					}
				}
				if (foundLead)
					break;
			}
		}
		else
		{
			leaderId = lastPlayer;
		}
	}

	/// <summary>
	/// Perform Transition
	/// </summary>
	[System.Obsolete("Unused")]
	private void Transition()
	{
		if (Time.time >= transitionEndTime)
			currentState = transitionNextState;
	}

	/// <summary>
	/// Starts the Round
	/// </summary>
	private void RoundStart()
	{
		if(!undoneReadyOnStart)
		{
			SetPlayersUnready();
			//////Debug.Log("Round start init done",Color.magenta);
			undoneReadyOnStart = true;
		}

		foreach(PlayerHandler player in players)
		{
			player.hasTraded = false;
		}

		//Players can decide to grand grand tichu or not.
		if (AllPlayersReady())
		{
			//////Debug.Log("All players ready", Color.magenta);
			DealCards(8);
			SetPlayersUnready();
			currentState = GameState.FIRST_DEAL;
			currentTrick = 0;
			undoneReadyOnStart = false;
		}
	}

	/// <summary>
	/// Deals the cards.
	/// </summary>
	/// <param name="amount">Amount of cards.</param>
	private void DealCards(int amount)
	{
		//////Debug.Log("Currently dealing cards (" + amount + ")",Color.cyan);
		for(int p = 0; p < players.Length; p++)
		{
			for(int i = 0; i < amount; i++)
			{
				Card topDeck = deck[0];
				deck.RemoveAt(0);
				players[p].hand.Add(topDeck);
				NetworkEmulator.main.SendDataTo(p, "C9+" + (char)topDeck.id);

				//If the selected card is the Mah Jong card, this player is the "leader".
				if (topDeck.GetType() == typeof(CardMahJong))
					leaderId = p;
			}
		}
	}

	/// <summary>
	/// Perform first deal round
	/// </summary>
	private void FirstDeal()
	{
		//Players can decide to call grand tichu or not.
		if(AllPlayersReady())
		{
			DealCards(6);
			SetPlayersUnready();
			currentState = GameState.SECOND_DEAL;
		}
	}

	/// <summary>
	/// Waits for players to be ready.
	/// </summary>
	/// <param name="nextState">Next state.</param>
	private void WaitForPlayersReady(GameState nextState)
	{
		if(AllPlayersReady())
		{
			SetPlayersUnready();

			currentState = nextState;
		}
	}

	/// <summary>
	/// Waits for players to confirm the trade.
	/// </summary>
	/// <returns></returns>
	private IEnumerator WaitForPlayersTradeConfirm()
	{
		while (!AllPlayersTraded())
		{
			yield return null;
		}

		//////Debug.Log ("~ LETS DO THIS! TRICK IT UP!");
		TrickStart ();
	}

	/// <summary>
	/// Executes the round's result
	/// </summary>
	private void RoundResult()
	{
		//////Debug.Log("Round result being executed",Color.magenta);
		//Calculate score per player, and reset the main deck. Give cards and tricks to winner if loser.
		foreach(PlayerHandler player in players)
		{
			if(player.isLoser)
			{
				foreach(PlayerHandler player2 in players)
				{
					if(player2.isWinner)
					{
						foreach (Card c in player.hand)
							player2.team.score += c.scoreValue;
						foreach (Card c in player.trickStash)
						{
							player2.team.score += c.scoreValue;
							individualScores[player2.playerId] += c.scoreValue;
							if(c.cardObject != null && c.cardObject.gameObject != null)
								Destroy (c.cardObject.gameObject);
						}
						player.trickStash.Clear();
						player.hand.Clear();
						break;
					}
				}
			}
			else
			{
				foreach(Card c in player.hand)
				{
					player.trickStash.Add(c);
				}
			}

			if (player.isWinner)
				player.team.score += (int)player.tichuType;
			else
				player.team.score -= (int)player.tichuType;

			individualScores[player.playerId] += player.isWinner ? (int)player.tichuType : -((int)player.tichuType);

			foreach (Card c in player.trickStash)
			{
				player.team.score += c.scoreValue;
				individualScores[player.playerId] += c.scoreValue;
				if(c.cardObject != null && c.cardObject.gameObject != null)
					Destroy (c.cardObject.gameObject);
			}
			player.trickStash.Clear();
			player.hand.Clear();
			player.tichuType = TichuType.NONE;
		}

		foreach(PlayerHandler player in players)
		{
			player.isWinner = false;
			player.isLoser = false;
		}
	}

	/// <summary>
	/// Checks whether there's a winner
	/// </summary>
	/// <returns></returns>
	private bool GotWinner()
	{
		bool win = false;
		foreach(PlayerHandler player in players)
		{
			if (player.team.score >= winScore)
			{
				if(winTeam != 0)
				{
					winTeam = Team.team1.score > Team.team2.score ? Team.team1.id : Team.team2.id;
				}
				else
					winTeam = player.team.id;
				win = true;
			}
		}
		return win;
	}

	/// <summary>
	/// Checks whether all players are ready
	/// </summary>
	/// <returns></returns>
	private bool AllPlayersReady()
	{
		foreach(PlayerHandler player in players)
		{
			if (!player.isReady)
				return false;
		}
		return true;
	}

	/// <summary>
	/// Checks whether all players have traded
	/// </summary>
	/// <returns></returns>
	private bool AllPlayersTraded()
	{
		foreach(PlayerHandler player in players)
		{
			if (!player.hasTraded)
				return false;
		}
		return true;
	}

	/// <summary>
	/// Sets the players unready.
	/// </summary>
	private void SetPlayersUnready()
	{
		foreach(PlayerHandler player in players)
		{
			player.isReady = false;
		}
		NetworkEmulator.main.SendData("R9");
	}

	/// <summary>
	/// Progresses the turn.
	/// </summary>
	/// <param name="forReal">Whether to actually increase the current turn</param>
	private void ProgressTurn(bool forReal = true)
	{
		if(forReal)
			currentTurn++;

		////Debug.Log (currentTurn + " Next Turn");

		int targetPlayerId = (leaderId + currentTurn) % players.Length;
		while(playersOut[targetPlayerId])
		{
			currentTurn++;
			passes++;
			targetPlayerId = (leaderId + currentTurn) % players.Length;
		}

		//turnIndicator.IndicateActivePlayer (targetPlayerId);

		int lastPlayHiVal = 255;
		int lastPlayLoVal = 255;
		if (lastPlay != null && lastPlay.Count > 0)
		{
			if (lastPlay[0] != null && lastPlay[0].card != null)
				lastPlayLoVal = (int)lastPlay[0].card.value;
			if (lastPlay[lastPlay.Count - 1] != null && lastPlay[lastPlay.Count - 1].card != null)
				lastPlayHiVal = (int)lastPlay[lastPlay.Count - 1].card.value;
		}

		NetworkEmulator.main.SendData("r9" + targetPlayerId.ToString() + ((int)lastPlayKind).ToString() + (char)lastValue + (char)lastPlay.Count + (char)lastPlayLoVal + (char)lastPlayHiVal); //Adjusted as of 2016.03.21
		//NetworkEmulator.main.SendData();
		turnProgressEvent((leaderId + currentTurn) % players.Length);

		SetPlayersUnready();
	}

	/// <summary>
	/// Handle received data
	/// </summary>
	/// <param name="data">Received data string.</param>
	public override void ReceiveData(string data)
	{
		char[] bytes = data.ToCharArray();
		int playerId = int.Parse(bytes[1].ToString());

		if (playerId == 9) //Ignore own messages
			return;

		//if (currentState == GameState.ROUND_START)
		//////Debug.Log("Message Arrived: " + data,new Color(1,.7f,.3f));

		switch(bytes[0])
		{
			case 'R': //Ready operation
				if (bytes[2] == 't') //Toggle ready
					players[playerId].isReady = !players[playerId].isReady;
				else if (bytes[2] == '-') //Ready false
					players[playerId].isReady = false;
				else if (bytes[2] == '+' || bytes[2] == 'p') //Ready true
				{
					players[playerId].isReady = true;
					if (currentState == GameState.TRICK && (leaderId + currentTurn)%4 == playerId)
						ProgressTurn(!(passes >= 3 && bytes[2] == 'p'));

					if (passes >= 3 && bytes[2] == 'p')
						TrickEnd();
				}
				break;
			case 'T': //Tichu State operation
				SetPlayerTichu(players[playerId]);
				break;
			case 't': //Trade operation
				if (bytes.Length < 4 || bytes.Length % 2 != 0)
					break;

				////Debug.Log("<color=#22aa44>Received Trade operation \"" + data + "\" from player " + playerId + "</color>");

				for(int i = 2; i < bytes.Length; i += 2)
				{
					int targetPlayer = int.Parse(bytes[i].ToString());
					int cardId = ((int)bytes[i + 1]);
					////Debug.Log("<color=#22aa44>Giving card ID \"" + cardId + "\" to player " + targetPlayer + "</color>");

					for (int j = 0; j < players[playerId].hand.Count; j++)
					{
						Card targetCard = players[playerId].hand[j];

						if (targetCard.id == cardId)
						{
							players[targetPlayer].hand.Add(targetCard);
							players[playerId].hand.Remove(targetCard);
							break;
						}
					}

					NetworkEmulator.main.SendDataTo(targetPlayer, "C9+" + bytes[i+1]);
					players [targetPlayer].tradeCardsReceived += 1;
					players [targetPlayer].HasTraded ();
				}
				break;
			case 'c': //Played Cards operation

				foreach(CardObject cObj in lastPlay)
				{
					Destroy(cObj.gameObject);
				}
				lastPlay.Clear();
				lastPlayer = playerId;
				lastPlayKind = (PlayKind)int.Parse(bytes[2].ToString());
				lastValue = 0;
				List<float> values = new List<float>();
				for(int i = 3; i < bytes.Length; i++)
				{
					int id = ((byte)bytes[i]);
					GameObject cardGameObj = Instantiate(cardPrefab, ui.player[playerId].playerCardSpawnPosition.position, Quaternion.identity) as GameObject;
					cardGameObj.transform.SetParent(cardParent, false);
					CardObject cardObj = cardGameObj.GetComponent<CardObject>();
					cardObj.card = idDeck[id];
					cardObj.manager = this;
					cardObj.uiHandler = ui;
					lastPlay.Add(cardObj);
					trickStash.Add(cardObj.card);

					for (int j = 0; j < players[playerId].hand.Count; j++)
					{
						Card targetCard = players[playerId].hand[j];

						if (targetCard.id == id)
						{
							players[playerId].hand.Remove(targetCard);
							break;
						}
					}

					cardObj.card.OnPlacement(playerId);
					lastValue += cardObj.card.value;
					lastCard = cardObj.card;
					values.Add(cardObj.card.value);

					if(players[playerId].hand.Count == 0)
					{
						goto case 'E';
					}
				}

				foreach(Card card in lastPlay)
				{
					if(card.GetType() == typeof(CardPhoenix))
					{
						lastValue -= card.value;
						if(lastPlayKind != PlayKind.SINGLE_PLAY)
						{
							foreach (float val in values)
							{
								if (val < 1000)
								{
									card.value = 0;
									card.value = Mathf.Max(card.value, val);
								}
							}
						}
						else
						{
							if (trickStash.Count > 1)
								card.value = trickStash[trickStash.Count - 2].value + 0.5f;
							else
								card.value = 1.5f;
						}
						lastValue += card.value;
					}
				}
				break;
			case 'P': //Pass/Skip Turn operation
				if (currentState == GameState.TRICK)
				{
					if (bytes[2] == '+')
					{
						passes++;
					}
					else if (bytes[2] == 'c')
					{
						passes = 0;
					}
				}
				break;
			case 'E': //Empty Hand operation
				//////Debug.Log("<color=#0f0>Hand Empty Message</color>");
				if (currentState == GameState.TRICK || currentState == GameState.TRICK_RESULT)
				{
					if (!playersOut[playerId])
					{
						PlayOutEffect(outOrder.Count, playerId);
						outOrder.Add(playerId);
					}

					playersOut[playerId] = true;
					int amountOut = 0;
					int balance = 0;
					for (int i = 0; i < playersOut.Length; i++)
					{
						playersOut[i] = players[i].hand.Count == 0;

						if (playersOut[i])
						{
							amountOut++;
							balance += players[i].team.id;
							//////Debug.Log("Player " + i + " is out.", Color.yellow);
						}
					}

					//////Debug.Log("Balance is now " + balance, Color.yellow);
					if (amountOut == 1)
						players[playerId].isWinner = true;

					if (Mathf.Abs(balance) >= 2)
						RoundEnd(true, (int)Mathf.Sign(balance));
					else if (amountOut >= 3)
					{
						for (int i = 0; i < players.Length; i++)
						{
							if (!playersOut[i])
								players[i].isLoser = true;
						}

						TrickEnd(true);
						RoundEnd(false, (int)Mathf.Sign(balance));
					}

					NetworkEmulator.main.SendDataTo(playerId, "E9");
				}
				break;
			case 'O': //Options operation (Added as of 2016.03.22)
				int musicVolume = (int)bytes[1];
				int soundVolume = (int)bytes[2];
				int language = (int)bytes[3];

				GameOptions.musicVolume = ((float)musicVolume) / 255;
				GameOptions.soundVolume = ((float)soundVolume) / 255;
				GameOptions.language = (GameLanguage)language;
				break;
			case 'B': //Break/Pause operation (Added as of 2016.03.22)
				{
					if (bytes[1] == '+')
						GameOptions.paused = true;
					else if (bytes[1] == '-')
						GameOptions.paused = false;
				}
				break;
			case 'D': //Dragon card operation //Added as of 2016.03.30
				{
					int targetPlayer = int.Parse(bytes[2].ToString());
					////Debug.Log("<color=#4ca>Sending trickstash containing the " + trickStash[0].name + " to player " + targetPlayer + "</color>");
					//ChangeWinPlayer(targetPlayer);
					trickGainPlayer = targetPlayer;
					TrickEndPartTwo();
				}
				break;
			case 'J': //Mahjong card operation //Added as of 2016.03.30
				{
					if (bytes[2] == 'X')
					{
						mahjongCard.ApplyValueWish(-1);
						ComputerPlayer.wishValue = -1;
						ui.mahJongIndicator.SetActive (false);
					}
					else
					{
						int targetValue = (byte)bytes[2];
						mahjongCard.ApplyValueWish(targetValue);
						ComputerPlayer.wishValue = targetValue;
						ui.mahJongIndicator.SetActive (true);
						ui.SetMahjongValue (targetValue);
						currentState = GameState.TRICK;
						ProgressTurn();
					}
					////Debug.Log("<color=#484>Mahjong Operation Received</color>");
					//TrickEndPartTwo();
					//currentTurn++;

				}
				break;
			case '!': //Ping-Pong operation
				{
					string message = data.Substring(2);
					////Debug.Log("<color=#50a>Ping message: " + message + "</color>");
					NetworkEmulator.main.SendDataTo(playerId, "!9" + message);
				}
				break;
			case '>':
				{
					////Debug.LogWarning("<color=#255>" + playerId + "> " + data.Substring(2) + "</color>");
				}
				break;
		}

		ui.GetPlay ();
		ui.GetTrick ();

		for (int i = 0; i < 4; i++)
			ui.GetHand (i);
	}

	/// <summary>
	/// Plaies the out-of-cards effect.
	/// </summary>
	/// <param name="fxNumber">Fx ID number.</param>
	/// <param name="posNumber">Position ID (player id).</param>
	public void PlayOutEffect(int fxNumber, int posNumber)
	{
		try
		{
			GameObject fx = Instantiate(outOfCardsFX[fxNumber], fxPosition[posNumber].position, Quaternion.identity) as GameObject;
			Destroy(fx, confettiEffectDuration);
		}
		catch { }
	}

	/// <summary>
	/// Checks if most players are out
	/// </summary>
	/// <returns></returns>
	public bool MostPlayersOut()
	{
		int amount = 0;
		foreach(bool o in playersOut)
		{
			if (o)
				amount++;
		}
		return amount >= 3;
	}

	/// <summary>
	/// Changes the player turn.
	/// </summary>
	public override void ChangePlayerTurn()
	{
		TrickEnd();
		currentTurn += 2;
		NetworkEmulator.main.SendData("r9" + ((leaderId + currentTurn) % 4).ToString() + ((int)PlayKind.NONE).ToString() + (char)0 + (char)0 + (char)0 + (char)0);
		turnProgressEvent((leaderId + currentTurn) % players.Length);
	}

	/// <summary>
	/// Changes the winning player.
	/// </summary>
	/// <param name="playerId">Player identifier.</param>
	public override void ChangeWinPlayer(int playerId)
	{
		lastPlayer = playerId;
	}

	/// <summary>
	/// Handle round's end
	/// </summary>
	/// <param name="teamWin">Is it a team win?</param>
	/// <param name="winningTeam">Winning team.</param>
	private void RoundEnd(bool teamWin, int winningTeam)
	{
		currentState = GameState.ROUND_RESULT;

		foreach(PlayerHandler player in players)
		{
			if (player.isLoser)
				outOrder.Add(player.playerId);
		}

		if(teamWin)
		{
			if (Team.team1.id == winningTeam)
				Team.team1.score += 200;
			if (Team.team2.id == winningTeam)
				Team.team2.score += 200;
			//////Debug.Log("Added 200 points to team " + winningTeam + ", they are now at " + (Team.team1.id == winningTeam? Team.team1.score : Team.team2.score).ToString(),Color.blue);

			for(int i = 0; i < individualScores.Length; i++)
			{
				if (players[i].team.id == winningTeam)
					individualScores[i] += 100;
			}

			for (int i = 0; i < playersOut.Length; i++)
				playersOut[i] = false;
		}
		else
		{
			RoundResult();
		}

		for (int i = 0; i < lastPlay.Count; i++)
			Destroy (lastPlay[i].gameObject);
		lastPlay.Clear();
		for (int i = 0; i < trickStash.Count; i++)
		{
			if(trickStash[i].cardObject != null && trickStash[i].cardObject.gameObject != null)
				Destroy(trickStash[i].cardObject.gameObject);
		}
		trickStash.Clear();

		foreach (PlayerHandler player in players)
		{
			if (player.isWinner)
				player.team.score += (int)player.tichuType;
			else
				player.team.score -= (int)player.tichuType;

			individualScores[player.playerId] += player.isWinner ? (int)player.tichuType : -((int)player.tichuType);

			player.isWinner = false;
			player.isLoser = false;
			player.tichuType = TichuType.NONE;
		}

		for (int i = 0; i < playersOut.Length; i++)
			playersOut[i] = false;

		foreach (PlayerHandler player in players)
		{
			player.hasTraded = false;
			player.tradeCardsReceived = 0;
			individualScoresTotal[player.playerId] += individualScores[player.playerId];
		}

		NetworkEmulator.main.SendData("C9cx");
		if (GotWinner())
		{
			currentState = GameState.GAME_RESULT;
		}
		else
		{
			ShuffleCards();
			currentState = GameState.SCOREBOARD;
		}

		foreach (PlayerHandler player in players)
		{
			player.isReady = false;
			player.displayTichu.Display (player.tichuType);
		}
	}

	/// <summary>
	/// Ends the trick
	/// </summary>
	/// <param name="isEnd">Is the final trick?</param>
	private void TrickEnd(bool isEnd = false) //Adjusted as of 2016.03.30
	{
		currentState = GameState.TRICK_RESULT;

		foreach (CardObject cObj in lastPlay)
		{
			cObj.card.OnTrickEnd();
			if (cObj.card.scoreValue == 25)
				Destroy(cObj.gameObject);
			else
			{
				Destroy(cObj.gameObject, trickCardDestroyDelay);
				ui.cardMovingObjects[trickGainPlayer].Add(cObj.gameObject);
			}
		}
		lastPlay.Clear();

		if (currentState != GameState.DRAGON_CARD)
			TrickEndPartTwo(isEnd);
	}

	/// <summary>
	/// Later part of trickś end
	/// </summary>
	/// <param name="isEnd">Last trick?</param>
	private void TrickEndPartTwo(bool isEnd = false) //Added as of 2016.03.30
	{
		if (trickStash.Count > 0)
		{
			foreach (Card card in trickStash)
			{
				if (card != null)
					players[/*lastPlayer*/trickGainPlayer].trickStash.Add(card);
			}
		}

		//////Debug.Log("Destroy CARDS!");

		for (int i = 0; i < lastPlay.Count; i++)
			Destroy(lastPlay[i].gameObject);
		lastPlay.Clear();
		for (int i = 0; i < trickStash.Count; i++)
		{
			try
			{
				Destroy(trickStash[i].cardObject.gameObject, trickCardDestroyDelay);
				ui.cardMovingObjects[trickGainPlayer].Add(trickStash[i].cardObject.gameObject);
			}
			catch { }
		}
		trickStash.Clear();

		currentTrick++;

		lastCard = null;
		lastPlayKind = PlayKind.NONE;
		lastValue = 0;

		if (!isEnd)
			currentState = GameState.TRICK;

		//turnIndicator.IndicateActivePlayer ((leaderId + currentTurn) % players.Length);
		turnProgressEvent((leaderId + currentTurn) % players.Length);
	}

	/// <summary>
	/// Sets the player tichu.
	/// </summary>
	/// <param name="player">Player.</param>
	public void SetPlayerTichu(PlayerHandler player)
	{
		switch(currentState)
		{
			case GameState.ROUND_START:
				player.tichuType = TichuType.GRAND_GRAND_TICHU;
				break;
			case GameState.FIRST_DEAL:
				player.tichuType = TichuType.GRAND_TICHU;
				break;
			default:
				player.tichuType = TichuType.TICHU;
				break;
		}

		if (player.tichuType != TichuType.NONE)
			player.displayTichu.Display (player.tichuType);
	}
}