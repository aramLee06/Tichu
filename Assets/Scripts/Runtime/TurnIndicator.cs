using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TurnIndicator : MonoBehaviour 
{
	public GameManager gameManager;
	public Transform[] playerPositions;
	public Image[] playerBackgrounds;
	public Sprite[] backgroundTextures;
	public SoundEffect turnSwitchSound;

	public AudioClip[] soundEffects;
	public AudioSource audioSource;

	public void Start()
	{
		GameManager.turnProgressEvent += IndicateActivePlayer;
	}

	public void IndicateActivePlayer (int currentActivePlayer)
	{
		NetworkEmulator.main.SendDataTo(currentActivePlayer, "v9");

		turnSwitchSound.audioClip = soundEffects [Random.Range (0, soundEffects.Length)];

		//if (gameManager.currentState != GameState.TRICK)
		{
			audioSource.PlayOneShot (turnSwitchSound.audioClip, turnSwitchSound.defaultVolume);

			transform.position = playerPositions [currentActivePlayer].position;
			playerBackgrounds [currentActivePlayer].sprite = backgroundTextures [4 + currentActivePlayer];

			for (int i = 0; i < 4; i++)
			{
				if (i != currentActivePlayer)
					playerBackgrounds [i].sprite = backgroundTextures [i];
			}
		}
	}
}