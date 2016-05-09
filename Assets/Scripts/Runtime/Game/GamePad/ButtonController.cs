using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
[System.Obsolete("No longer in use")]
public class ButtonController : MonoBehaviour 
{
	private Button button;
	private Material material;

	public bool inverted;
	private float activeTimer = 0;
	private float activeTime = 2;
	public GameObject lightRing;
	public enum RingColor {Green, Red}
	public RingColor ringColor;
	public GameObject mySocket;
	private Material socketMat;

	private float currentSpeed = 0;
	private float targetSpeed = 0;
	private float lerpSpeed = 0.05f;
	private bool pulse = true;
	private float currentGlow = 0.5f;
	private float socketGlow;

	public AudioSource mySound;

	//Teststuff
	public KeyCode myButton;

	void Start ()
	{
		button = GetComponent<Button> ();
		material = GetComponent<Image> ().material;
		socketMat = mySocket.GetComponent<Image> ().material;
		mySound = GetComponent <AudioSource> ();
	}

	void Update () 
	{
		//Debug.Log (button.IsInteractable());
		currentSpeed = Mathf.Lerp (currentSpeed, targetSpeed, lerpSpeed);
		if (inverted) 
		{
			material.SetFloat ("_Angle", material.GetFloat ("_Angle") - currentSpeed * Time.deltaTime);
		}
		else 
		{
			material.SetFloat ("_Angle", material.GetFloat ("_Angle") + currentSpeed * Time.deltaTime);
		}
		socketMat.SetFloat ("_GlowValue", socketGlow);

		if (pulse == false)
		{
			material.SetFloat ("_GlowValue", currentGlow);
			socketGlow = Mathf.Lerp (socketGlow, material.GetFloat ("_GlowValue"), 0.05f);

		}
		if (pulse == true) 
		{
			material.SetFloat ("_GlowValue", currentGlow + Mathf.Sin (Time.time * 3f) * 0.3f);
			socketGlow = Mathf.Lerp (socketGlow, material.GetFloat ("_GlowValue") * 1.1f, 0.25f);
		}

		if (button.IsInteractable ())
		{
			pulse = true;
			targetSpeed = 0.5f;
			currentGlow = Mathf.Lerp (currentGlow, 0.2f, 0.05f);

			if (activeTimer > Time.time)
			{
				currentGlow = Mathf.Lerp (currentGlow, 0.2f, 0.03f);
				pulse = false;
			}
		}
		else
		{
			pulse = false;
			targetSpeed = 0.1f;
			currentGlow = Mathf.Lerp (currentGlow, 0.0f, 0.05f);

			if (activeTimer > Time.time)
			{
				currentGlow = Mathf.Lerp (currentGlow, 0.0f, 0.03f);
			}
		}
	}

	public void Activate ()
	{
		activeTimer = Time.time + activeTime;
		currentSpeed = -60;
		currentGlow = 2;
		socketGlow = 2;
		lerpSpeed = 0.05f;
		//button.interactable = false;
		mySound.Play();

		if (ringColor == RingColor.Green) 
		{
			lightRing.GetComponent<RingsOfPower> ().myState = RingsOfPower.Ringstate.Green;
		}
		if (ringColor == RingColor.Red) 
		{
			lightRing.GetComponent<RingsOfPower> ().myState = RingsOfPower.Ringstate.Red;
		}
	}
}