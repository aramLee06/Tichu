using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// Scoreboard.
/// </summary>
public class Scoreboard : MonoBehaviour
{
	/// <summary>
	/// The round's score text.
	/// </summary>
	public Text[] roundScoreText;
	/// <summary>
	/// The total score text.
	/// </summary>
	public Text[] totalScoreText;
	/// <summary>
	/// The name of the player.
	/// </summary>
	public Text[] playerName;
	/// <summary>
	/// The placement image.
	/// </summary>
	public Image[] placementImage;
	/// <summary>
	/// The placement sprites.
	/// </summary>
	public Sprite[] placementSprites;
	/// <summary>
	/// The text colors.
	/// </summary>
	public Color[] normalColor, highColor;

	/// <summary>
	/// Updates the scoreboard.
	/// </summary>
	/// <returns>The placement.</returns>
	/// <param name="roundScores">Round scores.</param>
	/// <param name="outOrder">Out order.</param>
	public int[] UpdateScoreboard(int[] roundScores, int[] outOrder)
	{
		int[] place = new int[roundScores.Length];
		int highRound = 0;

		for(int i = 0; i < roundScores.Length; i++)
		{
			int team = 0;

			if (i == 0 || i == 2)
				team = 1;

			playerName [i].text = HostController.main.playerName [i];

			roundScoreText[i].text = roundScores[i].ToString();
			roundScoreText[i].color = normalColor [team];
			if (roundScores [i] > roundScores [highRound])
				highRound = i;

			try
			{
				int placement = 3;

				for(int j = 0; j < outOrder.Length; j++)
				{
					if (outOrder[j] == i)
						placement = j;
				}

				placementImage[i].sprite = placementSprites[placement + (team * 4)];
				Debug.Log("<color=#aa0>Player " + i + " is on place no. " + (placement+1) + "</color>");
				//place[i] = outOrder[i];
				place[i] = placement;
			}
			catch
			{
				placementImage[i].sprite = placementSprites[placementSprites.Length - 1];
				place[i] = placementSprites.Length - 1;
			}
		}

		totalScoreText[0].text = Team.team1.score.ToString();
		totalScoreText[0].color = normalColor [1];

		totalScoreText[1].text = Team.team2.score.ToString();
		totalScoreText[1].color = normalColor [0];

		if (Team.team1.score > Team.team2.score)
			totalScoreText[0].color = highColor [1];
		else
			totalScoreText[1].color = highColor [0];

		if (highRound == 0 || highRound == 2)
			roundScoreText [highRound].color = highColor [1];
		else
			roundScoreText [highRound].color = highColor [0];
		
		return place;
	}
}