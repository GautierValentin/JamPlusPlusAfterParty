using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Event : MonoBehaviour
{
    public Enemy Enemy;
    public Chest Chest;
    public List<GameObject> tiles;
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
            Dictionary<LinkDirection, float> dictdirections = Enemy.GetDirections();

            AudioScript.PlayClip(SFXClips.ATTACK);

            int attackpower = player.attackPower;

            if (dictdirections[player.lookDirection] == dictdirections[Enemy.lookDirection])
            {
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

            Enemy.transform.LookAt(player.transform.position);

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
            //Game Over : LoadSceneSync("GameOverScene");
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
            }

            if (tiles == null) return;

            for (int i = 0; i < tiles.Count; i++)
            {
                Inventory.AddTile(tiles[i]);
            }
        }
    }
}
