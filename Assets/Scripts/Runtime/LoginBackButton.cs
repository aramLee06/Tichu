using UnityEngine;
using UnityEngine.UI;
using System.Collections;
/// <summary>
/// Login back button.
/// </summary>
public class LoginBackButton : MonoBehaviour 
{
	/// <summary>
	/// The input field.
	/// </summary>
	public InputField inputField;

	public void Pressed ()
	{
		GetComponent <AudioSource> ().Play ();
		inputField.text = "";
	}
}