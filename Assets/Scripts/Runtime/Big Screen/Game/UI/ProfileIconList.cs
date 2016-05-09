using UnityEngine;
using System.Collections;

/// <summary>
/// A class that contains all of the Profile Icon sprites
/// </summary>
public class ProfileIconList : MonoBehaviour
{
	/// <summary>
	/// The various character profile icon sprites.
	/// </summary>
    public Sprite[] phoenixEmotions = new Sprite[6], dogEmotions = new Sprite[6], dragonEmotions = new Sprite[3], pandaEmotions = new Sprite[6];
	/// <summary>
	/// Singleton instance.
	/// </summary>
    private static ProfileIconList main;

    private void Awake()
    {
        main = this;
    }

    /// <summary>
    /// Returns the Profile Icon according to the set parameters
    /// </summary>
    /// <param name="character">The character (Dog, phoenix, dragon, panda...)</param>
    /// <param name="emotion">The character's emotion</param>
    /// <param name="lower">Whether it's for the top profile icons or the bottom</param>
    /// <returns>The respective parameters' icon</returns>
    public static Sprite GetSprite(Character character, Emotion emotion, bool lower)
    {
        if (main == null)
            return null;
        
        switch(character)
        {
            case Character.DOG:
                return main.dogEmotions[(int)emotion + (lower ? 3 : 0)];
            case Character.PHOENIX:
                return main.phoenixEmotions[(int)emotion + (lower ? 3 : 0)];
            case Character.DRAGON:
                return main.dragonEmotions[(int)emotion];
            case Character.PANDA:
                return main.pandaEmotions[(int)emotion + (lower ? 3 : 0)];
        }
        return null;
    }
}

public enum Character
{
    PHOENIX,
    DOG,
    DRAGON,
    PANDA,
}

public enum Emotion
{
    HAPPY,
    NEUTRAL,
    SAD,
}