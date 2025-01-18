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


<<<<<<< HEAD

    ///  ----
    /// FIELDS
    ///  ----
    [SerializeField] private int curLevel;
    [SerializeField] private int levelCount = 10;
=======
    ///  ----
    /// FIELDS
    ///  ----
    [SerializeField] int curLevel;
    [SerializeField] int levelCount = 10;
    [SerializeField] bool isSingleLevel = false;
>>>>>>> origin/UI_Galore


    ///  -----------
    /// CLASS METHODS
    ///  -----------
<<<<<<< HEAD
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
=======
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
>>>>>>> origin/UI_Galore
    }


    ///  -------------
    /// TEMPORARY STUFF
    ///  -------------
    void Update()
    {
<<<<<<< HEAD
        // Left Click
        if (Input.GetMouseButtonDown(0))
=======
        // C key
        if (Input.GetKeyDown(KeyCode.C))
>>>>>>> origin/UI_Galore
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
