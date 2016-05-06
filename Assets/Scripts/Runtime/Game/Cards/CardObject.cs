using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CardObject : MonoBehaviour
{
	public Card card;

	[System.NonSerialized] public UIHandler uiHandler;

	new public string name;
	public CardType cardType;
	public float value;
	public SpriteRenderer cardImage;

	public bool isSelected, isDragged, isHovering;
	public int handPos;

	public Manager manager;
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