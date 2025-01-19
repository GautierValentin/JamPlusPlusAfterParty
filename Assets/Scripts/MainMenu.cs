using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    ///  ----
    /// FIELDS
    ///  ----
    private LevelLoader LevelLoader;
    private AudioScript AudioScript;


    ///  -----------
    /// UNITY METHODS
    ///  -----------
    private void Awake()
    {
        LevelLoader = GameObject.Find("Level Loader").GetComponent<LevelLoader>();
        AudioScript = GameObject.Find("Main Camera").GetComponent<AudioScript>();
    }


    ///  -----------
    /// CLASS METHODS
    ///  -----------
    // "Start Game" Button implementation
    public void StartGame()
    {
        LevelLoader.SetIsSingleLevel(false);
        LevelLoader.LoadSpecificLevel(1);
    }

    // "Level Selection" Button implementation
    public void DisplayLevelSelection()
    {
        LevelLoader.LoadSpecificScene("LevelSelectionMenu");
    }

    // "Options" Button implementation
    public void ShowOptions()
    {
        LevelLoader.LoadSpecificScene("SettingsMenu", true);
    }

    // "Quit" Button implementation
    public void QuitApp()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    public void PlayClickSound()
    {
        AudioScript.PlayClip(SFXClips.BUTTONCLICK);
    }

    public void PlayHoverSound()
    {
        AudioScript.PlayClip(SFXClips.BUTTONHOVER);
    }
}
