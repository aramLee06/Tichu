using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class CardCombination
{
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