using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    ///
    /// INTERNAL STUFF
    ///
    private enum SpecialLevelValues : int
    {
        MainMenu = -100,
        LevelSelect,
        VictoryScreen,
    }



    ///  ----
    /// FIELDS
    ///  ----
    [SerializeField] private int curLevel;
    [SerializeField] private int levelCount = 10;


    ///  -----------
    /// CLASS METHODS
    ///  -----------
    public void LoadSpecificScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
    public void LoadSpecificLevel(int levelId)
    {
        SceneManager.LoadScene("Level " + levelId, LoadSceneMode.Single);
        curLevel = levelId;
    }
    public void LoadNextLevel()
    {
        int nextLevel = curLevel + 1;
        Debug.Log("Attempting to load level " + nextLevel);

        Debug.Log(nextLevel + " > " + levelCount + " = " + (nextLevel > levelCount));
        if (nextLevel <= levelCount)
            LoadSpecificLevel(nextLevel);
        else
            Debug.Log("Can't load next level : There is no next level !");
    }


    ///  -----------
    /// UNITY METHODS
    ///  -----------
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    ///  -------------
    /// TEMPORARY STUFF
    ///  -------------
    void Update()
    {
        // Left Click
        if (Input.GetMouseButtonDown(0))
        {
            LoadNextLevel();
        }

        // Space Bar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadSpecificScene("Level 1");
        }
    }
}
