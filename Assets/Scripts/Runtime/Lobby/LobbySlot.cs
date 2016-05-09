using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Lobby slot.
/// </summary>
public class LobbySlot : MonoBehaviour
{
	[Header ("Data")]
	/// <summary>
	/// The player id.
	/// </summary>
	public int player = -1;

	/// <summary>
	/// The player number.
	/// </summary>
	public int playerNumber;
	/// <summary>
	/// Is the player ready?
	/// </summary>
	public bool ready = false;
	/// <summary>
	/// The name of the player.
	/// </summary>
	public string playerName;

	[Header ("Object References")]
	/// <summary>
	/// The avatar image.
	/// </summary>
	public Image avatarImage;
	/// <summary>
	/// The ready indicator.
	/// </summary>
	public Image readyIndicator;
	/// <summary>
	/// The player name indicator.
	/// </summary>
	public Text playerNameIndicator;

	[Header ("Data References")]
	/// <summary>
	/// The flash speed.
	/// </summary>
	public float flashSpeed = 1;
	/// <summary>
	/// The join sound.
	/// </summary>
	public SoundEffect joinSound;
	/// <summary>
	/// The joined avatar color.
	/// </summary>
	public Color joinedColorAvatar;
	/// <summary>
	/// The idle avatar color.
	/// </summary>
	public Color idleColorAvatar;
	/// <summary>
	/// The color of the ready indicator.
	/// </summary>
	public Color readyIndicatorColor;

	/// <summary>
	/// Joins as specified player
	/// </summary>
	/// <param name="joinPlayer">Joined player.</param>
	public void JoinAs (int joinPlayer)
	{
		player = joinPlayer;
		SetName ("Player " + (joinPlayer + 1).ToString ());
		avatarImage.color = joinedColorAvatar;

		GetComponent<AudioSource> ().PlayOneShot (joinSound.audioClip, joinSound.defaultVolume);
	}


	/// <summary>
	/// Joins as AI.
	/// </summary>
	public void JoinAsAI ()
	{
		player = 5;
		SetName ("CPU " + playerNumber.ToString () + " " + GameSettings.difficulty [playerNumber - 1].ToString ());
		avatarImage.color = joinedColorAvatar;

		GetComponent<AudioSource> ().PlayOneShot (joinSound.audioClip, joinSound.defaultVolume);

		SetReady (true);
	}

	/// <summary>
	/// Leave the slot
	/// </summary>
	public void Leave ()
	{
		//Debug.Log ("Player " + player + " left");
		
		player = -1;
		SetName ("< Open >");
		avatarImage.color = idleColorAvatar;

		SetReady (false);
	}

	public void SetReady (bool setTo)
	{
		Debug.Log ("Set to " + setTo);

		ready = setTo;

		if (setTo)
			readyIndicator.color = Color.white;
		else
			readyIndicator.color = readyIndicatorColor;
	}

	public void SetName (string name)
	{
		playerName = name;
		playerNameIndicator.text = name;
	}
}