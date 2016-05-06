using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayAreaGlow : MonoBehaviour {

	public enum GlowState { Active, Inactive, Transition } //Transition State only for testing purposes, remove in lieu of coroutine
	public GlowState glowState;
	private Material myMat;
	private float glow;

	// Use this for initialization
	void Start () {
		myMat = GetComponent<Image>().material;
		glow = 0f;
	}
	
	// Update is called once per frame
	void Update () {

		myMat.SetFloat ("_GlowValue", glow);

		if (glowState == GlowState.Active) 
		{
			glow = 0.3f + Mathf.Sin (Time.time * 2f) * 0.3f;
		}

		if (glowState == GlowState.Inactive) 
		{
			glow = Mathf.Lerp (glow, 0.3f, 0.15f);
		}

		//Transition State only for testing purposes, remove in lieu of coroutine
		if (glowState == GlowState.Transition) 
		{
			StartCoroutine (Transit (GlowState.Active));
		}
	}

	public IEnumerator Transit (GlowState toState)
	{
		glow = Mathf.Lerp (glow, 1, 0.15f);
		yield return new WaitForSeconds (0.9f);
		glowState = toState;
	}
}
