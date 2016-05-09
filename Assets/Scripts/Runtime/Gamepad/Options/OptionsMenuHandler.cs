using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Options menu handler.
/// </summary>
public class OptionsMenuHandler : MonoBehaviour
{
	/// <summary>
	/// Singleton
	/// </summary>
    public static OptionsMenuHandler main;

	/// <summary>
	/// Sliders
	/// </summary>
    public Slider musicSlider, screenSoundSlider, localSoundSlider;
	/// <summary>
	/// The pause object.
	/// </summary>
    public GameObject pauseObject;

    private void Awake()
    {
        if (!main)
            main = this;
    }

	/// <summary>
	/// Sets the music volume.
	/// </summary>
    public void SetMusicVolume()
    {
        GameOptions.musicVolume = musicSlider.value;
        SendSettings();
    }

	/// <summary>
	/// Sets the screen sound volume.
	/// </summary>
    public void SetScreenSoundVolume()
    {
        GameOptions.soundVolume = screenSoundSlider.value;
        SendSettings();
    }

	/// <summary>
	/// Sets the local sound volume.
	/// </summary>
    public void SetLocalSoundVolume()
    {
        GameOptions.localSoundVolume = localSoundSlider.value;
        AudioListener.volume = GameOptions.localSoundVolume;
    }

	/// <summary>
	/// Sets the language.
	/// </summary>
    public void SetLanguage()
    {
        //GameOptions.language = ???;
        SendSettings();
    }

	/// <summary>
	/// Updates the option elements.
	/// </summary>
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

	/// <summary>
	/// Sends the settings.
	/// </summary>
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

	/// <summary>
	/// Pauses/Unpauses the game.
	/// </summary>
	/// <param name="pause">If set to <c>true</c> pause.</param>
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