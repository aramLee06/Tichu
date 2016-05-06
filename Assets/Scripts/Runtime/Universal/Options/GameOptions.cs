using UnityEngine;
using System.Collections;

public static class GameOptions
{
    public static float musicVolume = 1f;
    public static float soundVolume = 1f;
    public static float localSoundVolume = 1f;
    public static GameLanguage language = GameLanguage.ENGLISH;
    public static bool paused = false;
}

public enum GameLanguage
{
    ENGLISH,
    CHINESE,
}