using System;
using System.Collections.Generic;
using UnityEngine;

public class MoveToIsle : MonoBehaviour
{
    [Range(0.1f, 5f)]
    public float speed = 1.0f;

    private List<Transform> tilesPosition = new();

    [NonSerialized] public bool InMove;

    Func<bool> eventCaller;


    public void GetPath()
    {
        GameManager manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        List<Transform> Path = manager._mostAccuratePath;

        if(Path == null)
        {
            return;
        }

        for (int i = Path.Count - 1; i >= 0; --i)
        {
            tilesPosition.Add(Path[i]);
        }

        manager._playerPosition = Path[0];

        eventCaller = () =>
        {
            Path[0].GetComponent<Tile>()._isPOI = false;
            Path[0].GetComponent<Event>().IslandEvent(GetComponent<Player>());
            if(manager._queue.Count > 0)
            {
                manager._queue[manager._queue.Count - 1]();
                return true;
            }
            else
            {
                manager._needQueuing = false;
                return false;
            }
        };

        GetComponent<Animator>().SetBool("IsMoving", true);

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
                    if (false == eventCaller())
                    {
                        eventCaller = null;
                        InMove = false;
                        GetComponent<Animator>().SetBool("IsMoving", false);
                    }
                    return;
                }
                else
                {
                    float angle = Vector2.SignedAngle(new Vector2(tilesPosition[0].position.x - transform.position.x, tilesPosition[0].position.z - transform.position.z), new Vector2(0, 1));

                    GetComponent<Player>().transform.rotation = Quaternion.Euler(0, angle, 0);
                    GetComponent<Player>().powerCanvas.localRotation = Quaternion.Euler(90, - transform.rotation.eulerAngles.y, 0);
                }
            }

            transform.position = Vector3.MoveTowards(transform.position, tilePositionNoY, speed * Time.deltaTime);
        }
    }
}

