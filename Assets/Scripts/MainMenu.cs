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
    // "Start Game" Button implementation
    public void StartGame()
    {
        LevelLoader.SetIsSingleLevel(false);
        LevelLoader.LoadSpecificScene("Level 1");
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
        Application.Quit();
    }
}
