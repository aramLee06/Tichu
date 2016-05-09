using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Gamepad scoreboard.
/// </summary>
public class GamepadScoreboard : MonoBehaviour
{
	/// <summary>
	/// The child object.
	/// </summary>
    private GameObject child;

    public Text roundScoreText, totalScoreText, playerText;
    public Image placementImage, background;

	/// <summary>
	/// The placement number sprite.
	/// </summary>
    public Sprite[] placementNumberSprite;
	/// <summary>
	/// The background sprite.
	/// </summary>
	public Sprite[] backgroundSprite;

	/// <summary>
	/// The text color blue.
	/// </summary>
	public Color textColorBlue;

    private void Start()
    {
        child = transform.GetChild(0).gameObject;
        child.SetActive(false);
		if (ClientController.main.playerNumber == 0 || ClientController.main.playerNumber == 2)
		{
			background.sprite = backgroundSprite [0];
			roundScoreText.color = textColorBlue;
			totalScoreText.color = textColorBlue;
			playerText.color = textColorBlue;
		}
		else
			background.sprite = backgroundSprite [1];
    }

    public void SetActive(bool active)
    {
        child.SetActive(active);
    }

	/// <summary>
	/// Sets the player number.
	/// </summary>
	/// <param name="number">Number.</param>
    public void SetPlayerNumber(int number)
    {
		if (ClientController.main.playerName != "")
			playerText.text = ClientController.main.playerName;
		else
			playerText.text = "Player " + number;
    }

    public int RoundScore
    {
        set
        {
            roundScoreText.text = value.ToString();
        }
    }

    public int TotalScore
    {
        set
        {
            totalScoreText.text = value.ToString();
        }
    }

    public int Placement
    {
        set
        {
			if (ClientController.main.playerNumber == 0 || ClientController.main.playerNumber == 2)
				placementImage.sprite = placementNumberSprite[value];
			else
				placementImage.sprite = placementNumberSprite[value + 4];
        }
    }
}