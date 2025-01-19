using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : Character
{
    [SerializeField] Tile startingTile;
    [SerializeField] TextMeshProUGUI txtComp;

    public override void Start()
    {
        //GetComponent<Animator>().Play("Player_Idle");
        GameObject.Find("GameManager").GetComponent<GameManager>()._playerPosition = startingTile.transform;
        GameObject.Find("GameManager").GetComponent<GameManager>()._accessibleTiles.Add(startingTile.transform);
    }

    void FixedUpdate()
    {
        txtComp.text = attackPower.ToString();
    }
}
