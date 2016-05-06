using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GamepadTradeAreaHandler : MonoBehaviour 
{
	public Button butt;
	public float lerpSpeed = 1;
	public SlotID slotId;
	public Transform tradeAreaSlotPosition, playAreaExitPosition;

	public GamePadUIHandler uiHandler;

	void Update () 
	{
		if (uiHandler.currentState == GameState.TRADING)
			butt.interactable = true;
		else
			butt.interactable = false;


		Vector3 target;

		if (butt.interactable)
		{
			target = new Vector3 (transform.localPosition.x, tradeAreaSlotPosition.localPosition.y, transform.localPosition.z);
		}
		else
		{
			target = new Vector3 (transform.localPosition.x, playAreaExitPosition.localPosition.y, transform.localPosition.z);
		}

		transform.localPosition = Vector3.Lerp (transform.localPosition, target, Time.deltaTime * lerpSpeed);
	}

	public void Clear ()
	{
		uiHandler.gamepadManager.tradeSlot [Mathf.Abs ((int)slotId)].slotId = SlotID.HAND;
		uiHandler.PlaceCard(uiHandler.gamepadManager.tradeSlot [Mathf.Abs ((int)slotId)]);
	}

	void OnMouseOver ()
	{
		if (uiHandler.hand.Count >= uiHandler.heldCard && uiHandler.hand [uiHandler.heldCard] != null)
		{
			if (uiHandler.hand [uiHandler.heldCard].cardObject.isDragged)
			{
				uiHandler.hand [uiHandler.heldCard].cardObject.slotId = slotId;
			}
		}
	}
}