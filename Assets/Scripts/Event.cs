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


    private void Awake()
    {
        if (Enemy != null)  Enemy.tileReward = tiles.Count;
    }
    private void Start()
    {
        Inventory = GameObject.Find("Inventory").GetComponent<TileInventory>();
    }

    public void IslandEvent(Player player)
    {
        if (Enemy != null)
        {
            Dictionary<LinkDirection, float> dictdirections = Enemy.GetDirections();

            int attackpower = player.attackPower;

            if (dictdirections[player.lookDirection] == dictdirections[Enemy.lookDirection])
            {
               attackpower *= 2;
            }

            Enemy.transform.LookAt(player.transform.position);

            Enemy.GetComponent<Animator>().SetTrigger("Battle");
            player.GetComponent<Animator>().SetTrigger("Battle");

            if (Enemy.attackPower > attackpower)
            {
                player.GetComponent<Animator>().SetBool("IsDead", true);
                //Game Over : LoadSceneAsync("GameOverScene");
                return;
            }

            Enemy.GetComponent<Animator>().SetBool("IsDead", true);

            if (Enemy.isEnemyTheBoss)
            {
                //LoadSceneAsync("WinScene");
                return;
            }
        }

        if(Chest != null)
            Chest.OpenChest();

        if(Enemy != null)
        {
            player.AddAttackPower(Enemy.powerReward);
        }

        if(tiles == null) return;

        for (int i = 0; i < tiles.Count; i++)
        {
            Inventory.AddTile(tiles[i]);
        }
    }
}
