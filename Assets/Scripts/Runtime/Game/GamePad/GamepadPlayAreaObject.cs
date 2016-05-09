using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Obsolete("Unused")]
public class GamepadPlayAreaObject : MonoBehaviour 
{
	/// <summary>
	/// The play area positions.
	/// </summary>
	public Transform playAreaSlotPosition, playAreaExitPosition;
	/// <summary>
	/// The slot identifier.
	/// </summary>
	public SlotID slotId;
	/// <summary>
	/// The collider.
	/// </summary>
	public new Collider collider;
	/// <summary>
	/// The is hand.
	/// </summary>
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