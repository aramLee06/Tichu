using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UXLib.User;

public class LobbyPadHandler : MonoBehaviour 
{
	public ClientController client;

	public static LobbyPadHandler main;

	public InputField inputField;
	public Button[] avatarButtons;
	public Button[] leaveButtons;

	public int selectedAvatar = -1;
	public string selectedName = "";
	public string nextScene = "";

	public bool[] joined;
	public bool[] ready;

	public void Awake ()
	{
		main = this;
	}

	public void Start ()
	{
		client = GameObject.Find ("ClientController").GetComponent<ClientController> ();
		client.Send (4, "J");
	}

	public void LobbyJoin (int selected)
	{
		string data = "/" + selected;

		if (selectedAvatar != -1)
		{
			avatarButtons [selectedAvatar].interactable = true;
			avatarButtons [selectedAvatar].animator.SetBool ("IsInteractable", true);
			avatarButtons [selectedAvatar].animator.SetTrigger ("Normal");

			leaveButtons [selectedAvatar].interactable = false;

			data += "F" + selectedAvatar.ToString ();
		}
		else
			data += "E";

		client.Send (2, data);

		leaveButtons [selected].interactable = true;
		avatarButtons [selected].animator.SetBool ("IsInteractable", false);

		selectedAvatar = selected;

		selectedName = inputField.text;

		if (selectedName != "") 
		{
			SetName ();
		}

		Handheld.Vibrate ();
	}

	public void AIJoin (int selected, AIDifficulty difficulty)
	{
		string data = "A" + selected + ((int) difficulty + 1);

		Debug.LogFormat ("{0}, is comprised of selected: {1} and difficulty {2}", data, selected, difficulty);

		client.Send (2, data);

		avatarButtons [selected].animator.SetBool ("IsInteractable", false);

		leaveButtons [selected].interactable = true;
	}

	public void Ready ()
	{
		string data = "R" + selectedAvatar;

		if (ready [selectedAvatar])
			data += "0";
		else
			data += "1";

		client.Send (2, data);
	}

	public void Leave (int leave)
	{
		string data = "L" + leave;

		avatarButtons [leave].interactable = true;
		avatarButtons [leave].animator.SetBool ("IsInteractable", true);
		avatarButtons [leave].animator.SetTrigger ("Normal");

		leaveButtons [leave].interactable = false;

		if (leave == selectedAvatar)
		{
			selectedAvatar = -1;
		}
		else
			data += "AI";

		client.Send (2, data);
	}

	public void SetName ()
	{
		if (selectedAvatar != -1)
		{
			string data = "|" + inputField.text;
			selectedName = inputField.text;
			client.Send (4, data);
		}
	}

	public void StartGame ()
	{
		ready [selectedAvatar] = true;

		if (AllReady ())
		{
			client.Send (2, "Start");
		}
	}

	public bool AllReady ()
	{
		for (int i = 0; i < ready.Length; i++)
		{
			if (ready [i] != true)
			{
				Debug.Log (i + " is not ready");
				return false;
			}
		}

		return true;
	}

	public void ReceiveData (int index, string msg)
	{
		Debug.Log (msg);

		if (msg.StartsWith ("S"))
		{
			string input = msg.Remove (0, 1);

			for (int i = 0; i < 4; i++)
			{
				if (input.Substring (i, 1) == "1" && avatarButtons [i].interactable != false)
				{
					avatarButtons [i].interactable = false;
					avatarButtons [i].animator.SetBool ("IsInteractable", false);
					avatarButtons [i].animator.SetTrigger ("Disabled");
				}
			}
		}

		if (msg.StartsWith ("/") || msg.StartsWith ("A"))
		{
			string input = msg.Remove (0, 1);

			int selected = int.Parse (input.Substring (0, 1));

			joined [selected] = true;
			ready [selected] = false;

			avatarButtons [selected].interactable = false;
			avatarButtons [selected].animator.SetBool ("IsInteractable", false);
			avatarButtons [selected].animator.SetTrigger ("Disabled");

			if (input.Substring (1, 1) == "F")
			{
				int previous = int.Parse (input.Substring (2, 1));

				Debug.Log (previous);
				
				avatarButtons [previous].interactable = true;
				avatarButtons [previous].animator.SetBool ("IsInteractable", true);
				avatarButtons [previous].animator.SetTrigger ("Normal");
			}
		}

		if (msg.StartsWith ("L"))
		{
			string input = msg.Remove (0, 1);

			int selected = int.Parse (input.Substring (0, 1));

			joined [selected] = false;
			ready [selected] = false;

			avatarButtons [selected].interactable = true;
			avatarButtons [selected].animator.SetBool ("IsInteractable", true);
			avatarButtons [selected].animator.SetTrigger ("Normal");
		}

		if (msg.StartsWith ("R"))
		{
			string input = msg.Remove (0, 1);

			ready [int.Parse (input.Substring (0, 1))] = (input.Substring (1, 1) == "1");
		}

		if (msg.StartsWith ("A"))
		{
			string input = msg.Remove (0, 1);

			ready [int.Parse (input.Substring (0, 1))] = true;
		}

		if (msg == "Start")
		{
			client.playerNumber = selectedAvatar;
			client.playerName = selectedName;

			SceneManager.LoadScene (nextScene);
		}
	}
}