using UnityEngine;
using System.Collections;

public class ProfileIconList : MonoBehaviour
{
    public Sprite[] phoenixEmotions = new Sprite[6], dogEmotions = new Sprite[6], dragonEmotions = new Sprite[3], pandaEmotions = new Sprite[6];
    private static ProfileIconList main;

    private void Awake()
    {
        main = this;
    }

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