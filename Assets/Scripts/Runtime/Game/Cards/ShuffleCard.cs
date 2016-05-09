using UnityEngine;
using System.Collections;

[System.Obsolete("Unused due to time constraints")]
public class ShuffleCard : MonoBehaviour 
{
	/// <summary>
	/// The starting position.
	/// </summary>
	public Vector3 startingPos;
	/// <summary>
	/// The lerp speed.
	/// </summary>
	public float lerpSpeed;

	void Update ()
	{
		transform.localPosition = Vector3.LerpUnclamped (transform.localPosition, startingPos, Time.deltaTime * lerpSpeed); 
	}
}