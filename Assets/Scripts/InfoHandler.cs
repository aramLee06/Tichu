using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Info handler.
/// </summary>
public class InfoHandler : MonoBehaviour
{
	public Text currentPhase;
	public Text infoText;

	public string[] information;
	public string[] phaseTitle;

	/// <summary>
	/// Changes the text according to the state
	/// </summary>
	/// <param name="state">State.</param>
	public void ChangeState (GameState state)
	{
		currentPhase.text = phaseTitle [(int)state];
		infoText.text = information [(int)state];
	}
}