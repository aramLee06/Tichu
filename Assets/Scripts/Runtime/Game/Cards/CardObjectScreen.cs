using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CardObjectScreen : CardObject
{
	public ScreenUIHandler uiScreenHandler;
	public byte owner = 0;

	public override void Start ()
	{
		uiHandler = ScreenUIHandler.main;
		uiScreenHandler = (ScreenUIHandler) uiHandler;

		base.Start ();

		cardImage.sprite = uiHandler.GetCardImage (card.cardType, (int) card.value, card.name);
		cardImage.material.SetTexture ("_CardTex", cardImage.sprite.texture);
		GameManager.allCards.Add(this.gameObject);
	}
}