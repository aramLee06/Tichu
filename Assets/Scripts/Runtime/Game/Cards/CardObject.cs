using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Monobehaviour object for Cards
/// </summary>
public class CardObject : MonoBehaviour
{
	/// <summary>
	/// The card.
	/// </summary>
	public Card card;

	/// <summary>
	/// The user interface handler.
	/// </summary>
	[System.NonSerialized] public UIHandler uiHandler;

	/// <summary>
	/// The name.
	/// </summary>
	new public string name;
	/// <summary>
	/// The type of the card.
	/// </summary>
	public CardType cardType;
	/// <summary>
	/// The value.
	/// </summary>
	public float value;
	/// <summary>
	/// The card image.
	/// </summary>
	public SpriteRenderer cardImage;

	/// <summary>
	/// Whether the card object is selected, dragged or hovering.
	/// </summary>
	public bool isSelected, isDragged, isHovering;
	/// <summary>
	/// The hand position.
	/// </summary>
	public int handPos;

	/// <summary>
	/// The manager.
	/// </summary>
	public Manager manager;
	/// <summary>
	/// The slot identifier.
	/// </summary>
	public SlotID slotId = SlotID.HAND;

	public virtual void Start ()
	{
		manager = Manager.main;

		name = card.name;
		cardType = card.cardType;
		value = card.value;

		if (cardType != CardType.SPECIAL)
		{
			cardImage.material = uiHandler.cardMaterial [(int)cardType];
		}
		else
		{
			switch (name)
			{
				case "Dog":
					cardImage.material = uiHandler.cardMaterial [(int)cardType + 0];
					break;
				case "Mahjong":
					cardImage.material = uiHandler.cardMaterial [(int)cardType + 1];
					break;
				case "Phoenix":
					cardImage.material = uiHandler.cardMaterial [(int)cardType + 2];
					break;
				case "Dragon":
					cardImage.material = uiHandler.cardMaterial [(int)cardType + 3];
					break;
				default:
					cardImage.material = uiHandler.cardMaterial [(int)cardType];
					break;
			}
		}
	}

    /// <summary>
    /// Quickly returns the CardObject's card
    /// </summary>
    /// <param name="cardObject">The card object</param>
	public static implicit operator Card(CardObject cardObject)
	{
		return cardObject.card;
	}
}

public enum SlotID
{
	HAND = -1,
	PLAY = -2,
	TRADE_SLOT1 = 0,
	TRADE_SLOT2 = 1,
	TRADE_SLOT3 = 2
}