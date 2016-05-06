using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ScreenUIHandler : UIHandler
{
	[Header ("Files")]
	// Files
	public Sprite[] tichuGraphic;
	public AudioClip cardAbsorbSound;

	[Header ("Player Data")]
	public Player[] player;
	//public List<CardObject>[] hand = new List<CardObject>[4];

	[Header ("Game Data")]
	// General Game Data
	public GameManager gameManager;
	public HostController host;
	public int currentWorth = 0;
	public float gameTime = 0, turnTime = 60;
	public byte currentWinner = 0, currentTurn = 0;

	[Header ("UI Objects")]
	// UI Objects
	public GameObject turnIndicator;
	public GameObject mahJongIndicator;
	public Text mahjongWish,
	gameTimer, turnTimer,
	currentWorthCounter, currentWinnerIndicator;
	public HandDisplayerScreen[] handDisplays;
	public Transform trickAreaPosition,
	playAreaPosition,
	exitPosition,
	cardSpawnPosition;

	public List<GameObject>[] cardMovingObjects = new List<GameObject>[4]
	{
		new List<GameObject>(), new List<GameObject>(),
		new List<GameObject>(), new List<GameObject>(),
	};
	public Transform[] cardEndPositions;
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

	public void GetHand (int playerNumber)
	{
		handDisplays [playerNumber].SetCards (gameManager.players [playerNumber].hand.Count);
	}

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

    private float GetCardSpeed(Vector3 target, Vector3 position)
    {
		return (Time.deltaTime * cardSpeedCurve.Evaluate(Vector3.Distance(position, target) * targetDistanceUntilSlowdown) * cardLerpSpeed);
    }

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

	public void DealCards (List<Card> cards, Transform targetPos, float delay = 0.1f)
	{
		StartCoroutine (DelayCards (cards, targetPos, delay));
	}

	public IEnumerator DelayCards (List<Card> cards, Transform targetPosition, float delay = 0.1f)
	{
		for (int i = 0; i < cards.Count; i++)
		{
			Deal (cards [i], cards, targetPosition, i);
			yield return new WaitForSeconds (delay);
		}
	}

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

[System.Serializable]
public class Player
{
	// Player Data & UI Objects
	public Texture2D playerAvatar;
	public string playerName;
	public int playerScore;
	public List<Card> hand;
	public PlayerHandler handler;

	public Text playerScoreCounter;
	public Text playerNameIndicator;
	public Image playerAvatarDisplay;
	public Transform playerCardPosition;
	public Transform playerCardSpawnPosition;
}