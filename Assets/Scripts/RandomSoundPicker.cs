using UnityEngine;
using System.Collections;

public class RandomSoundPicker : MonoBehaviour 
{
	/// <summary>
	/// The sound effects.
	/// </summary>
	public SoundEffect[] soundEffects;
	/// <summary>
	/// The audio source.
	/// </summary>
	public AudioSource audioSource;

	public void Awake ()
	{
		int i = Random.Range (0, soundEffects.Length);
		audioSource.PlayOneShot(soundEffects [i].audioClip, soundEffects [i].defaultVolume);
	}
}