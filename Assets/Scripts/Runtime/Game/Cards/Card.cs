using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// Event delegate for when a card has been placed
/// </summary>
/// <param name="sender">Sending object</param>
/// <param name="args">Arguments</param>
public delegate void CardPlaceEventHandler(object sender, EventArgs args);

/// <summary>
/// Default Card class
/// </summary>
[System.Serializable]
public class Card
{
	/// <summary>
	/// The name.
	/// </summary>
    public string name;
	/// <summary>
	/// The score value.
	/// </summary>
    public int scoreValue;
	/// <summary>
	/// The type of the card.
	/// </summary>
    public CardType cardType;
	/// <summary>
	/// The card object.
	/// </summary>
	public CardObject cardObject;
	/// <summary>
	/// The value.
	/// </summary>
    public float value;
	/// <summary>
	/// The identifier.
	/// </summary>
    [HideInInspector]public int id;

	public void Start ()
	{
		cardObject.name = name;
		cardObject.cardType = cardType;
		cardObject.value = (int) value;
	}

    /// <summary>
    /// Whether the card can be played (used in special cards)
    /// </summary>
    /// <returns>Can the card be played?</returns>
    public virtual bool CanPlay()
    {
        return true;
    }

    /// <summary>
    /// Called when the card has been placed
    /// </summary>
    /// <param name="playerId">ID of the player that places the card</param>
    public virtual void OnPlacement(int playerId) { }

    /// <summary>
    /// Called when the trick ends
    /// </summary>
    public virtual void OnTrickEnd() { }
}

/// <summary>
/// Card type.
/// </summary>
public enum CardType
{
    PAGODA,
    JADE,
    STAR,
    SWORD,

    SPECIAL,
}