using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using UXLib;
using UXLib.Connect;
using UXLib.User;

using SimpleJSON;

public delegate void ConnectingHandler();

public class MainController : MonoBehaviour
{
	[SerializeField]
	public NetworkEmulator netEmu;
	[SerializeField]
	public int userIndex = -1;
	public int[] playerIndex = new int[4];
	public int playerNumber;
	[SerializeField]
	public string lobbyScene;

	public string package_name = "com.cspmedia.uxlib.test";

	public bool isConnected;

	public event ConnectingHandler connectingEvent = delegate{};

	public float timeout;

	/// <summary>
	/// This is Parents of connect class (e.g. UXHostController)
	/// This class is abstract class, so you can NOT use this.
	/// please, look Connect().
	/// 
	/// If you use ConnectController in TV Game, you can use UXHostController.
	/// else you use ConnectController in Game Pad, you can use UXClientController.
	/// </summary>
	public UXConnectController conCtrler = null;

	public static MainController main;

	public virtual void Awake ()
	{
		if (main != null)
			Destroy (this);
		else
		{
			main = this;
			DontDestroyOnLoad (gameObject);
		}

		netEmu = NetworkEmulator.main;
	}

	public virtual void Update()
	{
		/**
		 * Run() is very important Method. 
		 * if uxlib recieved message, parse that, and save to queue. 
		 * That was not worked in unity thread.
		 * 
		 * This Run method, get one message from queue, in unity thread.
		 * And then, The Event (e.g. OnUserAdd, OnGameStart) can access to unity Object
		 * 
		 * Just, call this method every frame.
		 */
		if (conCtrler != null)
		{
			conCtrler.Run ();
		}
	}

	/// <summary>
	/// Connect to server!
	/// </summary>
	public virtual void Connect()
	{
		Init (); // Init Mode

		conCtrler.Connect (); // Connect to server (Just Connect, NOT Join)

		StartCoroutine (WaitUntilConnected ());
		StartCoroutine(WaitUntilTimeout());
		connectingEvent();
	}

	public IEnumerator WaitUntilConnected ()
	{
		Debug.Log (conCtrler.IsConnected().ToString());
		yield return new WaitUntil (conCtrler.IsConnected);
		Join ();
	}

	public IEnumerator WaitUntilTimeout()
	{
		yield return new WaitForSeconds(timeout);
		if(!isConnected)
			Timeout();
	}

	public virtual void Timeout()
	{
		StopCoroutine(WaitUntilConnected());
	}

	public IEnumerator WaitUntilJoined ()
	{
		Debug.Log ("Wait for joining");
		yield return new WaitUntil (conCtrler.IsJoined);

		yield return new WaitUntil (HasUserIndex);
		LoadScene ();
	}

	public bool HasUserIndex ()
	{
		if (userIndex != -1)
			return true;
		else
			return false;
	}

	public virtual void LoadScene ()
	{
		SceneManager.LoadScene (lobbyScene);
	}

	/// <summary>
	/// Join Room!
	/// </summary>
	public virtual void Join()
	{
		conCtrler.Join (package_name); // This is Join. You need set to room number to UXRoom.Instance.RoomNumber

		Send(12, "5");
	}

	/// <summary>
	/// Send btn
	/// <param name="sendType">The different types of sending data and activating events.
	/// 0: update ready count
	/// 1: change lobby state
	/// 2: broadcast (send-all)
	/// 3: send target
	/// 4: send host
	/// 5: get user list
	/// 6: start game
	/// 7: restart game
	/// 8: result game
	/// 9: end game
	/// 10: exit
	/// 11: update user index
	/// 12: set max user
	/// 13: premium user
	/// </param>
	/// </summary>
	public virtual void Send(int sendType, string data1 = null, string data2 = null, string data3 = null)
	{
		//TestConnection.Test ();

		switch (sendType)
		{
		case 0: // update_ready_count 
			// Update ready count message send auto when change player state (ready || wait)
			break;
		case 1: // change_lobby_state
				if (!isHostMode()) 
				{
					//SetPlayerState(UXUser.LobbyState state) //state = 0(wait) OR 1(ready)
					if (int.Parse (data1) == 0) 
					{ //wait
						Debug.Log ("Set inactive");
						((UXClientController)conCtrler).SetPlayerState(UXUser.LobbyState.Wait);
					} 
					else 
					{//ready
						Debug.Log ("Set active");
						((UXClientController)conCtrler).SetPlayerState(UXUser.LobbyState.Ready);
					}
				}
			break;
		case 2: // broadcast
				conCtrler.SendData(data1); // Send Message to all 
			break;
		case 3: // send_target
				conCtrler.SendDataTo/*Code*/(int.Parse(data1), data2);
			break;
		case 4: // send_host
				conCtrler.SendDataToHost(data1);
			break;
		case 5:  //get_user_list
				conCtrler.RefreshUserListFromServer();
			break;
		case 6:  //start_game
				conCtrler.SendStartGame();
			break;
		case 7: //restart_game 
				conCtrler.SendRestartGame();
			break;
		case 8: //result_game 
				conCtrler.SendResultGame();
			break;
		case 9: //end_game
				conCtrler.SendEndGame();
			break;
		case 10: //exit 
				conCtrler.SendExit();
			break;
		case 11: // update_user_index
				conCtrler.SendUserIndex(int.Parse(data1),int.Parse(data2));
			break;
		case 12:  //max_user_set
				// IS host?
					// conCtler casting to UXHostController
					// call SetMaxUser, arguments 'max_user' is parseint data1
				if(isHostMode())
				{
					((UXHostController)conCtrler).SetMaxUser (int.Parse (data1));
				}
				// else
				// nothing
			break;
		case 13:  //premium_user
				UXPlayerController.Instance.IsPremium = true;			
			break;
		default :
			break;
		}
	}

	/// <summary>
	/// Event Binding
	/// </summary>
	protected virtual void Init()
	{
		// Very Important. Our Library is work based on Event
		// if you "what is this Event?!" then just slack me :)
		// I'm JJ
		conCtrler.OnReceived += OnReceived;
		conCtrler.OnUserAdded += OnUserAdded;
		conCtrler.OnUserRemoved += OnUserRemoved;
		conCtrler.OnGameStart += OnGameStart;
		conCtrler.OnGameRestart += OnGameRestart;
		conCtrler.OnGameResult += OnGameResult;
		conCtrler.OnGameEnd += OnGameEnd;
		conCtrler.OnUpdateReadyCount += OnUpdateReadyCount;
		conCtrler.OnUserListReceived += OnUserListReceived;
		conCtrler.OnIndexChanged += OnIndexChanged;
		conCtrler.OnExit += OnExit;
		conCtrler.OnHostDisconnected += OnHostDisconnected;
		conCtrler.OnUpdateReadyCount += OnUpdateReadyCount;
	}
		
	/// <summary>
	/// This test app is host mode?
	/// </summary>
	/// <returns><c>true</c>, if host mode was ised, <c>false</c> otherwise.</returns>
	public virtual bool isHostMode()
	{
		return false;
	}

	#region Event

	public void OnJoinSuccess(bool ishost)
	{
		
	}

	protected virtual void OnReceived (int userIndex, string msg)
	{
		if (netEmu != null)
			netEmu.ReceiveData (msg);
	}

	private void OnUserAdded (int userIndex, int code)
	{
		
	}

	protected virtual void OnUserRemoved (string name, int code)
	{
		int index = conCtrler.GetUserIndexFromCode (code);
	}

	protected virtual void OnGameStart ()
	{
		
	}

	protected virtual void OnGameRestart ()
	{
		
	}

	protected virtual void OnGameResult ()
	{
		
	}

	protected virtual void OnGameEnd ()
	{
		
	}

	protected virtual void OnLobbyStateChanged (int idx, UXUser.LobbyState state)
	{
		
	}

	protected virtual void OnUserListReceived (List<UXUser> list)
	{
		
	}

	protected virtual void OnIndexChanged (int idx)
	{
		
	}

	protected virtual void OnExit ()
	{
		
	}

	protected virtual void OnHostDisconnected ()
	{
		
	}
		
	protected virtual void OnUpdateReadyCount(int ready, int total)
	{
		
	}

	protected virtual void OnJoinPremiumUser ()
	{
		
	}

	#endregion
}