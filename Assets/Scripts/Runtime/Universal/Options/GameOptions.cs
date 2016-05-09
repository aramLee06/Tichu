using UnityEngine;
using System.Collections;

/// <summary>
/// Game options.
/// </summary>
public static class GameOptions
{
	/// <summary>
	/// The music volume.
	/// </summary>
    public static float musicVolume = 1f;
	/// <summary>
	/// The screen sound volume.
	/// </summary>
    public static float soundVolume = 1f;
	/// <summary>
	/// The local sound volume.
	/// </summary>
    public static float localSoundVolume = 1f;
	/// <summary>
	/// The language.
	/// </summary>
    public static GameLanguage language = GameLanguage.ENGLISH;
	/// <summary>
	/// Paused game?
	/// </summary>
    public static bool paused = false;
}

public enum GameLanguage
{
    ENGLISH,
    CHINESE,
}