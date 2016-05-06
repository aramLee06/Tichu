using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoginNumpadButton : MonoBehaviour 
{
	public InputField inputField;
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