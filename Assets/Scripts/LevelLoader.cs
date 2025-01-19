using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    // Possibly Useless
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
    [SerializeField] GameObject Camera;


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

        // Re-positions the Camera to be front-facing
        Camera.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
    public void LoadSpecificLevel(int ARGlevelId)
    {
        SceneManager.LoadScene("Level " + ARGlevelId, LoadSceneMode.Single);
        curLevel = ARGlevelId;

        // Re-positions the Camera to be top-down
        Camera.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
    }
    public void LoadNextLevel()
    {
        // Single Level case
        if (isSingleLevel)
        {
            LoadSpecificScene("LevelSelectionMenu");
            return;
        }

        // Regular playthrough
        int nextLevel = curLevel + 1;
        if (nextLevel <= levelCount)
            LoadSpecificLevel(nextLevel);
        else
            LoadSpecificScene("MainMenu");
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
        // Debug to next level
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadNextLevel();
        }
    }
}
