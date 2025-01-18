using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// In the EntryPoint scene only ! Place every DontDestroyOnLoad objects there, so we can make sure we load them exactly once.
public class EntryPointLoader : MonoBehaviour
{
    ///  ----
    /// FIELDS
    ///  ----
    private LevelLoader LevelLoader;


    ///  -----------
    /// UNITY METHODS
    ///  -----------
    void Awake()
    {
        LevelLoader = GameObject.Find("Level Loader").GetComponent<LevelLoader>();
    }
    // DO NOT change this to Awake, since this could prevent some GOs to become undestructible on loads and break a ton of stuff possibly.
    void Start()
    {
        LevelLoader.LoadSpecificScene("MainMenu");
    }
}
