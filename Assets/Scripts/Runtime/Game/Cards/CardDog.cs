using UnityEngine;
using System.Collections;

[System.Serializable]
public class CardDog : Card
{
    public override void OnPlacement(int playerId)
    {
        float distance = 12;
        float angle = (playerId * 90) + 45;

        Vector3 pos = new Vector3(distance * Mathf.Cos(angle * Mathf.Rad2Deg), 1.5f, distance * Mathf.Sin(angle * Mathf.Rad2Deg));

        SpecialCardEffectHandler.DogCard(pos);

        Debug.Log("Dog was played.");
        int myTeam = Manager.main.teamNumbersPerPlayer[playerId];

        for(int i = 0; i < Manager.main.teamNumbersPerPlayer.Length; i++)
        {
			if (playerId != i && Manager.main.teamNumbersPerPlayer [i] == myTeam)
			{
				Manager.main.ChangePlayerTurn ();
				//GameManager.main.turnIndicator.IndicateActivePlayer (i);
			}
        }
    }
}