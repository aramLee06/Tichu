using UnityEngine;
using System.Collections.Generic;
/// <summary>
/// Player handler.
/// </summary>
public class PlayerHandler : MonoBehaviour
{
	/// <summary>
	/// Is this a human player?
	/// </summary>
	public bool isHumanPlayer;
	/// <summary>
	/// The type of the tichu called.
	/// </summary>
	public TichuType tichuType = TichuType.NONE;
	/// <summary>
	/// The tichu display.
	/// </summary>
	public DisplayTichu displayTichu;

	/// <summary>
	/// The team.
	/// </summary>
	public Team team;
	/// <summary>
	/// The player identifier.
	/// </summary>
	public int playerId;
	/// <summary>
	/// The amount of trade cards received.
	/// </summary>
	public int tradeCardsReceived = 0;

	/// <summary>
	/// Has the player traded?
	/// </summary>
	public bool hasTraded = false;

	public bool isReady = false;
	public bool isWinner = false;
	public bool isLoser = false;

	/// <summary>
	/// The hand.
	/// </summary>
	/*[HideInInspector]*/ public List<Card> hand = new List<Card>();
	/// <summary>
	/// The trick stash.
	/// </summary>
	/*[HideInInspector]*/ public List<Card> trickStash = new List<Card>(); //Cards the player has won.

	/// <summary>
	/// The selected cards.
	/// </summary>
	public int[] selectedCards;

	/// <summary>
	/// Determines whether this instance has traded.
	/// </summary>
	/// <returns><c>true</c> if this instance has traded; otherwise, <c>false</c>.</returns>
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