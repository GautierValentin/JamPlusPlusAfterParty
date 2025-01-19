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

    private List<Vector3> tilesPosition = new();

    [NonSerialized] public bool InMove;

    Action eventCaller;


    public void GetPath()
    {
        List<Transform> Path = GameObject.Find("GameManager").GetComponent<GameManager>()._mostAccuratePath;

        if(Path == null)
        {
            return;
        }

        for (int i = Path.Count - 1; i >= 0; --i)
        {
            tilesPosition.Add(Path[i].position);
        }

        GameObject.Find("GameManager").GetComponent<GameManager>()._playerPosition = Path[0];

        eventCaller = () =>
        {
            Path[0].GetComponent<Tile>()._isPOI = false;
            Path[0].GetComponent<Event>().IslandEvent(GetComponent<Player>());
        };
        

        InMove = true;
    }

    void Update()
    {
        if (InMove)
        {
            float angle = Vector3.SignedAngle(transform.position, tilesPosition[0], new Vector3(0,1,0));

            GetComponent<Player>().lookDirection = LoopThroughKeyValuePairs(angle + 180);

            Vector3 tilePositionNoY = new Vector3(tilesPosition[0].x, transform.position.y, tilesPosition[0].z);

            if (Vector3.Distance(transform.position, tilePositionNoY) <= 0.005f)
                tilesPosition.RemoveAt(0);

            if (tilesPosition.Count == 0)
            {
                eventCaller();
                eventCaller = null;
                InMove = false;
                return;
            }

            transform.position = Vector3.MoveTowards(transform.position, new Vector3 (tilesPosition[0].x, transform.position.y, tilesPosition[0].z), speed * Time.deltaTime);

            //GameObject.Find("GameManager").GetComponent<GameManager>()._playerPosition = tilesPosition[0];
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

