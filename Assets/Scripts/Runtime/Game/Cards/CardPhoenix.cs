using UnityEngine;
using System.Collections;

/// <summary>
/// Phoenix' special card
/// </summary>
[System.Serializable]
public class CardPhoenix : Card
{

    /// <summary>
    /// Called when the card has been placed
    /// </summary>
    /// <param name="playerId">ID of the player that places the card</param>
    public override void OnPlacement(int playerId)
    {
        SpecialCardEffectHandler.PhoenixCard();

        if (Manager.main.lastCard == null)
            value = 1.5f;
        else
        {
            if(Manager.main.lastPlayKind == PlayKind.SINGLE_PLAY)
                value = Manager.main.lastCard.value + .5f;
            else if (Manager.main.lastPlayKind == PlayKind.NONE)
                value = 1.5f;
            else
            {
                float targetValue = 99999;
                foreach(Card card in Manager.main.lastPlay)
                {
                    if(card.GetType() != typeof(CardPhoenix))
                        targetValue = Mathf.Min(targetValue, card.value);//Verander dit in popup waar je kunt kierzen als welke kaart hij telt
                }
                if (Manager.main.lastPlayKind == PlayKind.STRAIGHT)
                    targetValue++;
            }

            if (value < 1)
                value += 1;
        }
    }
}