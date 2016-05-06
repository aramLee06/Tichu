using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using UXLib;
using UXLib.Connect;
using UXLib.User;

using SimpleJSON;

public class ClientController : MainController
{
	public static new ClientController main;
	public string mainScene, leadLobbyScene;
	public string playerName;

	public bool isConnecting;

	[SerializeField]
	protected InputField m_RoomNumberInputField;

	public override void Awake()
	{
		base.Awake();
		main = this;
	}

	public override void Update()
	{
		if (conCtrler != null)
		{
			((UXClientController)conCtrler).Run ();
		}
	}

	public override void Connect()
	{
		if (conCtrler != UXClientController.Instance)
		{
			isConnecting = true;
			conCtrler = UXClientController.Instance;
			conCtrler.player.GetUserCodeFromServer ();
			base.Connect ();
		}
	}

	public override void Timeout()
	{
		base.Timeout();
		conCtrler = null;
		Debug.Log("<color=#aa2200ff>Timeout occured.</color>");
		isConnecting = false;
	}

	public override void Join()
	{
		UXClientController.room.RoomNumber = m_RoomNumberInputField.text;

		base.Join ();

		StartCoroutine (WaitUntilJoined ());
	}

	public override void LoadScene ()
	{
		isConnected = true;

		if (userIndex == 0)
		{
			SceneManager.LoadScene (leadLobbyScene);
		}
		else
			SceneManager.LoadScene (lobbyScene);
	}

	public bool IsNotConnecting()
	{
		return !isConnecting;
	}

	protected override void Init()
	{ 
		((UXClientController)conCtrler).OnJoinSucceeded += OnJoinSuccess;

		base.Init ();
	}

	public override bool isHostMode()
	{
		return false;
	}

	#region Event

	protected override void OnHostDisconnected ()
	{
		DisconnectionDetection.main.DisconnectIndication ();
	}

	protected override void OnExit ()
	{
		DisconnectionDetection.main.DisconnectIndication ();
	}

	protected override void OnReceived (int userIndex, string msg)
	{
		base.OnReceived (userIndex, msg);

		if (LobbyPadHandler.main != null)
			LobbyPadHandler.main.ReceiveData (userIndex, msg);
	}

	protected override void OnIndexChanged (int idx)
	{
		userIndex = idx;

		Debug.Log ("User index was changed to: " + userIndex);
	}

	#endregion
}