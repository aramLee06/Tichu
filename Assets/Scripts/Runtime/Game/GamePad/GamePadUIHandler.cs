using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// UI Handler for the Gamepad
/// </summary>
public class GamePadUIHandler : UIHandler 
{
	[Header("General")]
	/// <summary>
	/// The gamepad manager.
	/// </summary>
	public GamepadManager gamepadManager;
	/// <summary>
	/// The player number.
	/// </summary>
	public byte playerNumber = 0;

	/// <summary>
	/// Various positions
	/// </summary>
	public Transform cardPosition, playPosition, cardSpawnPosition, playAreaSlotPosition;
	/// <summary>
	/// The trade slot positions.
	/// </summary>
	public Transform[] tradeSlotPosition;

	/// <summary>
	/// The dragon give buttons.
	/// </summary>
	public Button[] dragonGiveButton;
	/// <summary>
	/// The dragon sprites.
	/// </summary>
	public Sprite[] dragonSprites;

	public bool response = false;
	/// <summary>
	/// The special target number.
	/// </summary>
	public int specialTarget;

	/// <summary>
	/// The hand.
	/// </summary>
	public List<Card> hand;

	/// <summary>
	/// Various buttons
	/// </summary>
	public Button
	playButton, passButton,
	tichuButton;

	public Button[] buttonsDisabledOnNotTurn, tradeArea;
	public GameObject notYourTurnPanel, mahjongScreen, dragonScreen;

	public bool canCall = true;

	private void Awake()
	{
		if (!main)
			main = this;
	}

	void Start ()
	{
		gamepadManager = GamepadManager.main;
	}

	void Update ()
	{
		DisplayCards ();
	}

    /// <summary>
    /// Handles Dragon Event UI-side
    /// </summary>
	public void Dragon ()
	{
		dragonGiveButton [0].image.sprite = dragonSprites[gamepadManager.targetPlayer[0]];
		dragonGiveButton [1].image.sprite = dragonSprites[gamepadManager.targetPlayer[2]];

		Debug.Log (gamepadManager.targetPlayer[0] + " " + gamepadManager.targetPlayer[2]);

		StartCoroutine (WaitForResponse(dragonScreen, true));
	}

    /// <summary>
    /// Handles Mhajong Event UI-side
    /// </summary>
	public void Mahjong ()
	{
		StartCoroutine (WaitForResponse(mahjongScreen, false));
	}

    /// <summary>
    /// Handles respond from Dragon/Mahjong event
    /// </summary>
    /// <param name="target">Target value (Case of dragon: Player to give cards to, case of mahjong: wish value)</param>
	public void Respond (int target)
	{
		response = true;
		specialTarget = target;
	}

    /// <summary>
    /// Waits for mahjong/dragon event's resonse
    /// </summary>
    /// <param name="screen">Screen object</param>
    /// <param name="isDragon">Whether it's for the dragon event or not</param>
    /// <returns></returns>
	public IEnumerator WaitForResponse (GameObject screen, bool isDragon)
	{
		response = false;
		screen.SetActive (true);

		while (!response)
		{
			yield return null;
		}

		if (isDragon)
		{
			gamepadManager.DragonSelectPlayer (gamepadManager.targetPlayer [specialTarget]);
			Debug.Log (gamepadManager.targetPlayer [specialTarget]);
		}
		else
			NetworkEmulator.main.SendDataToHost("J" + playerNumber + (char)specialTarget);
				
		screen.SetActive (false);
	}

    /// <summary>
    /// Gets the current hand
    /// </summary>
	public void GetHand ()
	{
		//Debug.Log ("Gamepad ~ Get Hand");

		hand = gamepadManager.hand;

		for (int i = 0; i < hand.Count; i++)
		{
			if (hand [i].cardObject == null)
			{
				GameObject newCard = Instantiate (cardObjectPrefab, cardSpawnPosition.position, Quaternion.identity) as GameObject;
				hand [i].cardObject = newCard.GetComponent<CardObject> ();
				hand [i].cardObject.card = hand [i];
				hand [i].cardObject.uiHandler = this;
				hand [i].cardObject.transform.parent = cardPosition;
				hand [i].cardObject.handPos = i;
				hand [i].cardObject.handPos = GetHandPos (hand [i], hand.ToArray ());
				hand [i].Start ();
			}
		}
	}

    /// <summary>
    /// Gets the card's position in the hand
    /// </summary>
    /// <param name="card">Specified card</param>
    /// <param name="array">The hand</param>
    /// <param name="arrangeBySuit"></param>
    /// <returns></returns>
	public int GetHandPos (Card card, Card[] array, bool arrangeBySuit = false)
	{
		float cardValue = 0;
		float otherValue = 0;

		if (arrangeBySuit)
		{
			cardValue = card.value + ((int)card.cardType * 20f);

			if (array.Length > 0)
			{
				for (int i = 0; i < array.Length; i++)
				{
					otherValue = array [i].value + ((int)array [i].cardType * 20f);

					if (otherValue > cardValue)
					{
						//Debug.Log ("Set HandPos to " + i);
						OrganizeCards (hand, card.cardObject.handPos, i);
						return i;
					}
				}

				//Debug.Log ("Set HandPos to " + array.Length);
				return array.Length;
			}
			else
			{
				//Debug.Log ("Set HandPos to " + 0);
				return 0;
			}
		}
		else
		{
			cardValue = card.value;

			if (array.Length > 0)
			{
				for (int i = 0; i < array.Length; i++)
				{
					otherValue = array [i].value;

					if (otherValue > cardValue)
					{
						OrganizeCards (hand, card.cardObject.handPos, i);
						return i;
					}
				}
				return array.Length;
			}
			else
			{
				return 0;
			}
		}
	}

    /// <summary>
    /// Displays the current cards
    /// </summary>
	private void DisplayCards ()
	{
		for (int i = 0; i < hand.Count; i++)
		{
			if (hand [i].cardObject != null)
			{
				///Position
				float x = ((cardAreaTotalWidth / (hand.Count + 1)) * (i + 1)) - cardAreaWidthOffset;
				float y = (Mathf.Abs (x) * cardAreaTotalHeight);
				Vector3 target = new Vector3 (x, y, i * 0.05f);

				if (hand [i].cardObject.isSelected)
				{
					target.y -= cardInsetOnSelect;
				}
				else if (hand [i].cardObject.isHovering)
				{
					target.y += (cardInsetOnSelect * 0.1f);
				}

				if (hand [i].cardObject.isDragged)
				{
					if (option_MoveToBot)
					{
						target = cardPosition.position;
						target.y -= 4;
					}
					else
					{
						Vector3 pos;
						if (Application.isEditor)
							pos = Input.mousePosition;
						else
							pos = Input.touches [0].position;
						
						pos.z = 9;
						target = Camera.main.ScreenToWorldPoint (pos);
						target.z = cardInsetOnSelect;
					}
				}

				hand [i].cardObject.transform.position = Vector3.Lerp (hand [i].cardObject.transform.position, target, Time.deltaTime * cardLerpSpeed);

				float r = 0;

				///Rotation
				if (!hand [i].cardObject.isDragged)
				{
					if (x != 0f)
						r = (cardAreaTotalRotation * x);

					r = Mathf.LerpAngle (hand [i].cardObject.transform.localRotation.eulerAngles.z, r, Time.deltaTime * cardLerpSpeed);
				
					hand [i].cardObject.transform.localRotation = Quaternion.Euler (0, 0, r);
				}
			}
		}
	}

    /// <summary>
    /// Show the automatically suggested-to-play cards
    /// </summary>
	public void DisplaySuggestedPlay ()
	{
		if (currentState == GameState.TRICK)
		{
			if (gamepadManager.lastPlayKind != PlayKind.NONE)
			{
				List<Card> suggestedPlay = CardCombinationDetector.GetPlay (hand, gamepadManager.lastPlayKind, gamepadManager.lastValue);

				if (suggestedPlay != null)
				{
					Debug.Log ("So, the suggested play is not null!");

					foreach (CardObjectGamepad card in play)
					{
						play.Remove (card);
						gamepadManager.playSlot.Remove (card);
						card.isSelected = false;
						card.cardImage.material.SetFloat ("_Selected", 0);
						card.glow.enabled = false;
					}

					play.Clear ();
					gamepadManager.playSlot.Clear ();

					for (int i = 0; i < hand.Count; i++)
					{
						hand [i].cardObject.isSelected = false;
					}

					for (int i = 0; i < suggestedPlay.Count; i++)
					{
						suggestedPlay [i].cardObject.isSelected = true;
						play.Add (suggestedPlay [i].cardObject);
						gamepadManager.playSlot.Add (suggestedPlay [i].cardObject);
						suggestedPlay [i].cardObject.cardImage.material.SetFloat ("_Selected", 1);
						CardObjectGamepad temp = suggestedPlay [i].cardObject as CardObjectGamepad;
						temp.glow.enabled = true;
					}
				}
			}
		}
	}

    /// <summary>
    /// Show the trade-selected cards
    /// </summary>
	private void DisplayTrade ()
	{
		for (int i = 0; i < gamepadManager.tradeSlot.Length; i++)
		{
			if (gamepadManager.tradeSlot [i] != null)
			{
				if (!gamepadManager.tradeSlot [i].isDragged)
				{
					Vector3 target = tradeSlotPosition [i].position;
					gamepadManager.tradeSlot [i].transform.position = Vector3.Lerp (gamepadManager.tradeSlot [i].transform.position, target, Time.deltaTime * cardLerpSpeed);
				}
			}
		}
	}

    /// <summary>
    /// Places the card for trade/play
    /// </summary>
    /// <param name="card">target Card object</param>
	public void PlaceCard (CardObject card)
	{
		card.isSelected = false;
		card.isHovering = false;
		card.isDragged = false;

		hand.Remove (card);
		play.Remove (card);
		for (int i = 0; i < gamepadManager.tradeSlot.Length; i++)
		{
			if (gamepadManager.tradeSlot [i] == card)
			{
				gamepadManager.tradeSlot [i] = null;
			}
		}

		switch ((int)card.slotId)
		{
			case -2:
				play.Add (card);
				gamepadManager.playSlot = play;
				break;
			case -1:
				hand.Add (card.card);
				break;
			case 0:
				if (gamepadManager.tradeSlot [0] != null)
				{
					gamepadManager.tradeSlot [0].slotId = SlotID.HAND;
					hand.Insert (card.handPos, gamepadManager.tradeSlot [0]);
				}
				gamepadManager.tradeSlot[0] = card;
				break;
			case 1:
				if (gamepadManager.tradeSlot [1] != null)
				{
					gamepadManager.tradeSlot [1].slotId = SlotID.HAND;
					hand.Insert (card.handPos, gamepadManager.tradeSlot [1]);
				}
				gamepadManager.tradeSlot[1] = card;
				break;
			case 2:
				if (gamepadManager.tradeSlot [2] != null)
				{
					gamepadManager.tradeSlot [2].slotId = SlotID.HAND;
					hand.Insert (card.handPos, gamepadManager.tradeSlot [2]);
				}
				gamepadManager.tradeSlot[2] = card;
				break;
			default:
				hand.Add (card.card);
				break;
		}

		for (int i = 0; i < hand.Count; i++)
		{
			if (hand [i].cardObject != null)
			{
				hand [i].cardObject.handPos = i;
			}
		}
	}

    [System.Obsolete("Unused")]
    public void DisplayNotYourTurn (bool active)
	{
		if (notYourTurnPanel.activeSelf != active)
		{
			notYourTurnPanel.SetActive (active);
		}
	}

    /// <summary>
    /// Plays the selected cards. Called by UI Button.
    /// </summary>
	public void Play ()
	{
		Debug.Log ("Play");

		gamepadManager.SetReady ();
	}

    /// <summary>
    /// Skip current turn. Called by UI Button.
    /// </summary>
	public void Pass ()
	{
		Debug.Log ("Pass");

		gamepadManager.PassButton ();

		foreach (CardObjectGamepad card in play)
		{
			play.Remove (card);
			gamepadManager.playSlot.Remove (card);
			card.isSelected = false;
			card.cardImage.material.SetFloat ("_Selected", 0);
			card.glow.enabled = false;
		}
	}

    /// <summary>
    /// Call Tichu. Called by UI Button
    /// </summary>
	public void Tichu ()
	{
		gamepadManager.CallTichu ();
		canCall = false;
		tichuButton.interactable = false;
	}

    [System.Obsolete("Unused")]
    public void Pause ()
	{
		Debug.Log ("Pause");
	}

    [System.Obsolete("Unused")]
    public void Language (float value)
	{
		Debug.LogFormat ("Language {0}", value);
	}

    /// <summary>
    /// Change the game's current state in UI
    /// </summary>
    /// <param name="state">new State</param>
	public void StateSwitch (GameState state)
	{
		Debug.Log ("Gamepad ~ State Switch");

		tichuButton.GetComponent<TichuButtonHandler> ().SetTichu (state);

		currentState = state;
		if (currentState == GameState.ROUND_START)
			canCall = true;
		if (currentState == GameState.ROUND_RESULT)
			canCall = false;
		//if (currentState == GameState.FIRST_DEAL || currentState == GameState.SECOND_DEAL)
		tichuButton.interactable = canCall;
	}
}