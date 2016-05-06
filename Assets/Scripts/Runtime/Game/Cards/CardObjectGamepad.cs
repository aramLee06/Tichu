using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CardObjectGamepad : CardObject
{
	public GamePadUIHandler uiPadHandler;
	public GamepadManager gamepadManager;
	public TextMesh indicatorText;
	public SpriteRenderer glow;

	public override void Start ()
	{
		base.Start ();

		gamepadManager = (GamepadManager) manager;

		uiHandler = GamePadUIHandler.main;
		uiPadHandler = (GamePadUIHandler) uiHandler;
		cardImage.sprite = uiHandler.GetCardImage (cardType, (int) value, name);

		cardImage.material.SetTexture ("_MainTex", cardImage.sprite.texture);
		glow.color = cardImage.material.color;
	}

	private void OnMouseDown ()
	{
		if (uiHandler.currentState == GameState.TRADING)
		{
			if (!isSelected)
			{
				for (int i = 0; i <= 2; i++)
				{
					if (i < gamepadManager.tradeSlot.Length)
					{
						if (gamepadManager.tradeSlot [i] == null)
						{
							indicatorText.text = (gamepadManager.targetPlayer [i] + 1).ToString ();
							slotId = (SlotID)i;
							gamepadManager.tradeSlot [i] = this;
							isSelected = true;
							cardImage.material.SetFloat ("_Selected", 1);
							break;
						}
					}
				}
			}
			else
			{
				indicatorText.text = "";
				gamepadManager.tradeSlot [(int)slotId] = null;
				slotId = SlotID.HAND;
				isSelected = false;
				cardImage.material.SetFloat ("_Selected", 0);
			}
		}
		else if (uiHandler.currentState == GameState.TRICK)
		{
			if (!isSelected)
			{
				uiHandler.play.Add (this);
				gamepadManager.playSlot.Add (this);
				isSelected = true;
				cardImage.material.SetFloat ("_Selected", 1);
				glow.enabled = true;
			}
			else
			{
				uiHandler.play.Remove (this);
				gamepadManager.playSlot.Remove (this);
				isSelected = false;
				cardImage.material.SetFloat ("_Selected", 0);
				glow.enabled = false;
			}
		}
	}

	private void OnMouseOver ()
	{
		isHovering = true;
	}

	private void OnMouseExit ()
	{
		isHovering = false;
	}

	private void DeselectOther ()
	{
		for (int i = 0; i < uiPadHandler.hand.Count; i++)
		{
			uiPadHandler.hand [i].cardObject.isSelected = false;
			uiPadHandler.hand [i].cardObject.isHovering = false;
			uiHandler.play.Remove (uiPadHandler.hand [i].cardObject);
			gamepadManager.playSlot.Remove (uiPadHandler.hand [i].cardObject);
			uiPadHandler.hand [i].cardObject.cardImage.material.SetFloat ("_Selected", 0);
		}
	}

	private void Update ()
	{
		GetComponent<Collider> ().enabled = !isDragged;

		if (isDragged)
		{
			float r = Mathf.LerpAngle (transform.localRotation.eulerAngles.z, 0, Time.deltaTime * uiHandler.cardLerpSpeed * 2);
			transform.localRotation = Quaternion.Euler (0, 0, r);
		}

		if (isHovering && !isDragged)
		{
			uiHandler.newCardPos = handPos;

			if ((int)slotId >= 0)
			{
				if (uiPadHandler.hand [uiHandler.heldCard] != null)
				{
					GetComponent<Collider> ().enabled = false;
				}
			}
		}
	}
}