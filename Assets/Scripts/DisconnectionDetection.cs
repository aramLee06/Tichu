using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// Disconnection detection.
/// </summary>
public class DisconnectionDetection : MonoBehaviour 
{
	/// <summary>
	/// The frame.
	/// </summary>
	public GameObject frame;
	/// <summary>
	/// Singleton
	/// </summary>
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

	/// <summary>
	/// Indicates the disconnect
	/// </summary>
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