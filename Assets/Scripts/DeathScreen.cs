using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    ///  ----
    /// FIELDS
    ///  ----
    private LevelLoader LevelLoader;

    [SerializeField] Button BackToMainMenu;
    [SerializeField] Button Quit;
      


    ///  -----------
    /// UNITY METHODS
    ///  -----------
    void Start()
    {
        LevelLoader = GameObject.Find("Level Loader").GetComponent<LevelLoader>();

        BackToMainMenu.onClick.AddListener(() => {
            // Unpauses to avoid a softlock
            GameObject.Find("Main Camera").GetComponent<CameraController>().isPaused = false;
            GameObject.Find("Inventory").GetComponent<TileInventory>().isPaused = false;

            // Skiddadles to the Main Menu
            LevelLoader.LoadSpecificScene("MainMenu");
        });
        Quit.onClick.AddListener(() => {
            // Closes the game
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
            Application.Quit();
        });
    }
}
