using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Manager.
/// </summary>
public class Manager : MonoBehaviour
{
	[Header("Deck")] //Settings for several deck cards.
	/// <summary>
	/// The regular cards.
	/// </summary>
	public Card[] regularCards;
	/// <summary>
	/// The dog card.
	/// </summary>
	public CardDog dogCard;
	/// <summary>
	/// The dragon card.
	/// </summary>
	public CardDragon dragonCard;
	/// <summary>
	/// The mahjong card.
	/// </summary>
	public CardMahJong mahjongCard;
	/// <summary>
	/// The phoenix card.
	/// </summary>
	public CardPhoenix phoenixCard;

	/// <summary>
	/// The selected card object.
	/// </summary>
	public CardObject selectedCardObject;

	/// <summary>
	/// The team numbers per player.
	/// </summary>
	public int[] teamNumbersPerPlayer = new int[] { -1,1,-1,1 };

	/// <summary>
	/// The last kind of play.
	/// </summary>
	public PlayKind lastPlayKind;
	/// <summary>
	/// The last value.
	/// </summary>
	public float lastValue;
	/// <summary>
	/// The last card.
	/// </summary>
	public Card lastCard;
	/// <summary>
	/// The last play.
	/// </summary>
	public List<CardObject> lastPlay = new List<CardObject>();

	/// <summary>
	/// The player number.
	/// </summary>
	public int playerNumber = 0;

	/// <summary>
	/// The deck.
	/// </summary>
	public List<Card> deck = new List<Card>();
	/// <summary>
	/// Singleton
	/// </summary>
	public static Manager main;

	public virtual void Awake()
	{
		if (!main)
			main = this;
	}

	public virtual void ChangePlayerTurn() { }
	public virtual void ChangeWinPlayer(int playerId) { }
	public virtual void GetUserIndex (int index) { }
	public virtual void ReceiveData(string data) { }
	public virtual int GetWinPlayer() { return -1; }
	public virtual void HandleDragon () { }
	public virtual void HandleMahjong () { }

	/// <summary>
	/// Check if the specified player is human
	/// </summary>
	/// <returns></returns>
	/// <param name="id">Identifier.</param>
	public virtual bool IsHuman(int id)
	{
		return false;
	}

	/// <summary>
	/// Creates the identifier deck.
	/// </summary>
	public void CreateIDDeck()
	{
		int i = 0;
		foreach (Card card in regularCards)
		{
			card.id = i;
			i++;
		}
		dogCard.id = i;
		dragonCard.id = i + 1;
		mahjongCard.id = i + 2;
		phoenixCard.id = i + 3;

		deck = new List<Card>(regularCards);
		deck.Add(dogCard);
		deck.Add(dragonCard);
		deck.Add(mahjongCard);
		deck.Add(phoenixCard);
	}
}