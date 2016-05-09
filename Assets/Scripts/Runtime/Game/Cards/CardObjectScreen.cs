using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Card Object specified for the Screen's side
/// </summary>
public class CardObjectScreen : CardObject
{
	/// <summary>
	/// The user interface screen handler.
	/// </summary>
	public ScreenUIHandler uiScreenHandler;
	/// <summary>
	/// The owner.
	/// </summary>
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