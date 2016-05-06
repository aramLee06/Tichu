using UnityEngine;
using System.Collections;

[System.Serializable]
public class CardDragon : Card
{
    private int player;

    public override void OnPlacement(int playerId)
    {
        SpecialCardEffectHandler.DragonCard();

        player = playerId;
    }

    public override void OnTrickEnd()
    {
        if(Manager.main.GetWinPlayer() == player && Manager.main.lastCard.GetType() == typeof(CardDragon))
        {
            int[] choices = new int[2];
            int teamNumber = Manager.main.teamNumbersPerPlayer[player];
            int j = 0;
            for(int i = 0; i < Manager.main.teamNumbersPerPlayer.Length; i++)
            {
                if(Manager.main.teamNumbersPerPlayer[i] != teamNumber)
                {
                    choices[j] = i;
                    j++;
                }
            }

            //int selectedPlayer = 0;
            //Popup a screen that lets you select a player from the opposing team here.
            //Debug.Log("At this point we'd deal with the dragon card. For now, set it in the debug variable.");
            //Debug.Break();
            //selectedPlayer = debug_targetPlayer;

            //Manager.main.ChangeWinPlayer(selectedPlayer);
            Manager.main.HandleDragon(); //Added as of 2016.03.30
        }
    }
}