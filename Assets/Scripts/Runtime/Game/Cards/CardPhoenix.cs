using UnityEngine;
using System.Collections;

[System.Serializable]
public class CardPhoenix : Card
{
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