using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GamepadPlayAreaObject : MonoBehaviour 
{
	public Transform playAreaSlotPosition, playAreaExitPosition;
	public SlotID slotId;
	public new Collider collider;
	public bool isHand = false;

	public GamePadUIHandler uiHandler;

	void OnMouseOver ()
	{
		if (uiHandler.currentState == GameState.TRICK || isHand)
		{
			if (uiHandler.hand.Count > uiHandler.heldCard)
			{
				if (uiHandler.hand [uiHandler.heldCard].cardObject.isDragged)
				{
					uiHandler.hand [uiHandler.heldCard].cardObject.slotId = slotId;
				}
			}
		}
	}
}