using UnityEngine;
using UnityEngine.UI;
using System.Collections;
/// <summary>
/// Login numpad button.
/// </summary>
public class LoginNumpadButton : MonoBehaviour 
{
	/// <summary>
	/// The input field.
	/// </summary>
	public InputField inputField;
	/// <summary>
	/// The reject soundclip.
	/// </summary>
	public SoundEffect rejectClip;

	public void Pressed (int value)
	{
		if (inputField.text.Length < 5)
		{
			GetComponent<AudioSource> ().Play ();
			inputField.text += value.ToString ();
		}
		else
			GetComponent<AudioSource> ().PlayOneShot (rejectClip.audioClip, rejectClip.defaultVolume);
	}
}