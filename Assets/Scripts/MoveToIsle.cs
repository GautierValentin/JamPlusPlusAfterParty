using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class MoveToIsle : MonoBehaviour
{
    [Range(0.1f, 5f)]
    public float speed = 1.0f;

    private List<Vector3> tilesPosition;

    private bool InMove;

    public void GetPath()
    {
        List<Transform> Path = GameObject.Find("GameManager").GetComponent<GameManager>()._mostAccuratePath;

        for (int i = 0; i < Path.Count; i++)
        {
            tilesPosition.Add(Path[i].position);
        }

        InMove = true;
    }

    void Update()
    {
        if (InMove)
        {
            float angle = Vector3.SignedAngle(transform.position, tilesPosition[0], new Vector3(0,1,0));

            GetComponent<Player>().lookDirection = LoopThroughKeyValuePairs(angle + 180);

            if (transform.position.x == tilesPosition[0].x && transform.position.y == tilesPosition[0].y)
                tilesPosition.RemoveAt(0);

            if (tilesPosition.Count == 0)
            {
                InMove = false;
                return;
            }

            Vector3 newPos = Vector3.MoveTowards(transform.position, tilesPosition[0], speed * Time.deltaTime);
            newPos.z = transform.position.z;

            transform.position = newPos;
        }
    }

    public LinkDirection LoopThroughKeyValuePairs(float value)
    {
        foreach (var keyValuePair in GetComponent<Player>().GetDirections())
        {
            if (keyValuePair.Value == value)
            {
                return keyValuePair.Key;
            }
        }
        return default;
    }
}

