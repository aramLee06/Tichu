using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Screen user interface handler.
/// </summary>
public class ScreenUIHandler : UIHandler
{
	[Header ("Files")]
	// Files
	/// <summary>
	/// The tichu graphics.
	/// </summary>
	public Sprite[] tichuGraphic;
	/// <summary>
	/// The card absorb sound.
	/// </summary>
	public AudioClip cardAbsorbSound;

	[Header ("Player Data")]
	/// <summary>
	/// The players.
	/// </summary>
	public Player[] player;
	//public List<CardObject>[] hand = new List<CardObject>[4];

	[Header ("Game Data")]
	// General Game Data
	/// <summary>
	/// The game manager.
	/// </summary>
	public GameManager gameManager;
	/// <summary>
	/// The host controller.
	/// </summary>
	public HostController host;
	public int currentWorth = 0;
	public float gameTime = 0, turnTime = 60;
	public byte currentWinner = 0, currentTurn = 0;

	[Header ("UI Objects")]
	// UI Objects
	/// <summary>
	/// The turn indicator.
	/// </summary>
	public GameObject turnIndicator;
	/// <summary>
	/// The mah jong indicator.
	/// </summary>
	public GameObject mahJongIndicator;
	/// <summary>
	/// Various Text UI objects
	/// </summary>
	public Text mahjongWish,
	gameTimer, turnTimer,
	currentWorthCounter, currentWinnerIndicator;
	/// <summary>
	/// The hand displays.
	/// </summary>
	public HandDisplayerScreen[] handDisplays;
	/// <summary>
	/// Various positions
	/// </summary>
	public Transform trickAreaPosition,
	playAreaPosition,
	exitPosition,
	cardSpawnPosition;

	/// <summary>
	/// The moving card objects.
	/// </summary>
	public List<GameObject>[] cardMovingObjects = new List<GameObject>[4]
	{
		new List<GameObject>(), new List<GameObject>(),
		new List<GameObject>(), new List<GameObject>(),
	};
	/// <summary>
	/// The card end positions.
	/// </summary>
	public Transform[] cardEndPositions;
	/// <summary>
	/// The avatar default scalings.
	/// </summary>
	public Vector3[] avatarDefaultScaling = new Vector3[]
	{
		new Vector3(1,1,1), new Vector3(-1,1,1),
		new Vector3(-1,1,1), new Vector3(1,1,1),
	};

	public float avatarGetCardScaling = 1.5f;

	private void Awake ()
	{
		if (!main)
			main = this;

		gameManager = GameManager.main;
		host = GameObject.Find ("HostController").GetComponent<HostController> ();
	}

	private void Start ()
	{
		for (int i = 0; i < player.Length; i++)
		{
			if (host.playerName [i] != "")
				player [i].playerNameIndicator.text = host.playerName [i];
		}
	}

	/// <summary>
	/// Sets the mahjong value.
	/// </summary>
	/// <param name="value">Value.</param>
	public void SetMahjongValue (int value)
	{
		switch (value)
		{
			case 13:
				mahjongWish.text = "A";
				break;
			case 12:
				mahjongWish.text = "K";
				break;
			case 11:
				mahjongWish.text = "Q";
				break;
			case 10:
				mahjongWish.text = "J";
				break;
			default:
				mahjongWish.text = (value + 1).ToString();
				break;
		}
	}

	/// <summary>
	/// Gets the hand.
	/// </summary>
	/// <param name="playerNumber">Player number.</param>
	public void GetHand (int playerNumber)
	{
		handDisplays [playerNumber].SetCards (gameManager.players [playerNumber].hand.Count);
	}

	/// <summary>
	/// Gets the trick.
	/// </summary>
	public void GetTrick ()
	{
		//Debug.Log ("Screen ~ Get Trick");

		trick = gameManager.trickStash;

		for (int i = 0; i < (trick.Count - play.Count); i++)
		{
			if (trick [i].cardObject == null)
			{
				GameObject newCard = Instantiate (cardObjectPrefab, cardSpawnPosition.position, Quaternion.identity) as GameObject;
				trick [i].cardObject = newCard.GetComponent<CardObject> ();
				trick [i].cardObject.card = trick [i];
				trick [i].cardObject.uiHandler = this;
				trick [i].cardObject.transform.parent = trickAreaPosition;
				trick [i].Start ();
			}

			trick [i].cardObject.transform.parent = trickAreaPosition;
		}
	}

	/// <summary>
	/// Gets the play.
	/// </summary>
	public void GetPlay ()
	{
		//Debug.Log ("Gamepad ~ Get Hand");

		play = gameManager.lastPlay;

		for (int i = 0; i < play.Count; i++)
		{
			if (play [i] == null)
			{
				GameObject newCard = Instantiate (cardObjectPrefab, player[gameManager.leaderId].playerCardSpawnPosition.position, Quaternion.identity) as GameObject;
				play [i] = newCard.GetComponent<CardObject> ();
				play [i].card = play [i];
				play [i].uiHandler = this;
				play [i].Start ();
			}

			play [i].transform.parent = playAreaPosition;
		}
	}

	private void Update ()
	{
		DisplayPlay (play);
		DisplayTrick (trick);
		DisplayTrickReward ();

		currentWorth = 0;

		for (int i = 0; i < trick.Count; i++)
			currentWorth += trick [i].scoreValue;

		currentWorthCounter.text = currentWorth.ToString("");
		currentWinnerIndicator.text = (gameManager.lastPlayer + 1).ToString ();
	}

	/// <summary>
	/// Gets the card speed.
	/// </summary>
	/// <returns>The card speed.</returns>
	/// <param name="target">Target.</param>
	/// <param name="position">Position.</param>
    private float GetCardSpeed(Vector3 target, Vector3 position)
    {
		return (Time.deltaTime * cardSpeedCurve.Evaluate(Vector3.Distance(position, target) * targetDistanceUntilSlowdown) * cardLerpSpeed);
    }

	/// <summary>
	/// Displays the play.
	/// </summary>
	/// <param name="play">Play.</param>
    private void DisplayPlay (List<CardObject> play)
	{
		for (int i = 0; i < play.Count; i++)
		{
			play [i].transform.SetParent (playAreaPosition);
			Vector3 target = new Vector3 (((playAreaTotalWidth / (play.Count + 1)) * (i + 1)) - playAreaWidthOffset, 0); //playPosition.localPosition.y, playPosition.localPosition.z);
			play [i].transform.localPosition = Vector3.MoveTowards (play [i].transform.localPosition, target, GetCardSpeed(target, play[i].transform.localPosition));
			play [i].transform.localRotation = Quaternion.Euler(Vector3.zero);
		}
	}

	/// <summary>
	/// Displays the trick.
	/// </summary>
	/// <param name="trick">Trick.</param>
	private void DisplayTrick (List<Card> trick)
	{
		for (int i = 0; i < (trick.Count - play.Count); i++)
		{
			if (trick [i].cardObject != null)
			{
				trick [i].cardObject.transform.SetParent (trickAreaPosition);
				Vector3 target = new Vector3 (((playAreaTotalWidth / ((trick.Count - play.Count) + 1)) * (i + 1)) - playAreaWidthOffset, 0);
				trick [i].cardObject.transform.localPosition = Vector3.MoveTowards (trick [i].cardObject.transform.localPosition, target, GetCardSpeed(target, trick[i].cardObject.transform.localPosition));
				trick [i].cardObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
			}
		}
	}

	/// <summary>
	/// Displays the trick reward.
	/// </summary>
	private void DisplayTrickReward()
	{
		for (int i = 0; i < cardMovingObjects.Length; i++)
		{
			Vector3 targetPos = cardEndPositions [i].position;

			int j = 0;
			while (j < cardMovingObjects [i].Count)
			{
				if (cardMovingObjects [i] [j] == null)
					cardMovingObjects [i].RemoveAt (j);
				else
					j++;
			}

			bool close = false;
			foreach (GameObject go in cardMovingObjects[i])
			{
				go.transform.position = Vector3.MoveTowards (go.transform.position, targetPos, GetCardSpeed (targetPos, go.transform.position));
				if (Vector3.Distance (go.transform.position, targetPos) < .1f)
					close = true;
			}

			if (close)
				GetComponent<AudioSource> ().PlayOneShot (cardAbsorbSound);

			player [i].playerAvatarDisplay.transform.localScale = Vector3.Lerp (player [i].playerAvatarDisplay.transform.localScale, close ? avatarDefaultScaling [i] * avatarGetCardScaling : avatarDefaultScaling [i], .25f);
		}
	}

	/// <summary>
	/// Deals the cards.
	/// </summary>
	/// <param name="cards">Cards.</param>
	/// <param name="targetPos">Target position.</param>
	/// <param name="delay">Delay.</param>
	public void DealCards (List<Card> cards, Transform targetPos, float delay = 0.1f)
	{
		StartCoroutine (DelayCards (cards, targetPos, delay));
	}

	/// <summary>
	/// Delays the cards.
	/// </summary>
	/// <returns>The cards.</returns>
	/// <param name="cards">Cards.</param>
	/// <param name="targetPosition">Target position.</param>
	/// <param name="delay">Delay.</param>
	public IEnumerator DelayCards (List<Card> cards, Transform targetPosition, float delay = 0.1f)
	{
		for (int i = 0; i < cards.Count; i++)
		{
			Deal (cards [i], cards, targetPosition, i);
			yield return new WaitForSeconds (delay);
		}
	}

	/// <summary>
	/// Deal the specified card..
	/// </summary>
	/// <param name="card">Card.</param>
	/// <param name="targetList">Target list.</param>
	/// <param name="targetPosition">Target position.</param>
	/// <param name="handPos">Hand position.</param>
	public void Deal (Card card, List<Card> targetList, Transform targetPosition, int handPos)
	{
		if (card.cardObject == null)
		{
			GameObject newCard = Instantiate (cardObjectPrefab, cardSpawnPosition.position, Quaternion.identity) as GameObject;
			card.cardObject = newCard.GetComponent<CardObjectScreen> ();
			card.cardObject.transform.parent = targetPosition;
			card.cardObject.handPos = handPos;
			card.Start ();
		}
	}

	/// <summary>
	/// Switches the state
	/// </summary>
	/// <param name="state">State.</param>
	public void StateSwitch (GameState state)
	{
		currentState = state;

		switch (currentState)
		{
			case GameState.ROUND_START:
				
				break;
			case GameState.FIRST_DEAL:
				
				break;
			case GameState.SECOND_DEAL:
				
				break;
			case GameState.TRADING:
				
				break;
			case GameState.TRICK:
				
				break;
			case GameState.TRICK_RESULT:

				break;
			case GameState.ROUND_RESULT:

				break;
			default:
				
				break;
		}
	}
}

/// <summary>
/// Player Object
/// </summary>
[System.Serializable]
public class Player
{
	// Player Data & UI Objects
	/// <summary>
	/// The player avatar.
	/// </summary>
	public Texture2D playerAvatar;
	/// <summary>
	/// The name of the player.
	/// </summary>
	public string playerName;
	/// <summary>
	/// The player score.
	/// </summary>
	public int playerScore;
	/// <summary>
	/// The hand.
	/// </summary>
	public List<Card> hand;
	/// <summary>
	/// The player handler.
	/// </summary>
	public PlayerHandler handler;

	/// <summary>
	/// The player score counter.
	/// </summary>
	public Text playerScoreCounter;
	/// <summary>
	/// The player name indicator.
	/// </summary>
	public Text playerNameIndicator;
	/// <summary>
	/// The player avatar display.
	/// </summary>
	public Image playerAvatarDisplay;
	/// <summary>
	/// The player card position.
	/// </summary>
	public Transform playerCardPosition;
	/// <summary>
	/// The player card spawn position.
	/// </summary>
	public Transform playerCardSpawnPosition;
}