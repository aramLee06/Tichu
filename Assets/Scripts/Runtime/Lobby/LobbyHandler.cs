using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// Lobby handler.
/// </summary>
public class LobbyHandler : MonoBehaviour 
{
	/// <summary>
	/// The host controller.
	/// </summary>
	public HostController host;

	/// <summary>
	/// Singleton
	/// </summary>
	public static LobbyHandler main;

	/// <summary>
	/// The lobby slots.
	/// </summary>
	public LobbySlot[] lobbySlot;
	/// <summary>
	/// The indexes of the users.
	/// </summary>
	public int[] userIndex;
	/// <summary>
	/// The names of the players.
	/// </summary>
	public string[] playerName = new string[4];

	/// <summary>
	/// The next scene.
	/// </summary>
	public string nextScene;

	public void Awake ()
	{
		main = this;
	}

	public void Start ()
	{
		host = GameObject.Find ("HostController").GetComponent<HostController> ();
	}

	/// <summary>
	/// Handles the received data
	/// </summary>
	/// <param name="index">Index.</param>
	/// <param name="msg">Message.</param>
	public void ReceiveData (int index, string msg)
	{
		//Debug.Log (msg);

		if (msg.StartsWith ("J"))
		{
			string data = "S";

			for (int i = 0; i < 4; i++)
			{
				if (lobbySlot [i].player != -1)
					data += 1;
				else
					data += 0;
			}

			host.Send (2, data);
		}

		if (msg.StartsWith ("|"))
		{
			string input = msg.Remove (0, 1);

			lobbySlot [userIndex [index]].SetName (input);
			playerName [userIndex [index]] = input;
		}

		if (msg.StartsWith ("/"))
		{
			int input = int.Parse (msg.Substring (1, 1));
			if (lobbySlot [input].player == -1)
			{
				if (userIndex [index] != -1)
					lobbySlot [userIndex [index]].Leave ();
				lobbySlot [input].JoinAs (index);
				playerName [input] = "Player " + (index + 1);
				userIndex [index] = input;
			}
		}

		if (msg.StartsWith ("A"))
		{
			string input = msg.Remove (0, 1);

			int targetAI = int.Parse (input.Substring (0, 1));
			int difficulty = int.Parse (input.Substring (1, 1));

			if (difficulty > 0) //Set true
			{
				if (lobbySlot [targetAI] != null)
				{
					GameSettings.enableAI [targetAI] = true;
					GameSettings.difficulty [targetAI] = (AIDifficulty)(difficulty - 1);
					playerName [targetAI] = "CPU " + (targetAI + 1);
					lobbySlot [targetAI].JoinAsAI ();
				}
			}
			else if (difficulty == 0) //Set false
			{
				if (lobbySlot [targetAI] != null)
				{
					GameSettings.enableAI [targetAI] = false;
					playerName [targetAI] = "";
					lobbySlot [targetAI].Leave ();
				}
			}
		}

		if (msg.StartsWith ("L"))
		{
			string input = msg.Remove (0, 1);

			if (input.Length > 1)
			{
				int targetAI = int.Parse (input.Substring (0, 1));

				lobbySlot [targetAI].Leave ();

				GameSettings.enableAI [targetAI] = false;
				playerName [targetAI] = "";
			}
			else
			{
				lobbySlot [userIndex [index]].Leave ();
				userIndex [index] = -1;
				playerName [index] = "";
			}
		}

		if (msg.StartsWith ("Q"))
		{
			string input = msg.Remove (0, 1);

			if (userIndex[index] != -1)
				lobbySlot [userIndex [index]].Leave ();

			playerName [index] = "";
			userIndex [index] = -1;

			if (index == 0)
			{
				for (int i = 0; i < lobbySlot.Length; i++)
				{
					if (lobbySlot [i].player == 5)
					{
						lobbySlot [i].Leave ();

						GameSettings.enableAI [i] = false;
						playerName [i] = "";
					}
				}
			}
		}

		if (msg.StartsWith ("R"))
		{
			string input = msg.Remove (0, 1);

			int target = int.Parse (input.Substring (0, 1));

			lobbySlot [target].SetReady (input.Substring (1, 1) == "1");
		}

		if (msg == "Start")
		{
			for (int i = 0; i < userIndex.Length; i++)
			{
				host.playerIndex [i] = userIndex [i];
				host.playerName [i] = playerName [i];
			}

			SceneManager.LoadScene (nextScene);
		}
	}
}