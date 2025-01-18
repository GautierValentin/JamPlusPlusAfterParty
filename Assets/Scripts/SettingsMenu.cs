using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
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
    // Exit out of the menu when a click happens out of bounds
    public void CloseSettingsMenu()
    {
        LevelLoader.UnloadSpecificScene("SettingsMenu");
    }
}
