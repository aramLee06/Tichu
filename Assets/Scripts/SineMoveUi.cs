using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SineMoveUi : MonoBehaviour {

	//RequireComponent [RectTransform];
	private RectTransform myRekt;
	public Vector3 sineSpeed;
	public Vector3 sineAmp;
	private Vector3 localPos;

	// Use this for initialization
	void Start () {
		if (sineSpeed.magnitude <= Vector3.zero.magnitude)
		{
			sineSpeed = Vector3.one;
		}

		myRekt = GetComponent<RectTransform>();
		localPos = myRekt.localPosition;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		localPos.x = sineAmp.x * Mathf.Sin(Time.time * sineSpeed.x);
		localPos.y = sineAmp.y * Mathf.Sin(Time.time * sineSpeed.y);
		localPos.z = sineAmp.z * Mathf.Sin(Time.time * sineSpeed.z);

		myRekt.localPosition = localPos;
	}
}
