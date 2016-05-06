using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class CardCombinationDetector 
{
	public static List<Card> orderedHand = new List<Card>();

	public static List<Card> playCards = new List<Card>();

	public static List<CardCombination> pairCombos;
	public static List<CardCombination> doublePairCombos;
	public static List<CardCombination> threeOfAKindCombos;
	public static List<CardCombination> fullHouseCombos;
	public static List<CardCombination> straightCombos;
	public static List<CardCombination> bombCombos;

	public static List<CardCombination> combos;

	/// <summary>
	/// Gets the play.
	/// </summary>
	/// <returns>The play.</returns>
	/// <param name="hand">Hand.</param>
	/// <param name="playKind">Play kind.</param>
	/// <param name="lastValue">Last value.</param>
	/// <description>
	/// 
	/// </description>
	public static List<Card> GetPlay (List<Card> hand, PlayKind playKind, float lastValue)
	{
		GetPossibleCombinations (hand);

		Debug.Log ("Hand " + hand.Count + ", playkind " + playKind + ", last value " + lastValue);

		if (playKind != PlayKind.SINGLE_PLAY)
		{
			switch (playKind)
			{
				case PlayKind.ONE_PAIR:
					combos = pairCombos;
					break;
				case PlayKind.STAIRS:
					combos = doublePairCombos;
					break;
				case PlayKind.THREE_OF_A_KIND:
					combos = threeOfAKindCombos;
					break;
				case PlayKind.STRAIGHT:
					combos = straightCombos;
					break;
				case PlayKind.BOMB:
					combos = bombCombos;
					break;
			}

			if (combos.Count > 0)
			{
				for (int i = 0; i < combos.Count; i++)
				{
					if (CombinationHandler.IsHigher (combos [i].combination, lastValue))
						return combos [i].combination;
				}
			}
			else
				return null;
		}
		else
		{
			for (int i = 0; i < orderedHand.Count; i++)
			{
				if (orderedHand [i].value > lastValue && lastValue != 15)
					return new List<Card> { orderedHand [i] };
			}
		}

		return null;
	}

	private static bool IsRegularCard(Card card)
	{
		if (card.GetType() == typeof(CardDog) || card.GetType() == typeof(CardDragon) || card.GetType() == typeof(CardMahJong) || card.GetType() == typeof(CardPhoenix))
			return false;
		return true;
	}

	private static void GetPossibleCombinations(List<Card> hand)
	{
		orderedHand = CombinationHandler.OrderCards(hand);

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
}