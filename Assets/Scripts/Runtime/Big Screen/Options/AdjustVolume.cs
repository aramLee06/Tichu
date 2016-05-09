using UnityEngine;
using System.Collections;

/// <summary>
/// Volume adjuster script
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class AdjustVolume : MonoBehaviour
{
	/// <summary>
	/// The source.
	/// </summary>
    private AudioSource source;
	/// <summary>
	/// Is it a bgm source or sfx source?
	/// </summary>
    public bool isBgm;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        Update();
    }
    
    private void Update()
    {
        if (isBgm)
            source.volume = GameOptions.musicVolume;
        else
            source.volume = GameOptions.soundVolume;
    }
}