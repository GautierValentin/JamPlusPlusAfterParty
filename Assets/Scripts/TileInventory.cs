using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class TileInventory : MonoBehaviour
{
    public Transform mainCameraTransform;
    public float distanceFromMainCamera;
    Vector3 inventoryOffset;
    Vector3 powerOffset;

    [SerializeField] PowerManager powerManager;

    public Dictionary<Vector3Int, GameObject> tilesInGrid = new();

    List<TileHandler> tileScriptsInHand = new();
    List<GameObject> tilesInHand = new();
    int handSize;

    [SerializeField]
    Grid grid;

    public bool canSelect = true;

    public bool isTileSelected;
    GameObject tileSelected;

    public GameObject tile1;
    public GameObject tile2;
    public GameObject tile3;
    public GameObject tile4;
    public GameObject tile5;

    [SerializeField] List<GameObject> _startingHand;

    void Start()
    {
        handSize = 0;

        inventoryOffset = new Vector3(0, 0, -1.25f);
        distanceFromMainCamera = 2;

        foreach (var tile in _startingHand)
        {
            AddTile(tile);
        }

        int baseTileAmount = grid.transform.GetChild(0).transform.childCount;
        for (int i = 0; i < baseTileAmount; i++)
        {
            TileHandler baseTile = grid.transform.GetChild(0).transform.GetChild(i).gameObject.GetComponent<TileHandler>();

            baseTile.Place();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 resultingPosition = mainCameraTransform.position + mainCameraTransform.forward * distanceFromMainCamera;
        transform.position = resultingPosition;
        transform.eulerAngles = mainCameraTransform.rotation.eulerAngles + new Vector3(-90, 0, 0);

        // Debug //
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            AddTile(tile1);
        }

        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            AddTile(tile2);
        }

        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            AddTile(tile3);
        }

        else if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            AddTile(tile4);
        }

        else if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            AddTile(tile5);
        }

        else if (Input.GetKeyDown(KeyCode.K))
        {
            RemoveTile(0);
        }
        // Debug End //

        if(canSelect && false == GameObject.Find("Player").GetComponent<MoveToIsle>().InMove)
        {
            if (!isTileSelected)
            {
                for (int i = 0; i < handSize; i++)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hitTile;

                    if (Physics.Raycast(ray, out hitTile, 100, 1 << 6))
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            for (int j = 0; j < handSize; j++)
                            {
                                tileScriptsInHand[i].selected = false;
                            }

                            if (hitTile.collider.gameObject.GetComponent<TileHandler>().pickable)
                            {
                                isTileSelected = true;
                                hitTile.collider.gameObject.GetComponent<TileHandler>().selected = true;
                                tileSelected = hitTile.collider.gameObject;
                                tileSelected.transform.rotation = Quaternion.identity;
                            }
                        }

                        else
                        {
                            hitTile.collider.gameObject.GetComponent<TileHandler>().hovered = true;
                        }
                    }
                }
            }

            else
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitBoard;

                if (Physics.Raycast(ray, out hitBoard, 100, 1 << 7))
                {
                    Vector3Int gridCoord = grid.WorldToCell(hitBoard.point);

                    tileSelected.transform.position = grid.CellToWorld(gridCoord);
                }

                if (Input.GetMouseButtonDown(0))
                {
                    // Check if on valid slot and place here
                    Vector3Int gridCoord = grid.WorldToCell(hitBoard.point);

                    if (!tilesInGrid.ContainsKey(gridCoord))
                    {
                        tileSelected.GetComponent<TileHandler>().Place();

                        tileSelected.transform.parent = null;
                        RemoveTile(tilesInHand.IndexOf(tileSelected));

                        GameObject.Find("GameManager").GetComponent<GameManager>()._mostAccuratePath = null;

                        tileSelected.GetComponent<Tile>().OnSet();

                        GameObject.Find("Player").GetComponent<MoveToIsle>().GetPath();

                        tileSelected = null;
                        isTileSelected = false;
                    }
                }

                else if (Input.GetKeyDown(KeyCode.E))
                {
                    tileSelected.GetComponent<TileHandler>().RotateClockwise();
                }

                else if (Input.GetKeyDown(KeyCode.Q))
                {
                    tileSelected.GetComponent<TileHandler>().RotateCounterClockwise();
                }

                if (Input.GetMouseButtonDown(1))
                {
                    tileSelected = null;
                    isTileSelected = false;
                }
            }

        
        }

        for (int i = 0; i < handSize; i++)
        {
            if (tilesInHand[i] != tileSelected)
            {
                Vector3 hoveredOffset;
                Vector3 offsetInHand;

                offsetInHand = ((float)i - (float)handSize / 2 + 0.5f) * new Vector3(0.5f, 0, 0);

                if (tileScriptsInHand[i].hovered)
                {
                    hoveredOffset = new Vector3(0, 0.2f, 0.2f);
                }

                else
                {
                    hoveredOffset = Vector3.zero;
                }

                if (powerManager.currentPower != UsingPower.NONE)
                {
                    powerOffset = new Vector3(0, 0, -5);
                }

                else
                {
                    powerOffset = Vector3.zero;
                }

                tilesInHand[i].transform.localPosition = Vector3.Lerp(tilesInHand[i].transform.localPosition, inventoryOffset + offsetInHand + hoveredOffset + powerOffset, 4 * Time.deltaTime);
                tilesInHand[i].transform.localRotation = Quaternion.identity;

                tilesInHand[i].transform.localScale = Vector3.one * 0.15f;
            }

            else
            {
                tilesInHand[i].transform.localScale = new Vector3(.58f, .5f, .5f);
            }

            tileScriptsInHand[i].hovered = false;
        }
    }


    public void AddTile(GameObject tile)
    {
        GameObject newTile = Instantiate(tile, Vector3.zero, Quaternion.identity);

        newTile.transform.parent = transform;
        newTile.transform.localPosition = inventoryOffset + ((float)handSize / 2 + 0.5f) * new Vector3(0.5f, 0, 0);
        newTile.transform.localRotation = Quaternion.identity;

        tilesInHand.Add(newTile);
        tileScriptsInHand.Add(newTile.GetComponent<TileHandler>());

        handSize++;
    }

    void RemoveTile(int index)
    {
        tilesInHand.RemoveAt(index);
        tileScriptsInHand.RemoveAt(index);
        handSize--;
    }

    public void SetTilePlaced(GameObject tile)
    {
        Vector3Int gridCoord = grid.WorldToCell(tile.transform.position);

        tilesInGrid.Add(gridCoord, tile);

        tile.GetComponent<TileHandler>().pickable = false;
    }
}
