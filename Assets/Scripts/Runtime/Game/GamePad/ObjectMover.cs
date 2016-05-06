using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ObjectMover : MonoBehaviour 
{
	public Button butt;
	public Transform tradeAreaSlotPosition, playAreaExitPosition;
	public float lerpSpeed;
	public GamePadUIHandler uiHandler;

	void Update ()
	{
		Vector3 target;

		if (butt.interactable && !uiHandler.gamepadManager.tradingSlotContains())
		{
			target = new Vector3 (transform.localPosition.x, tradeAreaSlotPosition.localPosition.y, transform.localPosition.z);
		}
		else
		{
			target = new Vector3 (transform.localPosition.x, playAreaExitPosition.localPosition.y, transform.localPosition.z);
		}

		transform.localPosition = Vector3.Lerp (transform.localPosition, target, Time.deltaTime * lerpSpeed);
	}
}