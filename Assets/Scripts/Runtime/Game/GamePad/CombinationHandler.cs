using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// A class that checks whether a selected combination of cards is valid for play or not.
/// </summary>
public static class CombinationHandler
{
	/// <summary>
	/// The mahjong wish value.
	/// </summary>
	public static int wishValue = -1;

	//private static bool canPerformWish = false;
	/// <summary>
	/// The hand.
	/// </summary>
	private static List<Card> hand;
	/// <summary>
	/// The wish cards.
	/// </summary>
	private static List<Card> wishCards = new List<Card>();

	/// <summary>
	/// Checks whether the inserted combination is valid for play.
	/// </summary>
	/// <param name="cardCombination">The inserted combination</param>
	/// <param name="playKind">The kind of play the combo is</param>
	/// <param name="lastValue">The last value played (0 if none)</param>
	/// <param name="lastPlayKind">The last kind of play</param>
	/// <param name="orderedCards">The ordered combination (from low to high)</param>
	/// <param name="lastAmountOfCards">The last play's amount of cards</param>
	/// <param name="hand">The player's current hand</param>
	/// <param name="highValue">The last play's highest card's value</param>
	/// <param name="lowValue">The last play's lowest card's value</param>
	/// <returns>Is the combination valid?</returns>
	public static bool CheckCombination(List<Card> cardCombination, ref PlayKind playKind, float lastValue, PlayKind lastPlayKind, out List<Card> orderedCards, int lastAmountOfCards, List<Card> hand, float highValue, float lowValue)
	{
		if (cardCombination != null && cardCombination.Count > 0)
		{
			if (wishValue != -1)
			{
				foreach (Card card in hand)
				{
					if (card.value == wishValue)
					{
						//canPerformWish = true;
						CombinationHandler.hand = hand;
						wishCards.Add(card);
					}
				}
			}

			orderedCards = OrderCards(cardCombination);
			bool canPlay = false;
			bool isBomb = false;
			bool wishcardPlay = CanPlayWishCard(wishCards, playKind, lastValue, lastPlayKind, orderedCards, lastAmountOfCards, hand, lowValue, highValue);

			if (lastPlayKind == PlayKind.NONE)
				lastValue = -1;

			switch(lastPlayKind)
			{
				case PlayKind.NONE:
					if(ValidSingleCard(orderedCards,lastValue))
					{
						canPlay = true;
						playKind = PlayKind.SINGLE_PLAY;
					}
					if(ValidPairs(orderedCards,lastValue,lastAmountOfCards))
					{
						canPlay = true;
						playKind = PlayKind.STAIRS;
					}
					if(ValidThreeOfAKind(orderedCards, lastValue))
					{
						canPlay = true;
						playKind = PlayKind.THREE_OF_A_KIND;
					}
					if(ValidFullHouse(orderedCards,lastValue))
					{
						canPlay = true;
						playKind = PlayKind.FULL_HOUSE;
					}
					if(ValidStraight(orderedCards,lastValue,out isBomb, lastPlayKind,lastAmountOfCards))
					{
						canPlay = true;
						playKind = PlayKind.STRAIGHT;
					}
					break;
				case PlayKind.SINGLE_PLAY:
					canPlay = ValidSingleCard(orderedCards, lastValue);
					playKind = PlayKind.SINGLE_PLAY;
					break;
				case PlayKind.STAIRS:
					goto case PlayKind.ONE_PAIR;
				case PlayKind.ONE_PAIR:
					canPlay = ValidPairs(orderedCards, lastValue,lastAmountOfCards);
					playKind = PlayKind.ONE_PAIR;
					break;
				case PlayKind.THREE_OF_A_KIND:
					canPlay = ValidThreeOfAKind(orderedCards, lastValue);
					playKind = PlayKind.THREE_OF_A_KIND;
					break;
				case PlayKind.FULL_HOUSE:
					canPlay = ValidFullHouse(orderedCards, lastValue);
					playKind = PlayKind.FULL_HOUSE;
					break;
				case PlayKind.STRAIGHT:
					canPlay = ValidStraight(orderedCards, lastValue, out isBomb, lastPlayKind, lastAmountOfCards);
					playKind = PlayKind.STRAIGHT;
					break;
			}

			if(ValidBomb(orderedCards,lastValue,lastPlayKind))
			{
				canPlay = true;
				playKind = PlayKind.BOMB;
			}
			if(wishcardPlay)
			{
				bool inThere = false;
				foreach(Card card in orderedCards)
				{
					if (card.value == wishValue)
					{
						inThere = true;
						//wishValue = -1;
						//NetworkEmulator.main.SendDataToHost("J" + Manager.main.playerNumber + "X");
						//NetworkEmulator.main.SendDataToHost(">" + Manager.main.playerNumber + "Wishvalue has been reset!");
						break;
					}
				}

				canPlay = canPlay && inThere;
			}

			return canPlay;
		}

		orderedCards = null;
		return false;
	}

	/// <summary>
	/// Additional Play card function
	/// </summary>
	/// <param name="cards"></param>
	public static void PlayCards(List<Card> cards)
	{
		bool containsWishValue = false;

		foreach(Card c in cards)
		{
			if(c.value == wishValue)
			{
				containsWishValue = true;
				break;
			}
		}

		if(containsWishValue)
		{
			wishValue = -1;
			NetworkEmulator.main.SendDataToHost("J" + Manager.main.playerNumber + "X");
			NetworkEmulator.main.SendDataToHost(">" + Manager.main.playerNumber + "Wishvalue has been reset!");
		}
	}

	/// <summary>
	/// Checks whether a mahjong's wished card can be played
	/// </summary>
	/// <param name="wishCards">The list of valid wish cards in hand</param>
	/// <param name="playKind">Kind of play</param>
	/// <param name="lastValue">Last played total value</param>
	/// <param name="lastPlayKind">Last kind of play</param>
	/// <param name="orderedCards">Ordered hand</param>
	/// <param name="lastAmountOfCards"></param>
	/// <param name="hand">The player's hand</param>
	/// <param name="lowVal">Last play's lowest card value</param>
	/// <param name="highVal">Last play's highest card value</param>
	/// <returns>Whether the player can play his wish card (eventually forcing him to do so)</returns>
	public static bool CanPlayWishCard(List<Card> wishCards, PlayKind playKind, float lastValue, PlayKind lastPlayKind, List<Card> orderedCards, int lastAmountOfCards, List<Card> hand, float lowVal, float highVal)
	{
		if (wishValue == -1)
			return false;

		Debug.LogWarning("<color=#194>Wishcard value: " + wishValue + " against " + lastValue + "</color>");

		foreach (Card wishCard in wishCards)
		{
			switch (lastPlayKind)
			{
				case PlayKind.NONE:
					return true;
				case PlayKind.SINGLE_PLAY:
					if (wishCard.value > lastValue)
						return true;
					break;
				case PlayKind.ONE_PAIR:
					if(wishCards.Count >= 2)
					{
						if (wishCard.value <= lowVal)
							break;

						int minVal = (int)(lowVal + 1);
						int cards = 0;
						int originalAmount = 0;

						while(cards < lastAmountOfCards)
						{
							foreach(Card card in hand)
							{
								if(card.value == minVal)
								{
									cards++;
									if (cards % 2 == 0)
										minVal++;
								}
							}
							if (cards == originalAmount)
							{
								minVal++;
								originalAmount = cards;
							}
							if (minVal >= 14)
								break;
						}

						if (cards == lastAmountOfCards)
							return true;
					}
					break;
				case PlayKind.THREE_OF_A_KIND:
					if (wishCards.Count >= 3 && wishCard.value*3 > lastValue)
						return true;
					break;
				case PlayKind.STRAIGHT:
					{
						int ignoredVariable;
						GotWishStraight(lowVal, highVal, wishCard, lastAmountOfCards, out ignoredVariable);
					}
					break;
				case PlayKind.FULL_HOUSE:
					{
						if (wishCards.Count == 3 || wishCards.Count == 2)
						{
							if (wishCard.value * 2 > lastValue)
								return true;

							if (wishCard.value * 3 > lastValue && wishCards.Count == 3)
								return true;

							if (wishCards.Count == 2)
							{
								for (int targetVal = 1; targetVal <= 13; targetVal++)
								{
									if (targetVal != wishCard.value)
									{
										if (targetVal * 3 + wishCard.value * 2 > lastValue)
										{
											int cards = 0;
											foreach (Card card in hand)
											{
												if (card.value == targetVal)
													cards++;
											}
											if (cards == 3)
												return true;
										}
									}
								}
							}

							if (wishCards.Count == 3)
							{
								for (int targetVal = 1; targetVal <= 13; targetVal++)
								{
									if (targetVal != wishCard.value)
									{
										if (targetVal * 2 + wishCard.value * 3 > lastValue)
										{
											int cards = 0;
											foreach (Card card in hand)
											{
												if (card.value == targetVal)
													cards++;
											}
											if (cards == 3)
												return true;
										}
									}
								}
							}
						}
					}
					break;
				case PlayKind.BOMB:
					{
						if (wishCards.Count == 4)
							return true;

						int lowValue = -1;
						if(GotWishStraight(lowVal,highVal,wishCard,lastAmountOfCards,out lowValue))
						{
							if (lowValue == -1)
								break;
							List<Card> straight = new List<Card>();
							for(int i = 0; i < lastAmountOfCards; i++)
							{
								int val = lowValue;
								foreach(Card card in hand)
								{
									if(card.value == val)
									{
										straight.Add(card);
										val++;
									}
								}
							}

							bool fail = false;
							for(int i = 1; i < straight.Count; i++)
							{
								if (straight[i].cardType != straight[i - 1].cardType)
								{
									fail = true;
									break;
								}
							}
							if (!fail)
								return true;
						}
					}
					break;
			}
		}
		return false;
	}

	/// <summary>
	/// Checks whether the player has straights that contain wish cards
	/// </summary>
	/// <param name="lowVal">The last play's lowest card's value</param>
	/// <param name="highVal">The last play's highest card's value</param>
	/// <param name="wishCard">The selected wish card</param>
	/// <param name="lastAmountOfCards">Last number of played cards</param>
	/// <param name="lowestValue">The lowest value of the straight</param>
	/// <returns></returns>
	public static bool GotWishStraight(float lowVal, float highVal, Card wishCard, int lastAmountOfCards, out int lowestValue)
	{
		lowestValue = -1;
		int minVal = (int)lowVal + 1;
		int maxVal = (int)highVal + 1;

		if (wishCard.value >= minVal && wishCard.value <= maxVal)
		{
			int cards = 0;
			int targetValue = minVal;
			bool fail = false;

			while (cards < lastAmountOfCards)
			{
				foreach (Card card in hand)
				{
					if (targetValue > maxVal)
					{
						fail = true;
						break;
					}

					if (card.value == targetValue)
					{
						lowestValue = targetValue;
						targetValue++;
						cards++;
						break;
					}
					else
					{
						fail = true;
						break;
					}
				}
				if (fail)
					break;
			}

			if (cards == lastAmountOfCards)
				return true;
		}
		return false;
	}

	/// <summary>
	/// Orders the inserted cards from low to high
	/// </summary>
	/// <param name="cardCombination">The combination of cards to order</param>
	/// <returns>The ordered combination of cards</returns>
	public static List<Card> OrderCards(List<Card> cardCombination)
	{
		List<Card> orderedCards = new List<Card>();
		orderedCards.Add(cardCombination[0]);

		int cardAmount = cardCombination.Count;

		if (cardAmount > 1)
		{
			for (int i = 1; i < cardAmount; i++)
			{
				float value = cardCombination[i].value;

				int targetId = i;
				float lastValue = orderedCards[targetId - 1].value;

				while (value < lastValue)
				{
					targetId--;
					if (targetId == 0)
						break;

					lastValue = orderedCards[targetId - 1].value;
				}

				orderedCards.Insert(targetId, cardCombination[i]);
			}
		}
		return orderedCards;
	}

	/// <summary>
	/// Checks whether the card is a special card (dog, phoenix, dragon, mahjong...)
	/// </summary>
	/// <param name="card">The card to check</param>
	/// <returns></returns>
	private static bool IsSpecialPlayable(Card card)
	{
		return (card.GetType() == typeof(CardDragon) || card.GetType() == typeof(CardPhoenix));
	}

	/// <summary>
	/// Checks whether the card is a pheonix card
	/// </summary>
	/// <param name="card">Card to check</param>
	/// <returns></returns>
	private static bool IsPhoenix(Card card)
	{
		return card.GetType() == typeof(CardPhoenix);
	}

	/// <summary>
	/// Checks whether the inserted combo is higher than the inserted value
	/// </summary>
	/// <param name="combo">The combo to check</param>
	/// <param name="lastValue">The value to compare with</param>
	/// <returns>Combo's value > lastValue ?</returns>
	public static bool IsHigher(List<Card> combo, float lastValue)
	{
		float newValue = 0;

		for(int i = 0; i < combo.Count; i++)
		{
			Card card = combo[i];

			if (IsPhoenix(card))
			{
				if (i == 0)
					newValue += combo[i + 1].value - .5f;
				else
					newValue += combo[i - 1].value + .5f;
			}
			else
				newValue += card.value;
		}

		return newValue > lastValue;
	}

	/// <summary>
	/// Checks whether the combo is a valid-to-play single card
	/// </summary>
	/// <param name="combo">The "combo"</param>
	/// <param name="lastValue">The last played value</param>
	/// <returns></returns>
	private static bool ValidSingleCard(List<Card> combo, float lastValue)
	{
		if (combo.Count != 1)
			return false; //A single card doesn't mean multiple/none.

		if ((combo[0].value > lastValue || IsSpecialPlayable(combo[0])) && lastValue != 16)
		{
			return true; //Yes, it's that simple.
		}
		return false; //Else, not really.
	}

	/// <summary>
	/// Checks whether the combo is a valid-to-play pair
	/// </summary>
	/// <param name="combo">The combo to check</param>
	/// <param name="lastValue">Last play's value</param>
	/// <param name="amount">Amount of last played cards</param>
	/// <param name="isFullHouseCheck">Whether to use this function for the full house check, rather than an individual pairs check.</param>
	/// <returns></returns>
	private static bool ValidPairs(List<Card> combo, float lastValue, int amount, bool isFullHouseCheck = false)
	{
		if (combo.Count % 2 != 0 || (amount != 0 && combo.Count != amount) && !isFullHouseCheck)
			return false; //Pairs always come in.. Well, pairs. So no odd numbers.

		for(int i = 1; i < combo.Count; i += 2)
		{
			if (combo[i].value == 0 || combo[i-1].value == 0 || combo[i].value != combo[i - 1].value && !IsPhoenix(combo[i]) && !IsPhoenix(combo[i-1]))
				return false;
		}

		for(int i = 3; i < combo.Count; i += 2)
		{
			if (combo[i].value != (combo[i - 2].value + 1))
				return false;
		}

		return IsHigher(combo, lastValue);
	}

	/// <summary>
	/// Check whether the inserted combo is a valid-to-play three-of-a-kind. 
	/// </summary>
	/// <param name="combo">Combo to check</param>
	/// <param name="lastValue">The last play's value</param>
	/// <param name="isFullHouseCheck">Whether to use this function for the full house check, rather than an individual pairs check.</param>
	/// <returns></returns>
	private static bool ValidThreeOfAKind(List<Card> combo, float lastValue, bool isFullHouseCheck = false)
	{
		if (combo.Count != 3 && !isFullHouseCheck)
			return false; //Can't be more/less than 3 cards.

		float value = combo[0].value;

		if (IsPhoenix(combo[0]))
			value = combo[1].value;

		for (int i = 1; i < combo.Count; i++)
		{
			if (combo[i].value != value && !IsPhoenix(combo[i]))
				return false;
		}

		return IsHigher(combo, lastValue);
	}

	/// <summary>
	/// Check whether the combo is a valid-to-play full house
	/// </summary>
	/// <param name="combo">Combo to check</param>
	/// <param name="lastValue">The last play's value</param>
	/// <returns></returns>
	private static bool ValidFullHouse(List<Card> combo, float lastValue)
	{
		if (combo.Count != 5)
			return false; //Full house. So no free room left, and no overflow~

		bool returnValue = false;

		List<Card> threeCards = new List<Card>();
		List<Card> pairCards = new List<Card>();

		for (int i = 0; i < 3; i++)
		{
			threeCards.Add(combo[i]);
		}

		if(ValidThreeOfAKind(threeCards,-100,true))
		{
			pairCards.Add(combo[3]);
			pairCards.Add(combo[4]);
			if (ValidPairs(pairCards, -100, 2, true))
			{
				returnValue = true;
			}
		}
		else
		{
			threeCards.Clear();
			for (int i = 2; i < 5; i++)
			{
				threeCards.Add(combo[i]);
			}

			if(ValidThreeOfAKind(threeCards,-100,true))
			{
				pairCards.Add(combo[0]);
				pairCards.Add(combo[1]);
				if (ValidPairs(pairCards, -100, 2, true))
				{
					returnValue = true;
				}
			}
		}

		return returnValue && IsHigher(combo, lastValue);
	}

	/// <summary>
	/// Whether the combo is a valid-to-play straight
	/// </summary>
	/// <param name="combo">The combo to check</param>
	/// <param name="lastValue">Last play's value</param>
	/// <param name="isFlush">Whether the combo is a straight flush (bomb type combination)</param>
	/// <param name="lastPlayKind">Last play's kind</param>
	/// <param name="amount">Last play's amount of cards</param>
	/// <returns></returns>
	private static bool ValidStraight(List<Card> combo, float lastValue, out bool isFlush, PlayKind lastPlayKind, int amount)
	{
		isFlush = true; //Unless you're told otherwise.

		if (combo.Count < 5 || (amount != 0 && combo.Count != amount))
		{
			isFlush = false;
			Debug.Log ("This is not a flush because it's too small");
			return false; //Minimum of 5 cards.
		}

		for(int i = 1; i < combo.Count; i++)
		{
			if (combo [i - 1].value != (combo [i].value - 1) && !IsPhoenix (combo [i - 1]) && !IsPhoenix (combo [i]))
			{
				Debug.Log ("This is not a flush because it has a Phoenix in it");
				return false;
			}
			else
			{
				if (combo [i - 1].cardType != combo [i].cardType)
				{
					Debug.Log ("This is not a flush because it has cards of a different suit");
					isFlush = false;
				}
			}
		}
		return IsHigher(combo, lastValue) || (isFlush && lastPlayKind != PlayKind.BOMB);
	}

	/// <summary>
	/// Whether the combo is a valid-to-play bomb.
	/// </summary>
	/// <param name="combo">Combo to check</param>
	/// <param name="lastValue">The last play's value</param>
	/// <param name="lastPlayKind">The last play's kind</param>
	/// <returns></returns>
	private static bool ValidBomb(List<Card> combo, float lastValue, PlayKind lastPlayKind)
	{
		bool straightFlushBomb = false;
		bool validStraight = ValidStraight(combo, lastValue, out straightFlushBomb, lastPlayKind,0);
		straightFlushBomb = straightFlushBomb && validStraight;

		if (straightFlushBomb && combo[combo.Count - 1].value > lastValue)
			return true;

		if (combo.Count != 4)
			return false;

		for(int i = 1; i < 4; i++)
		{
			if (combo[i].value != combo[i - 1].value)
				return false;
		}

		return lastPlayKind != PlayKind.BOMB || IsHigher(combo, lastValue);
	}
}