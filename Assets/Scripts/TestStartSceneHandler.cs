using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TestStartSceneHandler : MonoBehaviour 
{
	public string hostLobbyScene, clientLobbyScene;

	public void Host () 
	{
		SceneManager.LoadScene (hostLobbyScene);
	}

	public void Client ()
	{
		SceneManager.LoadScene (clientLobbyScene);
	}
}