using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsHealthButtons : MonoBehaviour {


	public enum GlowState {Transition, Active, InActive,} //Transition State only for testing purposes, remove in lieu of coroutine
	public GlowState glowState;
	private Material myMat;
	private float glow;
	private float timer;

		// Use this for initialization
		void Start () {
			myMat = GetComponent<Image>().material;
			glow = 0f;
		}

		// Update is called once per frame
	void Update () {

		myMat.SetFloat ("_GlowValue", glow);

		//Add own timer later on for perfectly timed pulses
		//timer += Time.deltaTime;

		if (glowState == GlowState.Active) 
		{
			glow = Mathf.Lerp (glow, 0.2f, 0.15f);
		}

		if (glowState == GlowState.InActive) 
		{
			glow = Mathf.Lerp (glow, 0f, 0.15f);
		}

		//Transition State only for testing purposes, remove in lieu of coroutine
		if (glowState == GlowState.Transition)  
		{
			StartCoroutine (Transit (GlowState.Active));
		}
	}

	public IEnumerator Transit (GlowState toState)
	{
		//timer = 0;
		glow = 0.2f + Mathf.Sin (Time.time * 18f) * 0.2f;
		//glow = 0.2f + Mathf.Sin (timer * 18f) * 0.2f;
		yield return new WaitForSeconds (0.8f);
		glowState = toState;
	}
}

