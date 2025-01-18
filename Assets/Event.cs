using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Event : MonoBehaviour
{
    public Enemy Enemy;

    public Chest chest;

    public int amount = 0;

    public TileInventory inventory;

    public List<GameObject> tiles;

    public bool isEnemyTheBoss = false;

    public void IslandEvent(Player player)
    {
        if (Enemy != null)
        {
            Enemy.transform.LookAt(player.transform.position);

            if (Enemy.attackPower > player.attackPower)
            {
                player.ChangeAnimation("Player_Death");
                //Game Over : LoadSceneAsync("GameOverScene");
                return;
            }

            Enemy.ChangeAnimation("Enemy_Death");
            if (isEnemyTheBoss)
            {
                // LoadSceneAsync("WinScene");
                return;
            }
        }

        chest.OpenChest();

        player.AddAttackPower(amount);

        for (int i = 0; i < tiles.Count; i++)
        {
            inventory.AddTile(tiles[i]);
        }
    }
}
