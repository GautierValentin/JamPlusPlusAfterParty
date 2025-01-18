using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleLevelButton : MonoBehaviour
{
    ///  ----
    /// FIELDS
    ///  ----
    private static LevelLoader LevelLoader;
    [SerializeField] int levelId;


    ///  -----------
    /// UNITY METHODS
    ///  -----------
    void Awake()
    {
        if (LevelLoader == null)  LevelLoader = GameObject.Find("Level Loader").GetComponent<LevelLoader>();
    }


    ///  -----------
    /// CLASS METHODS
    ///  -----------
    public void LoadSingleLevel()
    {
        LevelLoader.SetIsSingleLevel(true);
        LevelLoader.LoadSpecificLevel(levelId);
    }
}
