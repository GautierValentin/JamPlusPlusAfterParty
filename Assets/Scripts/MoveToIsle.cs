using System;
using System.Collections.Generic;
using UnityEngine;

public class MoveToIsle : MonoBehaviour
{
    [Range(0.1f, 5f)]
    public float speed = 1.0f;

    private List<Transform> tilesPosition = new();

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
            tilesPosition.Add(Path[i]);
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

            Vector3 tilePositionNoY = new Vector3(tilesPosition[0].position.x, transform.position.y, tilesPosition[0].position.z);

            if (Vector3.Distance(transform.position, tilePositionNoY) <= 0.005f)
            {
                tilesPosition.RemoveAt(0);

                if (tilesPosition.Count == 0)
                {
                    eventCaller();
                    eventCaller = null;
                    InMove = false;
                    return;
                }
                else
                {
                    float angle = Vector2.SignedAngle(new Vector2(tilesPosition[0].position.x - transform.position.x, tilesPosition[0].position.z - transform.position.z), new Vector2(0, 1));

                    GetComponent<Player>().transform.rotation = Quaternion.Euler(0, angle, 0);
                    GetComponent<Player>().powerCanvas.localRotation = Quaternion.Euler(90, - transform.rotation.eulerAngles.y, 0);
                }
            }

            transform.position = Vector3.MoveTowards(transform.position, new Vector3 (tilesPosition[0].position.x, transform.position.y, tilesPosition[0].position.z), speed * Time.deltaTime);

            //GameObject.Find("GameManager").GetComponent<GameManager>()._playerPosition = tilesPosition[0];
        }
    }
}

