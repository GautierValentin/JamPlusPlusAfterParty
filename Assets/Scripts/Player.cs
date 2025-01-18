using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] Tile startingTile;

    public override void Start()
    {
        GetComponent<Animator>().Play("Player_Idle");
        GameObject.Find("GameManager").GetComponent<GameManager>()._playerPosition = startingTile.transform;
        GameObject.Find("GameManager").GetComponent<GameManager>()._accessibleTiles.Add(startingTile.transform);
    }

    // Update is called once per fram
    public  override void Update()
    {

    }
}
