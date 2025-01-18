using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveToIsle : MonoBehaviour
{
    public Transform player;

    [Range(0.1f, 2f)]
    public float speed = 1.0f;

    private List<Vector3> tilesPosition;

    private bool InMove;

    public void AddTilePosition(Vector3 tilePosition)
    {
        tilesPosition.Add(tilePosition);
    }

    public void StartMovements()
    {
        InMove = true;
    }

    void Update()
    {
        if (InMove)
        {
            if (player.position == tilesPosition[0])
                tilesPosition.RemoveAt(0);

            if (tilesPosition.Count == 0)
            {
                InMove = false;
                return;
            }

           player.position = Vector3.MoveTowards(player.position, tilesPosition[0], speed * Time.deltaTime);
        }
    }
}
