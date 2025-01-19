using System.Collections;
using System.Collections.Generic;
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
    AudioSource[] audioSources;

    // Start is called before the first frame update
    void Start()
    {
        audioSources = GetComponents<AudioSource>();
    }

    public void PlayClip(SFXClips clip)
    {
        audioSources[(int)clip].Play();
    }
}
