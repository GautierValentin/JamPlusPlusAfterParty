using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectionMenu : MonoBehaviour
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
    // "Close Selection Menu" Button implementation
    public void CloseSelectionMenu()
    {
        LevelLoader.SetIsSingleLevel(false);
        LevelLoader.LoadSpecificScene("MainMenu");
    }
}
