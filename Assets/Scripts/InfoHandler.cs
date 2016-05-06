using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InfoHandler : MonoBehaviour
{
	public Text currentPhase;
	public Text infoText;

	public string[] information;
	public string[] phaseTitle;

	public void ChangeState (GameState state)
	{
		currentPhase.text = phaseTitle [(int)state];
		infoText.text = information [(int)state];
	}
}