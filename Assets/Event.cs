using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour
{

    public Enemy Enemy;

    public int amount;

    public TileInventory inventory;

    public List<Tile> tiles;

    public void IslandEvent(Player player)
    {
        if (Enemy != null)
        {

            if (Enemy.attackPower > player.attackPower)
            {
                //Game Over
            }

            //Enemy die
            Destroy(Enemy);
        }

        player.AddAttackPower(amount);

        for (int i = 0; i < tiles.Count; i++)
        {
            //inventory.Add(tiles[i]);
        }
    }

}
