using UnityEngine;
using System.Collections;

[System.Obsolete("Unused due to time constraints")]
[RequireComponent (typeof(AudioSource))]
public class GamePadCardShuffler : MonoBehaviour 
{
	public ShuffleCard[] shuffleCard;
	public float maxShuffleRange, shakeThreshold;
	public SoundEffect[] shuffleEffect;
	public new AudioSource audio;

	void Start ()
	{
		audio = GetComponent<AudioSource> ();

		for (int i = 0; i < shuffleCard.Length; i++)
		{
			shuffleCard [i].transform.localPosition = new Vector3(Random.Range(-0.25f,0.25f), Random.Range(-0.25f,0.25f), 0.01f * i);
			shuffleCard [i].startingPos = shuffleCard [i].transform.localPosition;
		}
	}

	public void Shuffle (float intensity = 1f)
	{
		for (int i = 0; i < shuffleCard.Length; i++)
		{
			Vector2 shuffleRange = new Vector2(Random.Range (maxShuffleRange / -intensity, maxShuffleRange / intensity), Random.Range (maxShuffleRange / -intensity, maxShuffleRange / intensity));
			shuffleCard [i].transform.localPosition = new Vector3(shuffleRange.x, shuffleRange.y, shuffleRange.x * 0.125f);
			shuffleCard [i].startingPos = new Vector3(Random.Range(-0.1f,0.1f), Random.Range(-0.1f,0.1f), 0.01f * i);;
		}

		int chosenEffect = Random.Range (0, shuffleEffect.Length);

		audio.PlayOneShot (shuffleEffect[chosenEffect].audioClip, shuffleEffect[chosenEffect].defaultVolume);
	}

	void Update ()
	{
		float shakePower = Input.acceleration.magnitude;

		if (shakePower > shakeThreshold)
		{
			Shuffle (shakePower);
		}
	}
}