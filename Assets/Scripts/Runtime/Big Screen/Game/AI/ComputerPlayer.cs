using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

/// <summary>
/// Computer Player script, common basics for the AI
/// </summary>
public class ComputerPlayer : PlayerHandler
{
	/// <summary>
	/// The manager.
	/// </summary>
	[HideInInspector]public GameManager manager;
	[Header("AI Settings")]
	/// <summary>
	/// The ordered hand.
	/// </summary>
	public List<Card> orderedHand = new List<Card>();
	/// <summary>
	/// The great grand tichu chance.
	/// </summary>
	[Range(0,1)] public float greatGrandTichuChance = .001f;
	/// <summary>
	/// The first deal max value of cards possible, used by grand tichu chance.
	/// </summary>
	public int firstDealMaxVal = 300;
	/// <summary>
	/// The second deal max value of cards possible, used by tichu chance.
	/// </summary>
	public int secondDealMaxVal = 350;
	/// <summary>
	/// The think time. Artificial time so players can see what the AI has placed down before the other AI spams another play on top of it. You know, user friendliness.
	/// </summary>
	public float thinkTime = 1f;

	/// <summary>
	/// The Mahjing wish value.
	/// </summary>
	public static int wishValue = -1;

	/// <summary>
	/// The friend player identifier.
	/// </summary>
	private int friendId;
	/// <summary>
	/// Is the AI thinking?.
	/// </summary>
	private bool isThinking = false;

	/// <summary>
	/// Is it my turn?
	/// </summary>
	public bool isMyTurn;

	/// <summary>
	/// The trade slots.
	/// </summary>
	[HideInInspector]public Card[] tradeSlots = new Card[3];

	/// <summary>
	/// The ai module.
	/// </summary>
	[HideInInspector] public AIModuleBase aiModule;

	/// <summary>
	/// Set up the Computer Player
	/// </summary>
	private void Start()
	{
		manager = GameManager.main;
		friendId = (playerId + 2) % 4;
	}

	/// <summary>
	/// State Machine of the AI
	/// </summary>
	private void Update()
	{
		switch (manager.currentState)
		{
			case GameState.ROUND_START:
				goto case GameState.FIRST_DEAL;
			case GameState.FIRST_DEAL:
				if (!isReady)
				{
					hasTraded = false;
					if (hand.Count > 0)
						orderedHand = CombinationHandler.OrderCards(hand);
					CallTichu();
					isReady = true;
					isThinking = false;
				}
				break;
			case GameState.TRADING:
				if (!isReady)
				{
					CallTichu();
					orderedHand = CombinationHandler.OrderCards(hand);
					aiModule.SetupTrade (this, friendId);
					isReady = true;
				}
				break;
			case GameState.SCOREBOARD:
				isReady = true;
				wantedTichu = TichuType.NONE;
				tichuType = TichuType.NONE;
				break;
			case GameState.TRICK:
				{
					if (tichuType == TichuType.NONE)
					{
						if (manager.players [friendId].tichuType == TichuType.NONE)
						{
							if (wantedTichu != TichuType.NONE)
							{
								tichuType = wantedTichu;
								displayTichu.Display (tichuType);
							}
						}
					}
				}
				break;
		}
	}

	/// <summary>
	/// Perform the AI's Trick actions.
	/// </summary>
	public void DoTrick()
	{
		//Debug.Log(playerId + " doing trick. " + manager.MostPlayersOut());
		if (hasTraded && !manager.MostPlayersOut())
		{
			//Debug.Log(playerId + " keep going");
			//Debug.Log((manager.leaderId + manager.currentTurn) % 4 + " @ " + playerId + " ... " + isThinking);
			if ((manager.leaderId + manager.currentTurn) % 4 == playerId && !isThinking)
			{
				//Debug.Log(playerId + " almost there");
				isMyTurn = true;
				isThinking = true;
				if (hand.Count > 0)
					StartCoroutine(PerformTurn());
				if (hand.Count == 0)
					isThinking = false;
				isMyTurn = false;
			}
		}

		if (hand.Count == 0 && !isThinking && !manager.MostPlayersOut() && (manager.leaderId + manager.currentTurn) % 4 == playerId)
		{
			isThinking = true;
			NetworkEmulator.main.SendDataToHost("E" + playerId);
			isReady = true;
			NetworkEmulator.main.SendDataToHost("R" + playerId + "p");
			NetworkEmulator.main.SendDataToHost("P" + playerId + "+");
			isThinking = false;
		}
	}

	/// <summary>
	/// Performs the AI's turn.
	/// </summary>
	private IEnumerator PerformTurn()
	{
		orderedHand = CombinationHandler.OrderCards(hand);
		yield return new WaitForSeconds(thinkTime);

		//Debug.Log(playerId + " ==? " + (manager.leaderId + manager.currentTurn) % 4);
		if ((manager.leaderId + manager.currentTurn) % 4 == playerId && manager.currentState == GameState.TRICK)
		{

			GetPossibleCombinations();

			List<Card> wishCards = new List<Card>();

			if (wishValue >= 0)
			{
				foreach (Card c in hand)
				{
					if (c.value == wishValue)
						wishCards.Add(c);
				}
			}

			PlayKind kind;
			bool earlyPass = false;
			aiModule.PerformTurn(this, out kind, out earlyPass, wishCards);

			if (playCards == null || playCards.Count == 0 || earlyPass)
			{
				Pass();
				//Debug.Log("AI @ " + playerId + " passed");
			}
			else
			{
				//Debug.Log("AI @ " + playerId + " played");
				string message = "c" + playerId.ToString() + ((int)kind).ToString();
				string debug = "";
				foreach (Card card in playCards)
				{
					message += (char)(card.id);
					debug += card.name + ", ";
				}
				NetworkEmulator.main.SendDataToHost(message);

				isReady = true;
				NetworkEmulator.main.SendDataToHost("R" + playerId + "+");
				NetworkEmulator.main.SendDataToHost("P" + playerId + "c");

				if (hand.Count <= 0)
					NetworkEmulator.main.SendDataToHost ("E" + playerId);
			}
		}
		isThinking = false;
	}

	public List<Card> playCards = new List<Card>();

	public List<CardCombination> pairCombos;
	public List<CardCombination> doublePairCombos;
	public List<CardCombination> threeOfAKindCombos;
	public List<CardCombination> fullHouseCombos;
	public List<CardCombination> straightCombos;
	public List<CardCombination> bombCombos;

	/// <summary>
	/// Checks if the parameter card is not a special card
	/// </summary>
	/// <param name="card">The card to check</param>
	/// <returns>Returns true if the card is not special</returns>
	private bool IsRegularCard(Card card)
	{
		if (card.GetType() == typeof(CardDog) || card.GetType() == typeof(CardDragon) || card.GetType() == typeof(CardMahJong) || card.GetType() == typeof(CardPhoenix))
			return false;
		return true;
	}

	/// <summary>
	/// Generates the possible combinations the AI has.
	/// </summary>
	private void GetPossibleCombinations()
	{
		pairCombos = new List<CardCombination>();
		//Check for pairs
		for(int i = 1; i < orderedHand.Count; i++)
		{
			if(orderedHand[i].value == orderedHand[i-1].value && IsRegularCard(orderedHand[i]) && IsRegularCard(orderedHand[i-1]))
			{
				CardCombination cc = new CardCombination();
				cc.combination.Add(orderedHand[i]);
				cc.combination.Add(orderedHand[i-1]);
				pairCombos.Add(cc);
			}
		}

		doublePairCombos = new List<CardCombination>();
		//Check for double pairs
		for(int i = 1; i < orderedHand.Count-2; i++)
		{
			if(orderedHand[i].value == orderedHand[i-1].value && orderedHand[i + 2].value == orderedHand[i + 1].value)
			{
				if (orderedHand[i].value == orderedHand[i+1].value-1)
				{
					CardCombination cc = new CardCombination();
					for(int j = 0; j < 4; j++)
					{
						cc.combination.Add(orderedHand[(i - 1) + j]);
					}
					bool isValid = true;
					foreach(Card card in cc.combination)
					{
						if (!IsRegularCard(card))
							isValid = false;
					}

					if(isValid)
						doublePairCombos.Add(cc);
				}
			}
		}

		threeOfAKindCombos = new List<CardCombination>();
		//Check for Three-of-a-kinds
		for(int i = 2; i < orderedHand.Count; i++)
		{
			if(orderedHand[i].value == orderedHand[i-1].value && orderedHand[i].value == orderedHand[i-2].value)
			{
				CardCombination cc = new CardCombination();
				for(int j = -2; j <= 0; j++)
				{
					if(IsRegularCard(orderedHand[i+j]))
						cc.combination.Add(orderedHand[i + j]);
				}

				if(cc.combination.Count == 3)
					threeOfAKindCombos.Add(cc);
			}
		}

		fullHouseCombos = new List<CardCombination>();
		//Check for full-houses
		foreach(CardCombination pair in pairCombos)
		{
			foreach(CardCombination toak in threeOfAKindCombos)
			{
				CardCombination cc = new CardCombination();
				float value = 0;
				bool valid = true;
				foreach (Card card in pair.combination)
				{
					cc.combination.Add(card);
					value = card.value;
				}
				foreach (Card card in toak.combination)
				{
					if(card.value == value)
					{
						valid = false;
						break;
					}
					cc.combination.Add(card);
				}
				if(valid)
					fullHouseCombos.Add(cc);
			}
		}

		bombCombos = new List<CardCombination>();
		straightCombos = new List<CardCombination>();
		//Check for straights/straight flushes
		for(int i = 0; i < orderedHand.Count-5; i++)
		{
			int j = 0;
			float firstValue = 0;
			CardType firstType = CardType.SPECIAL;
			CardCombination cc = new CardCombination();
			bool isBomb = true;

			while((j+i) < orderedHand.Count)
			{
				if (j == 0)
				{
					firstValue = orderedHand[j + i].value;
					firstType = orderedHand[j + i].cardType;
					cc.combination.Add(orderedHand[j + i]);
				}
				else if (orderedHand[i + j].value == firstValue + j)
				{
					cc.combination.Add(orderedHand[j + i]);

					if (orderedHand[i + j].cardType != firstType)
						isBomb = false;

					if (cc.combination.Count >= 5)
					{
						if (isBomb)
							bombCombos.Add(cc);
						else
							straightCombos.Add(cc);

						List<Card> oldCombo = new List<Card>(cc.combination);
						cc = new CardCombination();
						cc.combination = oldCombo;
					}
				}
				else
					break;

				j++;
			}
		}

		//Check for bombs
		for(int i = 0; i < orderedHand.Count-4; i++)
		{
			float firstValue = 0;
			CardCombination cc = new CardCombination();
			for(int j = 0; j < 4; j++)
			{
				if(j == 0)
				{
					firstValue = orderedHand[i + j].value;
					cc.combination.Add(orderedHand[i + j]);
				}
				else if(orderedHand[i+j].value == firstValue)
				{
					cc.combination.Add(orderedHand[i + j]);
					if (cc.combination.Count >= 4)
						bombCombos.Add(cc);
				}
			}
		}
	}

	//Makes the AI skip a turn
	public void Pass()
	{
		isReady = true;
		NetworkEmulator.main.SendDataToHost("R" + playerId + "p");
		NetworkEmulator.main.SendDataToHost("P" + playerId + "+");
	}

	TichuType wantedTichu = TichuType.NONE;

	/// <summary>
	/// Causes the AI to call Tichu
	/// </summary>
	private void CallTichu()
	{
		float chance = 0;
		int handValue = HandValue();
		switch(manager.currentState)
		{
			case GameState.ROUND_START:
				chance = greatGrandTichuChance;
				if (UnityEngine.Random.value < chance)
					wantedTichu = TichuType.GRAND_GRAND_TICHU;
				break;
			case GameState.FIRST_DEAL:
				chance = (float)handValue / (float)firstDealMaxVal;
				if (UnityEngine.Random.value < chance)
					wantedTichu = TichuType.GRAND_TICHU;
				break;
			case GameState.TRADING:
				chance = (float)handValue / (float)secondDealMaxVal;
				if (UnityEngine.Random.value < chance)
					wantedTichu = TichuType.TICHU;
				break;
		}
	}

	/// <summary>
	/// Sets up the AI's trade action
	/// </summary>
	private void SetupTrade()
	{
		int slotId = 0;
		for(int i = 0; i < 4; i++)
		{
			if(i != playerId)
			{
				if (i == friendId)
					tradeSlots[slotId] = orderedHand[orderedHand.Count - 1];
				else
					tradeSlots[slotId] = orderedHand[slotId];
				slotId++;
			}
		}
	}

	/*private void PerformTrade()
    {
        int slotId = 0;
        for(int i = 0; i < 4; i++)
        {
            if(i != playerId)
            {
                manager.players[i].hand.Add(tradeSlots[slotId]);
                hand.Remove(tradeSlots[slotId]);
                slotId++;
            }
        }
    }*/

	/// <summary>
	/// Performs the AI's trade action. Must be setup first using SetupTrade()
	/// </summary>
	public void PerformTrade()
	{
		int slotId = 0;

		string msg = "t" + playerId.ToString();

		for(int i = 0; i < 4; i++)
		{
			if(i != playerId)
			{
				int targetPlayer = i;
				int cardId = tradeSlots[slotId].id;

				msg += targetPlayer.ToString() + ((char) cardId).ToString();

				slotId++;
			}
		}

		NetworkEmulator.main.SendDataToHost(msg);
		//hasTraded = true;
	}

	/// <summary>
	/// Returns the total value of the AI's hand
	/// </summary>
	/// <returns>The total value of the AI's hand</returns>
	private int HandValue()
	{
		int val = 0;
		for(int i = 0; i < orderedHand.Count; i++)
		{
			Card card = orderedHand[i];

			val += (int)card.value;
			val += card.scoreValue;

			if(i > 0)
			{
				if(orderedHand[i-1].value == card.value-1)
					val += (int)Mathf.Floor(card.value * .5f);
				if (orderedHand[i - 1].cardType == card.cardType)
					val += (int)Mathf.Floor(card.value * .25f);
			}
			if (i < orderedHand.Count-1)
			{
				if (orderedHand[i + 1].value == card.value + 1)
					val += (int)Mathf.Floor(card.value * .5f);
				if (orderedHand[i + 1].cardType == card.cardType)
					val += (int)Mathf.Floor(card.value * .25f);
			}

			if(i < orderedHand.Count-3 && orderedHand.Count > 4)
			{
				bool bomb = true;
				for(int j = 0; j < 4; j++)
				{
					if (orderedHand[i + j].cardType != orderedHand[i].cardType)
						bomb = false;
				}
				if (bomb)
					val += 25;
			}

			if (i < orderedHand.Count - 4 && orderedHand.Count > 5 && i > 4)
			{
				bool straight = true;
				for (int j = 0; j < 5; j++)
				{
					if (orderedHand[i + j].value-1 != orderedHand[i+(j-1)].value)
						straight = false;
				}
				if (straight)
					val += 10;
			}
		}
		return val;
	}
}