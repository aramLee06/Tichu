using UnityEngine;
using System.Collections;

public class QuitHandler : MonoBehaviour
{
	public static QuitHandler main;
	public GameObject window;

	void Awake ()
	{
		if (main == null)
			main = this;
		else
			Destroy (this);

		DontDestroyOnLoad (this);
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			window.SetActive (!window.activeSelf);
		}
	}

	public void Exit ()
	{
		Application.Quit ();
	}

	public void Close ()
	{
		window.SetActive (false);
	}
}