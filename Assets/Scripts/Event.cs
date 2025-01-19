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

    public Chest chest;

    public List<GameObject> tiles;


    [SerializeField] TextMeshProUGUI txtComp;


    TileInventory inventory;

    private void Start()
    {
        inventory = GameObject.Find("Inventory").GetComponent<TileInventory>();
        if (txtComp != null)
            txtComp.text = Enemy.attackPower.ToString();
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

        if(chest != null)
            chest.OpenChest();

        player.AddAttackPower(Enemy.amount);

        for (int i = 0; i < tiles.Count; i++)
        {
            inventory.AddTile(tiles[i]);
        }
    }
}
