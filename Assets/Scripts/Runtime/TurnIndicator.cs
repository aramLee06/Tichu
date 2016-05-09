using UnityEngine;
using UnityEngine.UI;
using System.Collections;
/// <summary>
/// Turn indicator.
/// </summary>
public class TurnIndicator : MonoBehaviour 
{
	/// <summary>
	/// The game manager.
	/// </summary>
	public GameManager gameManager;
	/// <summary>
	/// The player positions.
	/// </summary>
	public Transform[] playerPositions;
	/// <summary>
	/// The player backgrounds.
	/// </summary>
	public Image[] playerBackgrounds;
	/// <summary>
	/// The background textures.
	/// </summary>
	public Sprite[] backgroundTextures;
	/// <summary>
	/// The turn switch sound.
	/// </summary>
	public SoundEffect turnSwitchSound;

	/// <summary>
	/// The sound effects.
	/// </summary>
	public AudioClip[] soundEffects;
	/// <summary>
	/// The audio source.
	/// </summary>
	public AudioSource audioSource;

	public void Start()
	{
		GameManager.turnProgressEvent += IndicateActivePlayer;
	}

	/// <summary>
	/// Indicates the active player.
	/// </summary>
	/// <param name="currentActivePlayer">Current active player.</param>
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