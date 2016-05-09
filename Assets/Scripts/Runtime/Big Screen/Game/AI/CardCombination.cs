using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Combination of Cards, as used by the AI
/// </summary>
[System.Serializable]
public class CardCombination
{
	/// <summary>
	/// The combination of cards.
	/// </summary>
    public List<Card> combination = new List<Card>();
    public int combinedValue
    {
        get
        {
            int val = 0;
            foreach(Card card in combination)
            {
                val += (int)card.value;
            }
            return val;
        }
    }
}