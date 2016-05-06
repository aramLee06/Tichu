using UnityEngine;
using System.Collections;

public class BombSpawn : MonoBehaviour {
	/// <summary>
	/// The spawn transforms.
	/// </summary>
	public Transform[] spawnTransforms;

	/// <summary>
	/// The end position.
	/// </summary>
	private Vector3 endPos;
	private int counter = 0;

	public float smallWait;
	public float bigWait;

	public GameObject[] smallBomb;
	public GameObject[] bigBomb;

	// Use this for initialization
	void Start () 
	{
		endPos = new Vector3 (0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			StartCoroutine (Bomb ());
		}
	}

	public IEnumerator Bomb ()
	{
		if (counter < spawnTransforms.Length) 
		{
			yield return new WaitForSeconds (smallWait);
			for (int i = 0; i < smallBomb.Length; i++) 
			{
				Instantiate (smallBomb[i], spawnTransforms [counter].position + smallBomb[i].transform.position, Quaternion.identity);
			}
			counter += 1;
			StartCoroutine (Bomb ());
		}
		if (counter == spawnTransforms.Length) 
		{
			yield return new WaitForSeconds (bigWait);
			for (int i = 0; i < bigBomb.Length; i++) 
			{
				Instantiate (bigBomb[i], endPos, bigBomb[i].transform.rotation);
			}
			counter = 0;
		}
	}
}
