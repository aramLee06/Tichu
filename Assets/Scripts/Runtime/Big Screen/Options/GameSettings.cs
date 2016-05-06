using UnityEngine;
using System.Collections;

public static class GameSettings
{
    public static bool[] enableAI = new bool[4];
    public static AIDifficulty[] difficulty = new AIDifficulty[4];
}

public enum AIDifficulty
{
	EASY,
	NORMAL,
	HARD,
}