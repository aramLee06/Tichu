using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class WhoooPSH : MonoBehaviour {

	private Transform myTrans;
	public Transform startTrans;
	public Transform endTrans;
	private float speed;
	private Vector3 endPos;
	private Vector3 startPos;
	public Transform[] impacticlePrefabs;
	private ParticleSystem[] impacticles;
	private bool splooged;

	public KeyCode PlayKey;			//Demo
	public KeyCode DisableKey;			//Demo
	public KeyCode IntroKey;		//Demo

	public AudioSource impactSound;
	public AudioSource crySound;
	private AudioSource whipSound;
	private AudioSource whooSound;
	public AudioClip whipClip;
	public AudioClip whooshClip;
	private bool introSound = true;
	public float cryTimerPlus;

	void Start () 
	{
		splooged = true;
		impacticles = new ParticleSystem[impacticlePrefabs.Length];
		myTrans = transform;
		endPos = endTrans.position;
		startPos = startTrans.position;

		whipSound = gameObject.AddComponent <AudioSource>() as AudioSource;
		whipSound.clip = whipClip;
		whipSound.volume = 0.4f;
		whooSound = gameObject.AddComponent <AudioSource>() as AudioSource;
		whooSound.clip = whooshClip;
		whooSound.volume = 0.75f;

		for (int i = 0; i < impacticles.Length; i++)
		{
			impacticles[i] = (Instantiate(impacticlePrefabs[i], impacticlePrefabs[i].position+endPos, impacticlePrefabs[i].transform.rotation) as Transform).GetComponent<ParticleSystem>();
			impacticles [i].gameObject.SetActive (false);
		}

		speed = 14;
	}


	void Update () 
	{
		//myTrans.position = Vector3.Lerp (myTrans.position, endPos, speed);
		if (splooged == false) 
		{
			transform.position = Vector3.MoveTowards (myTrans.position, endPos, speed);
		}

		if (myTrans.position == endPos && splooged == false)
		{
			splooged = true;
			impactSound.Play ();
			whipSound.Play ();
			for (int i = 0; i < impacticles.Length; i++)
			{
				impacticles [i].gameObject.SetActive (true);
			}
		}

		if (Input.GetKeyDown (PlayKey))
		{
			StartCoroutine (Reset ());
			if (introSound == true)
			{
				crySound.Play ();
				GetComponent<MeshRenderer> ().enabled = false;
			}
		}

		if (Input.GetKeyDown (IntroKey))
		{
			introSound = !introSound;
		}

		if (Input.GetKeyDown (DisableKey))
		{
			GetComponent<MeshRenderer> ().enabled = !GetComponent<MeshRenderer> ().enabled;
		}
	}


	IEnumerator Reset ()
	{
		if (introSound == true) 
		{
			yield return new WaitForSeconds (crySound.clip.length + cryTimerPlus);
		}

		splooged = false;
		whooSound.Play ();
		GetComponent<MeshRenderer> ().enabled = true;
		myTrans.position = startPos;
		for (int i = 0; i < impacticles.Length; i++)
		{
			impacticles [i].gameObject.SetActive (false);
		}
	}
}
