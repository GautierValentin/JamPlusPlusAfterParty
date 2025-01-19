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
    // "Close Selection Menu" Button implementation
    public void CloseSelectionMenu()
    {
        LevelLoader.SetIsSingleLevel(false);
        LevelLoader.LoadSpecificScene("MainMenu");
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
