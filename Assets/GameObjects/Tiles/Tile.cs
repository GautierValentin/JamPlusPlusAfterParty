using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LinkDirection;

public class Tile : MonoBehaviour
{
    [Header("Accessible directions")]
    [SerializeField] bool _top;
    [SerializeField] bool _topRight;
    [SerializeField] bool _botRight;
    [SerializeField] bool _bot;
    [SerializeField] bool _botLeft;
    [SerializeField] bool _topLeft;

    [Header("It is a point of interrest if it contains something (loot / monster)")]
    [SerializeField] bool _isPOI;

    [NonSerialized] public bool[] _directions = new bool[6];
    

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

        //DEBUG//
        //if (Input.GetMouseButtonDown(0))
        //{
        //    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100, -1))
        //    {
        //        if (hit.collider.gameObject == gameObject)
        //        {
        //            OnSet();
        //            print("onset");
        //        }
        //    }
        //}
        //if (Input.GetMouseButtonDown(1))
        //{
        //    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100, -1))
        //    {
        //        if (hit.collider.gameObject == gameObject)
        //        {
        //            GameObject.Find("GameManager").GetComponent<GameManager>()._playerPosition = transform;
        //            print("locjed");
        //        }
        //    }
        //}
        //if (Input.GetMouseButtonDown(2))
        //{
        //    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100, -1))
        //    {
        //        if (hit.collider.gameObject == gameObject)
        //        {
        //            GameObject.Find("GameManager").GetComponent<GameManager>()._accessibleTiles.Add(transform);
        //            print("locjed222");
        //        }
        //    }
        //}
        //DEBUG END//
    }

    public void RotateRight()
    {
        bool buf = _directions[_directions.Length - 1];
        for (int i = _directions.Length - 1; i > 0; i--)
        {
            int prev = i - 1;
            _directions[i] = _directions[prev];
        }
        _directions[0] = buf;
    }

    public void RotateLeft()
    {
        bool buf = _directions[0];
        for (int i = 0; i < _directions.Length - 1; i++)
        {
            int next = i + 1;
            _directions[i] = _directions[next];
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

    public void OnSet()
    {
        GameManager manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        List<Tile> linked = GetLinkedTiles();

        foreach (Tile tile in linked)
        {
            // check if youi are adjacent to an accessible cell
            if(manager._accessibleTiles.Contains(tile.transform))
            {
                // if so, you get yourself an accessible tile
                manager._accessibleTiles.Add(transform);
                if (_isPOI)
                {
                    manager._mostAccuratePath = new();

                    Debug.Log("Hey there's an accessible point of interest");

                    List<Transform> arg = new();
                    arg.Add(tile.transform);
                    PathFind(arg);
                    
                }
                else
                {
                    // if you're not a POI, then you check for setting your niehbour accessible
                    foreach(Tile neighbour in linked)
                    {
                        if(false == manager._accessibleTiles.Contains(neighbour.transform))
                        {
                            neighbour.OnSet();
                        }
                    }
                }
                break;
            }
        }
    }

    void PathFind(List<Transform> list)
    {
        GameManager manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        list.Add(transform);

        if (transform == manager._playerPosition)
        {
            print("GGGG");
            
            if(manager._mostAccuratePath.Count >= list.Count || manager._mostAccuratePath.Count == 0)
            {
                manager._mostAccuratePath = list;
                print("most accurate is : " + list);
            }
            return;
        }

        List<Tile> linked = GetLinkedTiles();

        foreach (Tile tile in linked)
        {
            if(false == list.Contains(tile.transform))
            {
                tile.PathFind(list);
            }
        }
    }
}
