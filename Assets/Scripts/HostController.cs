using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using UXLib;
using UXLib.Connect;
using UXLib.User;

using SimpleJSON;
/// <summary>
/// Host controller.
/// </summary>
public class HostController : MainController
{
	/// <summary>
	/// Singleton
	/// </summary>
	public static new HostController main;

	/// <summary>
	/// The names of the players.
	/// </summary>
	public string[] playerName = new string[4];

	[SerializeField]
	protected Text m_RoomNumberDisplayField;
	[SerializeField]
	public LobbyHandler lobbyHandler;

	public override void Awake()
	{
		base.Awake();
		main = this;
	}

	public void Start ()
	{
		Connect ();

		lobbyHandler = LobbyHandler.main;
	}

	public override void Update()
	{
		if (conCtrler != null) 
		{
			((UXHostController)conCtrler).Run ();
		}
	}

	/// <summary>
	/// Connect to server!
	/// </summary>
	public override void Connect()
	{
		conCtrler = UXHostController.Instance;
		((UXHostController)conCtrler).CreateRoom (package_name, 2);
		m_RoomNumberDisplayField.text = UXConnectController.room.RoomNumber;

		base.Connect ();
	}

	/// <summary>
	/// Join Room!
	/// </summary>
	public override void Join()
	{
		base.Join ();
	}

	protected override void Init()
	{
		((UXHostController)conCtrler).OnJoinSucceeded += OnJoinSuccess;
		((UXHostController)conCtrler).OnUserLobbyStateChanged += OnLobbyStateChanged;
		((UXHostController)conCtrler).OnJoinPremiumUser += OnJoinPremiumUser;

		base.Init ();
	}

	public override bool isHostMode()
	{
		return true;
	}

	#region Event

	/// <summary>
	/// Raises the received event.
	/// </summary>
	/// <param name="userIndex">User index.</param>
	/// <param name="msg">Message.</param>
	protected override void OnReceived (int userIndex, string msg)
	{
		base.OnReceived (userIndex, msg);

		if (LobbyHandler.main != null)
			LobbyHandler.main.ReceiveData (userIndex, msg);
	}

	/// <summary>
	/// Raises the user removed event.
	/// </summary>
	/// <param name="name">Name.</param>
	/// <param name="code">Code.</param>
	protected override void OnUserRemoved (string name, int code)
	{
		base.OnUserRemoved (name, code);

		if (LobbyHandler.main != null)
		{
			string msg = "Q" + userIndex;
			//Debug.Log (msg);
			LobbyHandler.main.ReceiveData (userIndex, msg);
		}
	}

	protected override void OnLobbyStateChanged (int idx, UXUser.LobbyState state)
	{
		if (lobbyHandler != null)
		{
			lobbyHandler.lobbySlot [idx].SetReady (state == UXUser.LobbyState.Ready);
		}
	}

	#endregion
}