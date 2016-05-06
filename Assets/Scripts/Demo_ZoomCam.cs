using UnityEngine;
using System.Collections;

public class Demo_ZoomCam : MonoBehaviour {

	public Camera myCam;
	public Transform[] target;
	public int targetNumber= 0;
	private float targetFOV = 60;
	private Vector3 originalPos;
	private Vector3 targetPosition;

	// Use this for initialization
	void Start () {
		myCam = GetComponent<Camera> ();
		originalPos = transform.position;
		Debug.Log ("Zoom Target is " + target[targetNumber].gameObject.name);
		targetNumber = 2;
	}
	
	// Update is called once per frame
	void Update () {

		myCam.fieldOfView = Mathf.Lerp (myCam.fieldOfView, targetFOV, 0.5f);
		//int d = (int) Input.GetAxisRaw ("Mouse ScrollWheel");
		if (targetFOV == 20 || myCam.fieldOfView == 20 ) {
			GetComponent<Transform> ().position = Vector3.Lerp(transform.position, targetPosition, 0.25f);
		}

		if (Input.GetKeyDown (KeyCode.LeftArrow) && targetNumber > 0)
		{
			targetNumber -= 1;
			targetPosition = new Vector3 (target[targetNumber].position.x, transform.position.y, transform.position.z);
			Debug.Log ("Zoom Target is " + target[targetNumber].gameObject.name);
		}

		if (Input.GetKeyDown (KeyCode.RightArrow) && targetNumber < target.Length -1)
		{
			targetNumber += 1;
			targetPosition = new Vector3 (target[targetNumber].position.x, transform.position.y, transform.position.z);
			Debug.Log ("Zoom Target is " + target[targetNumber].gameObject.name);
		}

		if (Input.GetKeyDown (KeyCode.UpArrow)) 
		{
			targetPosition = new Vector3 (target[targetNumber].position.x, transform.position.y, transform.position.z);
			targetFOV = 20;
			Debug.Log ("Zoomed In");
		} 
		if (Input.GetKeyDown (KeyCode.DownArrow)) 
		{
			GetComponent<Transform> ().position = originalPos;
			targetFOV = 60;
			Debug.Log ("Zoomed Out");
		}
			
			
	}
}
