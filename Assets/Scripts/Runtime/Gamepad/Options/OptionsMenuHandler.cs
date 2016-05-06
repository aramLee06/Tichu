using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsMenuHandler : MonoBehaviour
{
    public static OptionsMenuHandler main;

    public Slider musicSlider, screenSoundSlider, localSoundSlider;
    public GameObject pauseObject;

    private void Awake()
    {
        if (!main)
            main = this;
    }

    public void SetMusicVolume()
    {
        GameOptions.musicVolume = musicSlider.value;
        SendSettings();
    }

    public void SetScreenSoundVolume()
    {
        GameOptions.soundVolume = screenSoundSlider.value;
        SendSettings();
    }

    public void SetLocalSoundVolume()
    {
        GameOptions.localSoundVolume = localSoundSlider.value;
        AudioListener.volume = GameOptions.localSoundVolume;
    }

    public void SetLanguage()
    {
        //GameOptions.language = ???;
        SendSettings();
    }

    public void UpdateOptionElements()
    {
        musicSlider.value = GameOptions.musicVolume;
        screenSoundSlider.value = GameOptions.soundVolume;
        localSoundSlider.value = GameOptions.localSoundVolume;
        //Set the language thing to the chosen language

        if(GameOptions.paused)
        {
            pauseObject.SetActive(true);
        }
    }

    public void SendSettings()
    {
        int playerNumber = GamepadManager.main.playerNumber;
        int musicVolume = (int)(GameOptions.musicVolume * 255);
        int soundVolume = (int)(GameOptions.soundVolume * 255);
        int gameLanguage = (int)GameOptions.language;

        NetworkEmulator.main.SendDataToHost("O" + playerNumber.ToString() + ((char)musicVolume) + ((char)soundVolume) + ((char)gameLanguage));
        for(int i = 0; i < 4; i++)
        {
            if (i != playerNumber)
                NetworkEmulator.main.SendDataTo(i,"O" + ((char)musicVolume) + ((char)soundVolume) + ((char)gameLanguage));
        }
    }

    public void PauseUnpauseGame(bool pause)
    {
        pauseObject.SetActive(pause);
        int playerNumber = GamepadManager.main.playerNumber;

        string addition = pause ? "+" : "-";

        NetworkEmulator.main.SendDataToHost("B" + playerNumber.ToString() + addition);
        for (int i = 0; i < 4; i++)
        {
            if (i != playerNumber)
                NetworkEmulator.main.SendDataTo(i, "B" + addition);
        }

        GameOptions.paused = pause;
    }
}