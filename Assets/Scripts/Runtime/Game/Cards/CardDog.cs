using UnityEngine;
using System.Collections;

/// <summary>
/// Dog's Special Card
/// </summary>
[System.Serializable]
public class CardDog : Card
{
    /// <summary>
    /// Called when the card has been placed
    /// </summary>
    /// <param name="playerId">ID of the player that places the card</param>
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