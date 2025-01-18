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
    [SerializeField] int curLevel;
    [SerializeField] int levelCount = 10;
    [SerializeField] bool isSingleLevel = false;


    ///  -----------
    /// CLASS METHODS
    ///  -----------
    // Setters
    public void SetIsSingleLevel(bool ARGvalue)
    {
        isSingleLevel = ARGvalue;
    }

    // Scene loading & Cie
    public void LoadSpecificScene(string ARGsceneName, bool ARGshouldLoadAdditively = false)
    {
        SceneManager.LoadScene(ARGsceneName, ARGshouldLoadAdditively ? LoadSceneMode.Additive : LoadSceneMode.Single);
    }
    public void LoadSpecificLevel(int ARGlevelId)
    {
        SceneManager.LoadScene("Level " + ARGlevelId, LoadSceneMode.Single);
        curLevel = ARGlevelId;
    }
    public void LoadNextLevel()
    {
        // Single Level case
        if (isSingleLevel)  LoadSpecificScene("MainMenu");

        // Regular playthrough
        int nextLevel = curLevel + 1;
        if (nextLevel <= levelCount)  LoadSpecificLevel(nextLevel);
        else
            Debug.Log("Can't load next level : There is no next level !");
    }
    public void UnloadSpecificScene(string ARGsceneName)
    {
        SceneManager.UnloadSceneAsync(ARGsceneName);
    }


    ///  -------------
    /// TEMPORARY STUFF
    ///  -------------
    void Update()
    {
        // C key
        if (Input.GetKeyDown(KeyCode.C))
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
