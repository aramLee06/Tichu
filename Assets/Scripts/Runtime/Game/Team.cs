using UnityEngine;
using System.Collections;

/// <summary>
/// Team.
/// </summary>
public class Team
{
	/// <summary>
	/// Team 1
	/// </summary>
    public static Team team1 = new Team() { id = 1 };
	/// <summary>
	/// Team 2
	/// </summary>
    public static Team team2 = new Team() { id = -1 };

	/// <summary>
	/// The score.
	/// </summary>
    public int score;
	/// <summary>
	/// The identifier.
	/// </summary>
    public int id;
}