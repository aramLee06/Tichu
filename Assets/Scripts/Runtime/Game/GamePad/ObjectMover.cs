using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Moves Card Objecs accordingly
/// </summary>
public class ObjectMover : MonoBehaviour 
{
	/// <summary>
	/// Button
	/// </summary>
	public Button butt;
	/// <summary>
	/// Various positions
	/// </summary>
	public Transform tradeAreaSlotPosition, playAreaExitPosition;
	/// <summary>
	/// The lerp speed.
	/// </summary>
	public float lerpSpeed;
	/// <summary>
	/// The user interface handler.
	/// </summary>
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