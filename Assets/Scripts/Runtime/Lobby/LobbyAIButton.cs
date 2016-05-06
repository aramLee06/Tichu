using UnityEngine;
using System.Collections;

public class LobbyAIButton : MonoBehaviour
{
	public LobbyPadHandler lobbyPadHandler;
	public int selected;
	public AIDifficulty aiDifficulty;

	public void SetAI ()
	{
		lobbyPadHandler.AIJoin (selected, aiDifficulty);
	}
}

// ( ͡° ͜ʖ ͡°) This is fine.