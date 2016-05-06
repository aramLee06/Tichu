using UnityEngine;
using System.Collections.Generic;

public class PlayerHandler : MonoBehaviour
{
	public bool isHumanPlayer;
	public TichuType tichuType = TichuType.NONE;
	public DisplayTichu displayTichu;

	public Team team;
	public int playerId;
	public int tradeCardsReceived = 0;

	public bool hasTraded = false;

	public bool isReady = false;
	public bool isWinner = false;
	public bool isLoser = false;

	/*[HideInInspector]*/ public List<Card> hand = new List<Card>();
	/*[HideInInspector]*/ public List<Card> trickStash = new List<Card>(); //Cards the player has won.

	public int[] selectedCards;

	public void HasTraded ()
	{
		if (tradeCardsReceived == 3)
			hasTraded = true;
	}
}

public enum TichuType
{
	NONE = 0,
	TICHU = 100,
	GRAND_TICHU = 200,
	GRAND_GRAND_TICHU = 400,
}