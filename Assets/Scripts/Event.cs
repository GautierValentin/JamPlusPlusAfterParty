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

            if (Enemy.attackPower > attackpower)
            {
                player.ChangeAnimation("Player_Death");
                //Game Over : LoadSceneAsync("GameOverScene");
                return;
            }

            Enemy.ChangeAnimation("Enemy_Death");
            if (Enemy.isEnemyTheBoss)
            {
                //LoadSceneAsync("WinScene");
                return;
            }
        }

        if(Chest != null)
            Chest.OpenChest();

        player.AddAttackPower(Enemy.powerReward);

        for (int i = 0; i < tiles.Count; i++)
        {
            Inventory.AddTile(tiles[i]);
        }
    }
}
