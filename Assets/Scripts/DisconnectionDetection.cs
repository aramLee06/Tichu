using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DisconnectionDetection : MonoBehaviour 
{
	public GameObject frame;
	public static DisconnectionDetection main;

	public void Awake ()
	{
        if (!main)
        {
            main = this;
            DontDestroyOnLoad(this);
            GamepadManager.timeoutEvent += DisconnectIndication;
        }
        else
            Destroy(gameObject);
	}

	public void DisconnectIndication ()
	{
		frame.SetActive (true);
	}

	public void Return ()
	{
		SceneManager.LoadScene (0);
		Destroy (gameObject);
	}

	public void Quit ()
	{
		Application.Quit ();
	}
}