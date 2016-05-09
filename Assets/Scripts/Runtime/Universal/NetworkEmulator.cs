using UnityEngine;
using System.IO;
using System.Collections.Generic;

/// <summary>
/// Network emulator.
/// </summary>
public class NetworkEmulator : MonoBehaviour
{
	/// <summary>
	/// The manager.
	/// </summary>
	public Manager manager;
	/// <summary>
	/// Singleton
	/// </summary>
	public static NetworkEmulator main;
	/// <summary>
	/// Main controller
	/// </summary>
	public MainController netCon;

	/// <summary>
	/// The player indices
	/// </summary>
	public int[] playerIndex = new int[4];

	[Tooltip("Enables debug logs when data is sent. Debug logs are not compiled, however.")]
	public bool enableDebugging;

	private Stream stream;
	private StreamWriter writer;

	/// <summary>
	/// The encryption dictionary.
	/// </summary>
	public Dictionary<byte, string> encryptionDictionary = new Dictionary<byte, string>(); //I'm getting real tired of this bug.
	/// <summary>
	/// The decryption dictionary.
	/// </summary>
	public Dictionary<string, byte> decryptionDictionary = new Dictionary<string, byte>(); 

	public void Awake()
	{
		//TestConnection.Test ();

		if (!main)
			main = this;

		netCon = MainController.main;
		//Debug.Log (netCon);

		if (!netCon.isHostMode ())
			manager.playerNumber = netCon.playerNumber;
		else
			for (int i = 0; i < playerIndex.Length; i++)
			{
				if (netCon.playerIndex [i] >= 0)
					playerIndex [netCon.playerIndex [i]] = i;
			}

		InitializeEncryptionDictionary();

		#if UNITY_EDITOR
		if(enableDebugging)
		{
			string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/Tishu NetEmu Logs";
			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);
			System.DateTime dt = System.DateTime.Now;
			stream = new FileStream((path + "/NetEmu Log - " + dt.Year + "." + dt.Month + "." + dt.Day + "." + dt.Hour + "." + dt.Minute + "." + dt.Second + ".txt").ToString(),FileMode.OpenOrCreate, FileAccess.Write,FileShare.Write);
			writer = new StreamWriter(stream);
		}
		#endif
	}

	private void Start ()
	{
		netCon.netEmu = this;
	}

	/// <summary>
	/// Initializes the encryption dictionary.
	/// </summary>
	private void InitializeEncryptionDictionary()
	{
		char[] c = new char[16]
		{
			'0', '1', '2', '3', '4', '5', '6', '7',
			'8', '9', 'A', 'B', 'C', 'D', 'E', 'F'
		};

		for(byte n = 0; n < 255; n++)
		{
			char firstChar = c[(int)Mathf.Floor(n/16)];
			char secondChar = c[n%16];

			encryptionDictionary.Add(n, firstChar.ToString() + secondChar.ToString());
			decryptionDictionary.Add(firstChar.ToString() + secondChar.ToString(), n);
		}
		encryptionDictionary.Add(255, "FF");
		decryptionDictionary.Add("FF", 255);
	}

	public void OnApplicationQuit()
	{
		#if UNITY_EDITOR
		if (stream != null)
		{
			writer.Close();
			stream.Close();
		}
		#endif
	}

	/// <summary>
	/// Encrypts the string for JSON usage.
	/// </summary>
	/// <returns>The string.</returns>
	/// <param name="input">Input.</param>
	public string FixString(string input)
	{
		//Debug.Log (input);
		//input.Replace("\"", "\\\""); //Replace " with \"
		char[] bytes = input.ToCharArray();

		char[] newBytes = new char[bytes.Length * 2];

		int j = 0;
		for(int i = 0; i < bytes.Length; i++)
		{
			char[] eb = encryptionDictionary[(byte)bytes[i]].ToCharArray();
			newBytes[j] = eb[0];
			newBytes[j + 1] = eb[1];
			j += 2;
		}

		return new string(newBytes);
	}

	/// <summary>
	/// Decrypts the string.
	/// </summary>
	/// <returns>The string.</returns>
	/// <param name="input">Input.</param>
	public string UnfixString(string input)
	{
		//Debug.Log (input);
		//input.Replace("\\\"","\"");
		char[] bytes = input.ToCharArray();
		char[] newBytes = new char[bytes.Length / 2];

		int j = 0;
		for (int i = 0; i < newBytes.Length; i++)
		{
			string eb = bytes[j].ToString() + bytes[j + 1].ToString();
			newBytes[i] = (char)decryptionDictionary[eb];
			j += 2;
		}

		return new string(newBytes);
	}

	/// <summary>
	/// Sends the data.
	/// </summary>
	/// <param name="msg">Message.</param>
	public void SendData(string msg)
	{
		msg = FixString(msg);
		#if UNITY_EDITOR
		if(enableDebugging)
		{
			writer.Write("NetEmu ~ " + "CLIENTS" + " @ " + msg + " ([" + UnfixString(msg) + "])");
		}
		#endif

		//Debug.Log ("Broadcast Data");

		try
		{
			netCon.Send (2, msg);
		}
		catch(System.Exception e) { Debug.LogError(e.Message + "\n" + e.StackTrace + "\n<b>Original message:</b> " + msg); }
	}

	/// <summary>
	/// Sends the data to a specified id.
	/// </summary>
	/// <param name="id">Identifier.</param>
	/// <param name="msg">Message.</param>
	public void SendDataTo(int id, string msg)
	{
		msg = FixString(msg);
		#if UNITY_EDITOR
		if(enableDebugging)
		{
			writer.WriteLine("NetEmu ~ " + id + " @ " + msg + " ([" + UnfixString(msg) + "])");
		}
		#endif

		//Debug.Log ("Send Data To " + id + ". Which actually is to user: " + playerIndex[id]);

		if(manager.IsHuman(id))
		{
			try
			{
				netCon.Send (3, playerIndex[id].ToString(), msg);
			}
			catch (System.Exception e) { Debug.LogError(e.Message + "\n" + e.StackTrace); }
		}
	}

	/// <summary>
	/// Sends the data to host.
	/// </summary>
	/// <param name="msg">Message.</param>
	public void SendDataToHost(string msg)
	{
		msg = FixString(msg);
		#if UNITY_EDITOR
		if(enableDebugging)
		{
			writer.WriteLine("NetEmu ~ " + "HOST" + " @ " + msg + " ([" + UnfixString(msg) + "])");
		}
		#endif

		//Debug.Log ("Send Data To Host");

		try
		{
			netCon.Send (4, msg);
		}
		catch (System.Exception e) { Debug.LogError(e.Message + "\n" + e.StackTrace); }
	}

	/// <summary>
	/// Receives the data.
	/// </summary>
	/// <param name="msg">Message.</param>
	public void ReceiveData (string msg)
	{
		msg = UnfixString(msg);
		#if UNITY_EDITOR
		if(enableDebugging)
		{
			writer.WriteLine("NetEmu ~ " + " @ " + msg + " ([" + FixString(msg) + "])");
		}
		#endif
		try
		{
			manager.ReceiveData (msg);
		}
		catch (System.Exception e) { Debug.LogError(e.Message + "\n" + e.StackTrace + "\n<b>Original Message:</b> " + msg); }
	}
}