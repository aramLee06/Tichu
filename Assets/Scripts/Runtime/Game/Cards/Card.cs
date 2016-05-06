using UnityEngine;
using System;
using System.Collections;

public delegate void CardPlaceEventHandler(object sender, EventArgs args);

[System.Serializable]
public class Card
{
    public string name;
    public int scoreValue;
    public CardType cardType;
	public CardObject cardObject;
    public float value;
    [HideInInspector]public int id;

	public void Start ()
	{
		cardObject.name = name;
		cardObject.cardType = cardType;
		cardObject.value = (int) value;
	}

    public virtual bool CanPlay()
    {
        return true;
    }

    public virtual void OnPlacement(int playerId) { }

    public virtual void OnTrickEnd() { }
}

public enum CardType
{
    PAGODA,
    JADE,
    STAR,
    SWORD,

    SPECIAL,
}