using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof (Image))] 

public class RingsOfPower : MonoBehaviour {

	public enum Ringstate {Inactive, Active, Green, Red}
	public Ringstate myState;

	private Color targetColor;
	private Color targetTint;
	private float targetGlow;
	private float targetSpeed;

	[Header("Backring Stuff")]
	public GameObject backRing;
	public Color backActTint;
	public Color backInactTint;
	private Material backRingMat;

	[Header("Colors")]
	public Color inactColor; //= new Vector4(255,145,7,255);
	public Color neutralColor; //= new Vector4(251,255,0,255);
	public Color greenColor; //= new Vector4(0,255,1,255);
	public Color redColor; //= new Vector4(255,0,0,255);

	[Header("Tint")]
	public Color inactTint;
	public Color neutralTint;
	public Color greenTint;
	public Color redTint;

	private Animator myAnim;
	private Material myMat;

	// Use this for initialization
	void Start () {
		
		myAnim = gameObject.GetComponent<Animator> ();
		myMat = gameObject.GetComponent<Image>().material;
		backRingMat = backRing.GetComponent<Image> ().material;

	}
	
	// Update is called once per frame
	void Update () {

		myMat.SetColor ("_Color", targetColor);
		myMat.SetColor ("_Tint", targetTint);
		myMat.SetFloat ("_GlowValue", targetGlow);
		myAnim.speed = targetSpeed;

		if (myState == Ringstate.Active) 
		{
			targetColor = Color.Lerp (targetColor, inactColor, 0.25f);
			targetTint = Color.Lerp (targetTint, inactTint, 0.25f);
			targetGlow = Mathf.Lerp (targetGlow, 0, 0.25f);
			targetSpeed = Mathf.Lerp (targetSpeed, 0.1f, 0.1f);

			backRingMat.SetColor("_Tint", Color.Lerp(backRingMat.GetColor("_Tint"), backInactTint, 0.15f));

			if (targetSpeed > 0.09999f && targetSpeed < 0.15f) 
			{
				targetSpeed = 0.1f;
			}
		}

		if (myState == Ringstate.Inactive) 
		{
			targetColor = Color.Lerp (targetColor, neutralColor, 0.1f);
			targetTint = Color.Lerp (targetTint, neutralTint, 0.1f);
			targetGlow = Mathf.Lerp (targetGlow, 0.1f, 0.1f);
			targetSpeed = Mathf.Lerp (targetSpeed, 0.15f, 0.1f);

			backRingMat.SetColor ("_Tint", Color.Lerp(backRingMat.GetColor("_Tint"), backActTint, 0.15f));

			if (targetSpeed > 0.1999f && targetSpeed < 0.25f) 
			{
				targetSpeed = 0.2f;
			}
		}

		if (myState == Ringstate.Green) 
		{
			targetColor = Color.Lerp (targetColor, greenColor, 0.25f);
			targetTint = Color.Lerp (targetTint, greenTint, 0.25f);
			targetGlow = Mathf.Lerp (targetGlow, 0.6f, 0.25f);
			targetSpeed = Mathf.Lerp (targetSpeed, 15f, 0.4f);

			if (targetColor == greenColor) 
			{
				myState = Ringstate.Active;
			}
		}

		if (myState == Ringstate.Red) 
		{
			targetColor = Color.Lerp (targetColor, redColor, 0.25f);
			targetTint = Color.Lerp (targetTint, redTint, 0.25f);
			targetGlow = Mathf.Lerp (targetGlow, 0.6f, 0.25f);
			targetSpeed = Mathf.Lerp (targetSpeed, 15f, 0.4f);

			if (targetColor == redColor) 
			{
				myState = Ringstate.Active;
			}
		}
	}
}
