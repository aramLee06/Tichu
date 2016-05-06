using UnityEngine;
using System.Collections;

public class Test_GlowyGlow : MonoBehaviour {

	public KeyCode glowKey;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (glowKey))
		{
			GetComponent<MeshRenderer>().material.SetFloat("_Selected", 1f);
		}

		else
		{
			GetComponent<MeshRenderer>().material.SetFloat("_Selected", 0f);
		}

	}
}
