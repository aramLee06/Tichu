using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AdjustVolume : MonoBehaviour
{
    private AudioSource source;
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