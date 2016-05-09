using UnityEngine;
using System.Collections;

/// <summary>
/// Game settings class
/// </summary>
public static class GameSettings
{
	/// <summary>
	/// The array determining who is AI and who is not.
	/// </summary>
    public static bool[] enableAI = new bool[4];
	/// <summary>
	/// The difficulty of said AI.
	/// </summary>
    public static AIDifficulty[] difficulty = new AIDifficulty[4];
}

public enum AIDifficulty
{
	EASY,
	NORMAL,
	HARD,
}