using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UXLib.Base;
using UXLib.Connect.Protocol;
using UnityEngine;

using SimpleJSON;

namespace UXLib.Connect
{
	public class UXClientSocketNew : UXObject
	{
		const int RBUFFER_SIZE = (4096); //ReceiveBuffer Size

		public static int SOCK_ERROR_SEND = 1;
		public static int SOCK_ERROR_SEND_FILE = 2;
		public static int SOCK_ERROR_READ = 3;

		string hostIP;
		int hostPort;
		private Socket socket;
		private Socket cbSock; //async callback socket

		FileStream fileStream = null;
		BinaryWriter binaryWriter = null;
		bool isFileRecieved;

		private byte[] recieveBuffer = new byte[RBUFFER_SIZE];

		UXProtocol Protocol = UXProtocol.Instance;

		public delegate void OnConnectHandler();
		public delegate void OnConnectFailedHandler();
		public delegate void OnDisConnectHandler();
		public delegate void OnDataReceivedHandler(byte[] arr);
		public delegate void OnErrorHandler(int err, string msg);
		public delegate void OnSendEndedHandler();
		public delegate void OnFileSendEndedHandler();

		public event OnConnectHandler OnConnect;
		public event OnConnectFailedHandler OnConnectFailed;
		public event OnDisConnectHandler OnDisconnect;
		public event OnDataReceivedHandler OnDataReceived;
		public event OnErrorHandler OnError;
		public event OnSendEndedHandler OnSendEnded;
		public event OnFileSendEndedHandler OnFileSendEnded;

		public UXClientSocketNew(string name = "ClientSocket") : base(name) {
			isFileRecieved = false; 
		}


		public bool Open(string host, int port)
		{
			hostIP = host;
			hostPort = port;

			try
			{
				IPAddress serverIp = IPAddress.Parse(hostIP);
				IPEndPoint ipep = new IPEndPoint(serverIp, port);

				socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

				socket.BeginConnect(ipep, new AsyncCallback(OnConnected), socket);//���� ȣ��Ʈ ���ῡ ���� �񵿱� ��û����
				socket.ReceiveTimeout = 2000;
				socket.SendTimeout = 2000;
			}
			catch (Exception e) //�������α׷��� �����Ҷ� ��Ÿ���� ����
			{
				if (OnConnectFailed != null)
				{
					OnConnectFailed();
				}

				return false;
			}

			return true;
		}


		private void OnConnected(IAsyncResult ar)
		{
			try
			{
				if (socket.Connected == false)
				{
					if (OnConnectFailed != null)
					{
						OnConnectFailed();
					}
					return;
				}

				Socket tmpSocket = (Socket) ar.AsyncState;
				tmpSocket.ReceiveTimeout = 2000;
				tmpSocket.SendTimeout = 2000;

				tmpSocket.EndConnect(ar); //���� ���� �񵿱� ���� ��û�� �����ϴ�. 
				cbSock = tmpSocket; 
				cbSock.BeginReceive(this.recieveBuffer, 0, recieveBuffer.Length, SocketFlags.None, new AsyncCallback(OnMessaged), cbSock);//������ �ޱ� (�񵿱�)
				if (OnConnect != null)
				{
					OnConnect();
				}
			}
			catch (SocketException se)
			{
				if (se.SocketErrorCode == SocketError.NotConnected)
				{
					Console.WriteLine(se.Message);

					this.Open(this.hostIP, this.hostPort);
				}
			}

		}//OnConnected


		private void OnMessaged(IAsyncResult ar)
		{
			try
			{
				Socket tempSocket = (Socket) ar.AsyncState;
				int nSize = tempSocket.EndReceive(ar); //�������� �񵿱� �б⸦ ������  ���� ����Ʈ ���� ��ȯ
				if (nSize != 0) //������ ������
				{
					string msg = new UTF8Encoding().GetString(recieveBuffer, 0, nSize); // ����Ʈ->���ڿ�
					byte[] arr = recieveBuffer.Skip(0).Take(nSize).ToArray();
					if (OnDataReceived != null)
					{
						//Debug.Log ("OnMessaged : " + msg);
						OnDataReceived(arr);
					}
					this.Receive();
				}
			}
			catch (SocketException se)
			{
				if (se.SocketErrorCode == SocketError.ConnectionReset)
				{
					this.Open(this.hostIP, this.hostPort);
				}
			}
		}


		public void Receive() //�����͸� �񵿱������� �ٵ礤��
		{
			cbSock.BeginReceive(this.recieveBuffer, 0, recieveBuffer.Length, SocketFlags.None, new AsyncCallback(OnMessaged), cbSock);
		}



		public void SendFile(string filePath)
		{
			if (IsConnected() == false)
			{
				return;
			}

			socket.BeginSendFile(filePath, new AsyncCallback(OnFileSended), socket);
		}


		public void OnFileSended(IAsyncResult ar)
		{

			Thread.Sleep(1000);

			if (OnFileSendEnded != null)
			{
				OnFileSendEnded();
			}
		}


		public void Write(string msg, bool isAsync = false)
		{
			try
			{
				if (IsConnected() == false)
				{
					if (OnError != null)
						OnError(SOCK_ERROR_SEND, "Socket was closed");

					return;
				}

				msg.Replace(UXConnectController.DATA_DELIMITER.ToString(), "");
				//Debug.Log("Write : " + msg);
				var jsonNode = JSON.Parse(msg);
				string command = jsonNode["cmd"];

				byte[] data = Protocol.GeneratorFactory(command).Generate(jsonNode);
				/*
                if (command == "join")
                {
                    data = Protocol.GeneratorFactory(command).Generate(jsonNode);
                    string result = "";
                    foreach (byte b in data) 
                    {
                        result += (b + " ");
                    }
                    Debug.Log("BYTE : " + result);
                }
                else
                {
                    data = new UTF8Encoding().GetBytes(msg + UXConnectController.DATA_DELIMITER);
                }
                //byte[] data = Protocol.GeneratorFactory(jsonNode["cmd"].ToString()).Generate(jsonNode);
                */

				socket.BeginSend(data, 0, data.Length, SocketFlags.None,new AsyncCallback(OnSended), msg); //������
				if (OnSendEnded != null)
					OnSendEnded();

			}
			catch (SocketException se)
			{
				if (OnError != null)
					OnError(SOCK_ERROR_SEND, "Send error: " + se.Message);

				return;
			}
		}

		private void OnSended(IAsyncResult ar)
		{
			string msg = (string) ar.AsyncState;
			if (OnSendEnded != null)
				OnSendEnded();
		}


		public void Disconnect()
		{
			if (socket != null)
			{
				if (socket.Connected == true)
				{
					socket.Close();
				}
			}

			socket = null;

			if (OnDisconnect != null)
			{
				OnDisconnect();
			}
		}


		public void RecieveFile(string fileName)
		{
			fileStream = File.Create(fileName); //������ ��ο� ������ �����.
			binaryWriter = null;
		}


		public bool IsConnected()
		{
			if (socket == null)
			{
				return false;
			}

			return socket.Connected;
		}

	}//class
}//namespace
