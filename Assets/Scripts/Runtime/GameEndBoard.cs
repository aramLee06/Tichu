using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// Game end board.
/// </summary>
public class GameEndBoard : MonoBehaviour
{
	[Header ("Winning Team")]
	public Text winTeamText;
	public Text winTeamAmp;
	public Text[] winPlayerName;
	public Text winTotalScore;
	public Image[] winAvatar;

	[Header ("Losing Team")]
	public Text loseTeamText;
	public Text loseTeamAmp;
	public Text[] losePlayerName;
	public Text loseTotalScore;
	public Image[] loseAvatar;

	[Header ("General")]
	/// <summary>
	/// The background.
	/// </summary>
	public Image background;
	public Sprite[] backgroundSprite;
	public Color blue, red;
	/// <summary>
	/// The happy avatars.
	/// </summary>
	public Sprite[] happyAvatars;
	/// <summary>
	/// The sad avatars.
	/// </summary>
	public Sprite[] sadAvatars;
	/// <summary>
	/// The end song.
	/// </summary>
	public AudioSource endSong;
	/// <summary>
	/// The gameplay music.
	/// </summary>
	public DynaMusicHandler gameplayMusic;

	/// <summary>
	/// Updates the scoreboard.
	/// </summary>
	/// <param name="winningTeam">Winning team.</param>
	public void UpdateScoreboard (int winningTeam)
	{
		gameplayMusic.Stop ();

		background.sprite = backgroundSprite [winningTeam];

		if (winningTeam == 0)
		{
			// Winning Team:
			winTeamText.text = "Blue Team WINS";
			winTeamText.color = blue;

			winTotalScore.text = Team.team1.score.ToString ();
			winTotalScore.color = blue;

			winPlayerName [0].text = HostController.main.playerName [0];
			winPlayerName [1].text = HostController.main.playerName [2];
			winPlayerName [0].color = blue;
			winPlayerName [1].color = blue;

			winTeamAmp.color = blue;

			winAvatar [0].sprite = happyAvatars [0];
			winAvatar [1].sprite = happyAvatars [2];

			// Losing Team:
			loseTeamText.text = "Red Team LOSES";
			loseTeamText.color = red;

			loseTotalScore.text = Team.team2.score.ToString ();
			loseTotalScore.color = red;

			losePlayerName [0].text = HostController.main.playerName [1];
			losePlayerName [1].text = HostController.main.playerName [3];
			losePlayerName [0].color = red;
			losePlayerName [1].color = red;

			loseTeamAmp.color = red;

			loseAvatar [0].sprite = sadAvatars [1];
			loseAvatar [1].sprite = sadAvatars [3];
		}
		else
		{
			// Winning Team:
			winTeamText.text = "Red Team WINS";
			winTeamText.color = red;

			winTotalScore.text = Team.team2.score.ToString ();
			winTotalScore.color = red;

			winPlayerName [0].text = HostController.main.playerName [1];
			winPlayerName [1].text = HostController.main.playerName [3];
			winPlayerName [0].color = red;
			winPlayerName [1].color = red;

			winTeamAmp.color = red;

			winAvatar [0].sprite = happyAvatars [1];
			winAvatar [1].sprite = happyAvatars [3];

			// Losing Team:
			loseTeamText.text = "Blue Team LOSES";
			loseTeamText.color = blue;

			loseTotalScore.text = Team.team1.score.ToString ();
			loseTotalScore.color = blue;

			losePlayerName [0].text = HostController.main.playerName [0];
			losePlayerName [1].text = HostController.main.playerName [2];
			losePlayerName [0].color = blue;
			losePlayerName [1].color = blue;

			loseTeamAmp.color = blue;

			loseAvatar [0].sprite = sadAvatars [0];
			loseAvatar [1].sprite = sadAvatars [2];
		}


	}
}