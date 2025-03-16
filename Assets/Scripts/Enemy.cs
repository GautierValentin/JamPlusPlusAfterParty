using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : Character
{
    ///  ----
    /// FIELDS
    ///  ----
    public int powerReward;
    public int tileReward;
    public bool isEnemyTheBoss;

    public Transform rewardCanvas;

    [SerializeField] TextMeshProUGUI powerText;
    [SerializeField] TextMeshProUGUI powerRewardText;
    [SerializeField] TextMeshProUGUI tileRewardText;


    ///  -----------
    /// UNITY METHODS
    ///  -----------
    public override void Start()
    {
        powerText.text = attackPower.ToString();

        if (!isEnemyTheBoss)
        {
            powerRewardText.text = "+ " + powerReward.ToString();
            tileRewardText.text = "+ " + tileReward.ToString();
            GetComponent<Animator>().Play("Enemy_Idle");
        }
        else
        {
            powerRewardText.text = "";
            tileRewardText.text = "";
        }


        base.Start();

        powerCanvas.localRotation = Quaternion.Euler(90, -dictDirection[lookDirection], 0);
    }

    public void CommitSuicide()
    {
        Destroy(gameObject);
        if (isEnemyTheBoss)
        {
            GameObject.Find("Level Loader").GetComponent<LevelLoader>().LoadNextLevel();
            GameObject.Find("Main Camera").GetComponent<AudioScript>().PlayClip(SFXClips.VICTORY);

            Scene[] loadedScenes = SceneManager.GetAllScenes();
            foreach (Scene curScene in loadedScenes)
            {
                if (curScene.name == "Level 4")
                {
                    // Pauses the game
                    GameObject.Find("Main Camera").GetComponent<CameraController>().isPaused = false;
                    GameObject.Find("Inventory").GetComponent<TileInventory>().isPaused = false;

                    GameObject.Find("Level Loader").GetComponent<LevelLoader>().LoadSpecificScene("VictoryScreen", true);

                    break;
                }
            }
        }
    }

    public void PlaySkeletonDeathSFX()
    {
        GameObject.Find("Main Camera").GetComponent<AudioScript>().PlayClip(SFXClips.BOSSDEATH);
    }
}
