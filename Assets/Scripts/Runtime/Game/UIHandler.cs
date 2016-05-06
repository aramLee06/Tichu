using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIHandler : MonoBehaviour 
{
	public static UIHandler main;

	[Header ("Files")]
	public CardImage[] cardImage;
	public Material[] cardMaterial;
	public GameObject cardObjectPrefab;

    [Header("UI Objects")]

    public float cardLerpSpeed = 1, targetDistanceUntilSlowdown = 8;
    public AnimationCurve cardSpeedCurve = new AnimationCurve(new Keyframe(0, 1), new Keyframe(1, 1));

	[SerializeField] protected float
	cardAreaTotalWidth = 12, cardAreaWidthOffset = 6, cardAreaTotalHeight = -0.5f, cardAreaTotalRotation = 35, cardInsetOnSelect = -1,
	playAreaTotalWidth = 12, playAreaWidthOffset = 6;

	public int heldCard, newCardPos;

	[Header ("Game Data")]

	public GameState currentState = GameState.ROUND_START;

	public bool option_MoveToBot = false, option_SwapCards = false;

	//public List<Card> hand;
	public List<CardObject> play;

	public List<Card> trick;

	public Sprite GetCardImage (CardType type, int value, string name = null)
	{
		if (type != CardType.SPECIAL)
		{
			//Debug.Log (value);
			return cardImage [(int)type].image [value - 1];
		}
		else
		{
			switch (name)
			{
				case "Dog":
					return cardImage [4].image [0];
					//break;
				case "Mahjong":
					return cardImage [4].image [1];
					//break;
				case "Phoenix":
					return cardImage [4].image [2];
					//break;
				case "Dragon":
					return cardImage [4].image [3];
					//break;
				default:
					return cardImage [4].image [0];
					//break;
			}
		}
	}
		
	public void OrganizeCards (List<Card> cardList, int indexA, int indexB, bool swap = false)
	{
		if (swap || option_SwapCards)
		{
			SwapCards (cardList, indexA, indexB);
		}
		else
		{
			Card tmp = cardList [indexA];
			cardList.RemoveAt (indexA);
			cardList.Insert (indexB, tmp);

			for (int i = 0; i < cardList.Count; i++)
			{
				cardList [i].cardObject.handPos = i;
				cardList [i].cardObject.isHovering = false;
				cardList [i].cardObject.isDragged = false;
				cardList [i].cardObject.isSelected = false;
			}
		}
	}

	public void SwapCards (List<Card> cardList, int indexA, int indexB)
	{
		Card tmp = cardList [indexA];
		cardList [indexA] = cardList [indexB];
		cardList [indexB] = tmp;

		cardList [indexA].cardObject.handPos = indexA;
		cardList [indexB].cardObject.handPos = indexB;

		cardList [indexA].cardObject.isHovering = false;
		cardList [indexA].cardObject.isDragged = false;
		cardList [indexA].cardObject.isSelected = false;

		cardList [indexB].cardObject.isHovering = false;
		cardList [indexB].cardObject.isDragged = false;
		cardList [indexB].cardObject.isSelected = false;
	}
}
	
[System.Serializable]
public class CardImage
{
	public Sprite[] image;
}