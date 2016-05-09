using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Mahjong screen handler.
/// </summary>
public class MahjongScreenHandler : MonoBehaviour 
{
	/// <summary>
	/// The selected value.
	/// </summary>
	public int value;
	public Text text;
	/// <summary>
	/// The user interface.
	/// </summary>
	public GamePadUIHandler ui;

	/// <summary>
	/// Sets the mahjongś wish card value
	/// </summary>
	/// <param name="newval">Value.</param>
	public void SetValue (int newval)
	{
		if ((value + newval) >= 1 && (value + newval) <= 13)
			value += newval;

		switch (value)
		{
			case 13:
				text.text = "Ace";
				break;
			case 12:
				text.text = "King";
				break;
			case 11:
				text.text = "Queen";
				break;
			case 10:
				text.text = "Jack";
				break;
			default:
				text.text = (value + 1).ToString();
				break;
		}
	}

	public void Respond ()
	{
		ui.Respond (value);
	}
}