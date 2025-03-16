using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    ///  ----
    /// FIELDS
    ///  ----
    private LevelLoader LevelLoader;

    [SerializeField] AudioMixer AudioMixer;

    [SerializeField] Slider GlobalVolumeSlider;
    [SerializeField] Slider MusicVolumeSlider;
    [SerializeField] Slider SFXVolumeSlider;

    [SerializeField] Button BackToMainMenu;

    bool isPaused = false;


    ///  -----------
    /// UNITY METHODS
    ///  -----------
    private void Awake()
    {
        LevelLoader = GameObject.Find("Level Loader").GetComponent<LevelLoader>();

        BackToMainMenu.onClick.AddListener(() => {
            // Unpauses to avoid a softlock
            GameObject.Find("Main Camera").GetComponent<CameraController>().isPaused = false;
            GameObject.Find("Inventory").GetComponent<TileInventory>().isPaused = false;

            LevelLoader.LoadSpecificScene("MainMenu");
        });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused) ClosePauseMenu();

            isPaused = !isPaused;
        }
    }


    ///  -----------
    /// CLASS METHODS
    ///  -----------
    // Exit out of the menu when a click happens out of bounds
    public void ClosePauseMenu()
    {
        // Unpauses controllers if in gameplay (won't crash if it can't find them)
        GameObject cam = GameObject.Find("Main Camera");
        if (cam) cam.GetComponent<CameraController>().isPaused = false;
        GameObject inv = GameObject.Find("Inventory");
        if (inv) inv.GetComponent<TileInventory>().isPaused = false;

        LevelLoader.UnloadSpecificScene("PauseMenu");
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
