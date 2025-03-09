using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SFXClips
{
    ATTACK = 1,
    BUTTONCLICK,
    BUTTONHOVER,
    DEATH,
    LOOT,
    TILEACTION,
    TILESELECT,
    TILESET,
    VICTORY,
    BOSSDEATH,
}

public class AudioScript : MonoBehaviour
{
    AudioSource[] audioSources;

    // Start is called before the first frame update
    void Start()
    {
        audioSources = GetComponents<AudioSource>();
    }

    public void PlayClip(SFXClips clip, float _pitch = 1)
    {
        audioSources[(int)clip].pitch = _pitch;

        audioSources[(int)clip].Play();
    }
}
