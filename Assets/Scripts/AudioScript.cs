using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum SFXClips
{
    ATTACK = 0,
    BUTTONCLICK,
    BUTTONHOVER,
    DEATH,
    LOOT,
    TILEACTION,
    TILESELECT,
    TILESET,
    VICTORY
}

public class AudioScript : MonoBehaviour
{
    AudioSource musicSource;
    AudioSource[] sfxSources = new AudioSource[9];

    void Start()
    {
        AudioSource[] allSources = GetComponents<AudioSource>();

        // Extracts the music and the SFXs separately.
        // !!! This only works because there is :
        //     - Only 1 music
        //     - The SFX sources are added in the same order as the SFCLips enum (up there ↑)
        musicSource = allSources[0];
        for (int i = 1; i < allSources.Length; i++)
        {
            Debug.Log("adding " + allSources[i].name + " at index " + i);
            sfxSources[i-1] = allSources[i];
        }

        // Debug message
        Debug.Log("musicSources : " + musicSource.name);
        for (int i = 0; i < sfxSources.Length; i++)
            Debug.Log("sfxSources[" + i + "] : " + sfxSources[i].resource.name);
    }

    public void PlaySFXClip(SFXClips clip, float _pitch = 1)
    {
        sfxSources[(int)clip].pitch = _pitch;
        sfxSources[(int)clip].Play();
    }
}
