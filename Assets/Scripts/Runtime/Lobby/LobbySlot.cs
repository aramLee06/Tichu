using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LobbySlot : MonoBehaviour
{
	[Header ("Data")]
	public int player = -1;

	public int playerNumber;
	public bool ready = false;
	public string playerName;

	[Header ("Object References")]
	public Image avatarImage;
	public Image readyIndicator;
	public Text playerNameIndicator;

	[Header ("Data References")]
	public float flashSpeed = 1;
	public SoundEffect joinSound;
	public Color joinedColorAvatar;
	public Color idleColorAvatar;
	public Color readyIndicatorColor;

	public void JoinAs (int joinPlayer)
	{
		player = joinPlayer;
		SetName ("Player " + (joinPlayer + 1).ToString ());
		avatarImage.color = joinedColorAvatar;

		GetComponent<AudioSource> ().PlayOneShot (joinSound.audioClip, joinSound.defaultVolume);
	}

	public void JoinAsAI ()
	{
		player = 5;
		SetName ("CPU " + playerNumber.ToString () + " " + GameSettings.difficulty [playerNumber - 1].ToString ());
		avatarImage.color = joinedColorAvatar;

		GetComponent<AudioSource> ().PlayOneShot (joinSound.audioClip, joinSound.defaultVolume);

		SetReady (true);
	}

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