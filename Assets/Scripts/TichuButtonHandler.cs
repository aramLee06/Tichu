using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TichuButtonHandler : MonoBehaviour
{
	/// <summary>
	/// The image.
	/// </summary>
	public Image image;
	/// <summary>
	/// The tichu sprite to use.
	/// </summary>
	public Sprite[] tichuSprite;

	public void SetTichu (GameState state)
	{
		switch (state)
		{
			case GameState.ROUND_START:
				image.sprite = tichuSprite [0];
				break;
			case GameState.FIRST_DEAL:
				image.sprite = tichuSprite [1];
				break;
			case GameState.TRADING:
				image.sprite = tichuSprite [2];
				break;
			default:
				image.sprite = tichuSprite [2];
				break;
		}
	}
}