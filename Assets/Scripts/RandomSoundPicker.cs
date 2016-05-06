using UnityEngine;
using System.Collections;

public class RandomSoundPicker : MonoBehaviour 
{
	public SoundEffect[] soundEffects;
	public AudioSource audioSource;

	public void Awake ()
	{
		int i = Random.Range (0, soundEffects.Length);
		audioSource.PlayOneShot(soundEffects [i].audioClip, soundEffects [i].defaultVolume);
	}
}