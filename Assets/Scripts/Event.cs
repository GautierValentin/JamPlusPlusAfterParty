using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Event : MonoBehaviour
{
    public Enemy Enemy;
    public Chest Chest;
    public List<GameObject> tiles = new();
    TileInventory Inventory;

    private AudioScript AudioScript;


    private void Awake()
    {
        if (Enemy != null)  Enemy.tileReward = tiles.Count;
    }
    private void Start()
    {
        Inventory = GameObject.Find("Inventory").GetComponent<TileInventory>();
        AudioScript = GameObject.Find("Main Camera").GetComponent<AudioScript>();
    }

    public void IslandEvent(Player player)
    {
        bool gameover = false;
        
        if (Enemy != null)
        {
            AudioScript.PlayClip(SFXClips.ATTACK);

            int attackpower = player.attackPower;

            if (Mathf.Abs(player.transform.rotation.eulerAngles.y - Enemy.transform.rotation.eulerAngles.y) <= 1)
            {
               Debug.Log("x2 damage");
               attackpower *= 2;
            }

            if (Enemy.attackPower > attackpower)
            {
                player.GetComponent<Animator>().SetBool("IsDead", true);
                gameover = true;
            }
            else
            {
                Enemy.GetComponent<Animator>().SetBool("IsDead", true);
            }

            Enemy.transform.rotation = Quaternion.Euler(0, player.transform.rotation.eulerAngles.y + 180, 0);
            Enemy.powerCanvas.localRotation = Quaternion.Euler(90, -Enemy.transform.rotation.eulerAngles.y, 0);
            Enemy.rewardCanvas.localRotation = Quaternion.Euler(90, -Enemy.transform.rotation.eulerAngles.y, 0);

            Enemy.GetComponent<Animator>().SetTrigger("Battle");
            player.GetComponent<Animator>().SetTrigger("Battle");

            if (Enemy.isEnemyTheBoss)
            {
                //LoadSceneAsync("WinScene");
                AudioScript.PlayClip(SFXClips.VICTORY);
                return;
            }
        }


        if (gameover)
        {
            Destroy(player);
        }
        else
        {
             if(Chest != null)
            {
                AudioScript.PlayClip(SFXClips.LOOT);
                Chest.OpenChest();
            }

            if (Enemy != null)
            {
                AudioScript.PlayClip(SFXClips.LOOT);
                player.AddAttackPower(Enemy.powerReward);
                Destroy(Enemy.gameObject);
            }

            if (tiles == null) return;

            for (int i = 0; i < tiles.Count; i++)
            {
                Inventory.AddTile(tiles[i]);
            }
        }
    }
}
