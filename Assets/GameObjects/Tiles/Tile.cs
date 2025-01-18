using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LinkDirection;

public class Tile : MonoBehaviour
{
    [SerializeField] bool _top;
    [SerializeField] bool _topRight;
    [SerializeField] bool _botRight;
    [SerializeField] bool _bot;
    [SerializeField] bool _botLeft;
    [SerializeField] bool _topLeft;

    [SerializeField] public bool[] _directions = new bool[6];
    

    private void Start()
    {
        _directions[(int)TOP] = _top;
        _directions[(int)TOPLEFT] = _topLeft;
        _directions[(int)TOPRIGHT] = _topRight;
        _directions[(int)BOT] = _bot;
        _directions[(int)BOTLEFT] = _botLeft;
        _directions[(int)BOTRIGHT] = _botRight;
    }

    private void Update()
    {
        //if(Input.GetMouseButtonDown(0))
        //{
        //    if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100, -1))
        //    {
        //        if (hit.collider.gameObject == gameObject)
        //        {
        //            var test = GetLinkedTiles();
        //            print(test.Count);
        //        }
        //    }
        //}
    }

    public void RotateRight()
    {
        bool buf = _directions[0];
        for (int i = 0; i < _directions.Length - 1; i++)
        {
            int prev = i - 1;
            if (prev < 0) prev = _directions.Length - 1;
            _directions[prev] = _directions[i];
        }
        _directions[_directions.Length - 1] = buf;
    }

    public void RotateLeft()
    {
        bool buf = _directions[0];
        for (int i = 0; i < _directions.Length - 1; i++)
        {
            int next = i + 1;
            if (next > 6) next = _directions.Length - 1;
            _directions[next] = _directions[i];
        }
        _directions[_directions.Length - 1] = buf;
    }

    List<Tile> GetLinkedTiles()
    {
        List<Transform> found = new List<Transform>();
        Collider[] hit = Physics.OverlapSphere(transform.position, 1);
        foreach (Collider col in hit)
        {
            if (col.TryGetComponent<Tile>(out _))
            {
                found.Add(col.transform);
            }
        }

        Tile[] orderedFound = new Tile[6];
        
        foreach(Transform target in found)
        {
            if(Mathf.Approximately(transform.position.z, target.position.z) && transform.position.x < target.position.x)
            {
                orderedFound[0] = target.GetComponent<Tile>();
                break;
            }
        }

        foreach (Transform target in found)
        {
            if (transform.position.z < target.position.z && transform.position.x < target.position.x)
            {
                orderedFound[1] = target.GetComponent<Tile>();
                break;
            }
        }

        foreach (Transform target in found)
        {
            if (transform.position.z < target.position.z && transform.position.x > target.position.x)
            {
                orderedFound[2] = target.GetComponent<Tile>();
                break;
            }
        }

        foreach (Transform target in found)
        {
            if (Mathf.Approximately(transform.position.z, target.position.z) && transform.position.x > target.position.x)
            {
                orderedFound[3] = target.GetComponent<Tile>();
                break;
            }
        }

        foreach (Transform target in found)
        {
            if (transform.position.z > target.position.z && transform.position.x > target.position.x)
            {
                orderedFound[4] = target.GetComponent<Tile>();
                break;
            }
        }

        foreach (Transform target in found)
        {
            if (transform.position.z > target.position.z && transform.position.x < transform.position.x)
            {
                orderedFound[5] = target.GetComponent<Tile>();
                break;
            }
        }

        List<Tile> linked = new List<Tile>();
        for(int i=0; i< _directions.Length -1; i++)
        {
            if (orderedFound[i] == null) continue;
            if (_directions[i] && orderedFound[i]._directions[(i + 3) % 6])
            {
                linked.Add(orderedFound[i]);
            }
        }
        return linked;
    }

}
