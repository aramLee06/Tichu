using UnityEngine;
using System.Collections;

/// <summary>
/// Lobby AI button.
/// </summary>
public class LobbyAIButton : MonoBehaviour
{
	/// <summary>
	/// The lobby pad handler.
	/// </summary>
	public LobbyPadHandler lobbyPadHandler;
	/// <summary>
	/// The selected player.
	/// </summary>
	public int selected;
	/// <summary>
	/// The ai difficulty.
	/// </summary>
	public AIDifficulty aiDifficulty;

	/// <summary>
	/// Sets the AI.
	/// </summary>
	public void SetAI ()
	{
		lobbyPadHandler.AIJoin (selected, aiDifficulty);
	}
}

// ( ͡° ͜ʖ ͡°) This is fine.