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

    [SerializeField] Slider GlobalVolumeSlider;
    [SerializeField] Slider MusicVolumeSlider;
    [SerializeField] Slider SFXVolumeSlider;


    ///  -----------
    /// UNITY METHODS
    ///  -----------
    private void Awake()
    {
        LevelLoader = GameObject.Find("Level Loader").GetComponent<LevelLoader>();
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
        AudioMixer.SetFloat("GlobalVolume", Mathf.Log10(GlobalVolumeSlider.value) * 20f);
    }
    // Handles updates of the Music Volume setting
    public void UpdateMusicVolume()
    {
        AudioMixer.SetFloat("MusicVolume", Mathf.Log10(MusicVolumeSlider.value) * 20f);
    }
    // Handles updates of the SFX Volume setting
    public void UpdateSFXVolume()
    {
        AudioMixer.SetFloat("SFXVolume", Mathf.Log10(SFXVolumeSlider.value) * 20f);
    }

}
