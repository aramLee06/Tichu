using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Default deck generation class
/// </summary>
public static class EditorGenerateCards
{
    /// <summary>
    /// Generates the default, unshuffled deck
    /// </summary>
    /// <returns>Array of cards; the deck.</returns>
    public static Card[] GenerateCards()
    {
        int types = 4;
        int values = 13;

        Card[] regularCards = new Card[types * values];

        for (int i = 0; i < types; i++)
        {
            for (int j = 0; j < values; j++)
            {
                Card newCard = new Card();
                newCard.cardType = (CardType)i;
                newCard.value = j;

                string valueName = (j + 1).ToString();

                if (j == 10)
                    valueName = "JACK";
                else if (j == 11)
                    valueName = "QUEEN";
                else if (j == 12)
                    valueName = "KING";
                else if (j == 0)
                    valueName = "ACE";

                newCard.name = newCard.cardType.ToString() + " " + valueName;

                if (j == 9 || j == 12)
                    newCard.scoreValue = 10;
                else if (j == 4)
                    newCard.scoreValue = 5;

                if (j == 0)
                    newCard.value = 13;

                regularCards[(i * values) + j] = newCard;
            }
        }

        return regularCards;
    }
}