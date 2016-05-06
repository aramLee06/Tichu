using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoginBackButton : MonoBehaviour 
{
	public InputField inputField;

	public void Pressed ()
	{
		GetComponent <AudioSource> ().Play ();
		inputField.text = "";
	}
}