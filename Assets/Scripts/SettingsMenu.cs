using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    ///  ----
    /// FIELDS
    ///  ----
    private LevelLoader LevelLoader;

    [SerializeField] AudioMixer AudioMixer;
    private AudioListener AudioListener;
    private AudioSource MusicAudioSource;
    private AudioSource SFXAudioSource;
    private List<AudioSource> SFXAudioSubSources;

    [SerializeField] Slider GlobalVolumeSlider;
    [SerializeField] Slider MusicVolumeSlider;
    [SerializeField] Slider SFXVolumeSlider;


    ///  -----------
    /// UNITY METHODS
    ///  -----------
    private void Awake()
    {
        LevelLoader = GameObject.Find("Level Loader").GetComponent<LevelLoader>();

        // Retrieves the AudioListener and sets up stuff to retrieve the AudioSources
        GameObject cam = GameObject.Find("Camera");
        AudioListener = cam.GetComponent<AudioListener>();

        // Retrieves the AudioSources and differenciate them so they won't be confounded later on
        AudioSource[] camAudioSources = cam.GetComponents<AudioSource>();
        AudioMixerGroup[] musicMixerGroup = AudioMixer.FindMatchingGroups("Music");     // Will always have a single member
        AudioMixerGroup[] sfxMixerGroup = AudioMixer.FindMatchingGroups("SFX");         // Will contain : [0] the parent SFX source and [1+] all SFX sub-sources
        foreach (AudioSource src in camAudioSources)
        {
            AudioMixerGroup group = src.outputAudioMixerGroup;

            if (group == musicMixerGroup[0])  MusicAudioSource = src;
            else if (group == sfxMixerGroup[0])  SFXAudioSource = src;
            else SFXAudioSubSources.Add(src);       // Assumes any other sources can only be an SFX sub-source, which might not be true forever
        }
    }


    ///  -----------
    /// CLASS METHODS
    ///  -----------
    // Exit out of the menu when a click happens out of bounds
    public void CloseSettingsMenu()
    {
        LevelLoader.UnloadSpecificScene("SettingsMenu");
    }

    // Handles updates of the Global Volume setting
    public void UpdateGlobalVolume()
    {
        AudioListener.volume = GlobalVolumeSlider.value;
    }
    // Handles updates of the Music Volume setting
    public void UpdateMusicVolume()
    {
        MusicAudioSource.volume = Mathf.Log10(MusicVolumeSlider.value) * 20f;
    }
    // Handles updates of the SFX Volume setting
    public void UpdateSFXVolume()
    {
        SFXAudioSource.volume = Mathf.Log10(SFXVolumeSlider.value * 20f);
    }

}
