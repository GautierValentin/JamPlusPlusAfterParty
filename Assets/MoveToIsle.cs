using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveToIsle : MonoBehaviour
{
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
            if (transform.position == tilesPosition[0])
                tilesPosition.RemoveAt(0);

            if (tilesPosition.Count == 0)
            {
                InMove = false;
                return;
            }

            transform.position = Vector3.MoveTowards(transform.position, tilesPosition[0], speed * Time.deltaTime);
        }
    }
}
