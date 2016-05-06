using UnityEngine;
using System.Collections;

public class ShuffleCard : MonoBehaviour 
{
	public Vector3 startingPos;
	public float lerpSpeed;

	void Update ()
	{
		transform.localPosition = Vector3.LerpUnclamped (transform.localPosition, startingPos, Time.deltaTime * lerpSpeed); 
	}
}