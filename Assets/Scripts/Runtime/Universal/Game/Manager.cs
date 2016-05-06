using UnityEngine;
using System.Collections.Generic;

public class Manager : MonoBehaviour
{
	[Header("Deck")] //Settings for several deck cards.
	public Card[] regularCards;
	public CardDog dogCard;
	public CardDragon dragonCard;
	public CardMahJong mahjongCard;
	public CardPhoenix phoenixCard;

	public CardObject selectedCardObject;

	public int[] teamNumbersPerPlayer = new int[] { -1,1,-1,1 };

	public PlayKind lastPlayKind;
	public float lastValue;
	public Card lastCard;
	public List<CardObject> lastPlay = new List<CardObject>();

	public int playerNumber = 0;

	public List<Card> deck = new List<Card>();
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

	public virtual bool IsHuman(int id)
	{
		return false;
	}

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