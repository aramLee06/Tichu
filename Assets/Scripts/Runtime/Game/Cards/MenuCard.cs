using UnityEngine;
using System.Collections;

[System.Obsolete("No longer in use")]
public class MenuCard : MonoBehaviour 
{
	/// <summary>
	/// The various speeds and offset.
	/// </summary>
	public float lerpSpeed = 1, rotateSpeed = 1, zOffset;
	/// <summary>
	/// The target and starting positions.
	/// </summary>
	public Transform targetPos, startingPos;
	public bool active;

	void Update ()
	{
		Vector3 target, rotateTarget, rot;
		float step = Time.deltaTime * rotateSpeed;

		if (active)
		{
			target = new Vector3 (targetPos.position.x, targetPos.position.y, zOffset);
			rot = new Vector3 (0, 0, 0);
			rotateTarget = Vector3.RotateTowards (transform.localRotation.eulerAngles, rot, step, 10f);
		}
		else
		{
			target = new Vector3 (startingPos.position.x, startingPos.position.y, zOffset);
			rot = new Vector3 (0, 180, 0);
			rotateTarget = Vector3.RotateTowards (transform.localRotation.eulerAngles, rot, step, 15f);
		}

		transform.localPosition = Vector3.LerpUnclamped (transform.localPosition, target, Time.deltaTime * lerpSpeed);
		transform.localRotation = Quaternion.Euler (rotateTarget);
	}
}