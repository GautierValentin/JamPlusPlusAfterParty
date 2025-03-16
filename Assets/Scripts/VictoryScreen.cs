using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
    ///  ----
    /// FIELDS
    ///  ----
    private LevelLoader LevelLoader;



    ///  -----------
    /// UNITY METHODS
    ///  -----------
    void Start()
    {
        LevelLoader = GameObject.Find("Level Loader").GetComponent<LevelLoader>();
    }

    public void CloseVictoryScreenpt()
    {
        LevelLoader.UnloadSpecificScene("VictoryScreen");
    }
}
