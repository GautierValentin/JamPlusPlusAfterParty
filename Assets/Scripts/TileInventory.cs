using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class TileInventory : MonoBehaviour
{
    public Transform mainCameraTransform;
    public float distanceFromMainCamera;
    Vector3 inventoryOffset;

    List<TileHandler> tileScriptsInHand = new();
    List<GameObject> tilesInHand = new();
    int handSize;

    bool isTileSelected;
    GameObject tileSelected;

    public GameObject tile;

    void Start()
    {
        handSize = 0;

        inventoryOffset = new Vector3(0, 0, -2);
        distanceFromMainCamera = 5;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 resultingPosition = mainCameraTransform.position + mainCameraTransform.forward * distanceFromMainCamera;
        transform.position = resultingPosition;
        transform.eulerAngles = mainCameraTransform.rotation.eulerAngles + new Vector3(-90, 0, 0);

        // Debug //
        if (Input.GetKeyDown(KeyCode.J))
        {
            AddTile(tile);
        }

        else if (Input.GetKeyDown(KeyCode.K))
        {
            RemoveTile(0);
        }
        // Debug End //

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

                        Debug.Log("Selected");
                        isTileSelected = true;
                        hitTile.collider.gameObject.GetComponent<TileHandler>().selected = true;
                        tileSelected = hitTile.collider.gameObject;
                        tileSelected.transform.rotation = Quaternion.identity;
                    }
                    else
                    {
                        hitTile.collider.gameObject.GetComponent<TileHandler>().hovered = true;
                        tileSelected = null;
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
                tileSelected.transform.position = hitBoard.point + new Vector3(0.5f, 0.5f, 0);
            }

            if (Input.GetMouseButtonDown(0))
            {
                // Check if on valid slot and place here

                Debug.Log("Unselected");
                tileSelected.GetComponent<TileHandler>().selected = false;
                isTileSelected = false;
            }

            else if (Input.GetKeyDown(KeyCode.E))
            {
                tileSelected.GetComponent<TileHandler>().RotateClockwise();
            }

            else if (Input.GetKeyDown(KeyCode.A))
            {
                tileSelected.GetComponent<TileHandler>().RotateCounterClockwise();
            }
        }

        for (int i = 0; i < handSize; i++)
        {
            if (tilesInHand[i] != tileSelected)
            {
                Vector3 hoveredOffset;
                Vector3 offsetInHand;

                offsetInHand = ((float)i - (float)handSize / 2 + 0.5f) * new Vector3(3, 0, 0);

                if (tileScriptsInHand[i].hovered)
                {
                    hoveredOffset = new Vector3(0, 0.2f, 0.2f);
                }

                else
                {
                    hoveredOffset = Vector3.zero;
                }

                tilesInHand[i].transform.localPosition = Vector3.Lerp(tilesInHand[i].transform.localPosition, inventoryOffset + offsetInHand + hoveredOffset, 4 * Time.deltaTime);
                tilesInHand[i].transform.localRotation = Quaternion.identity;
            }
        }
    }


    void AddTile(GameObject tile)
    {
        GameObject newTile = Instantiate(tile, Vector3.zero, Quaternion.identity);

        newTile.transform.parent = transform;
        newTile.transform.localPosition = inventoryOffset + ((float)handSize / 2 + 0.5f) * new Vector3(3, 0, 0);
        newTile.transform.localRotation = Quaternion.identity;

        tilesInHand.Add(newTile);
        tileScriptsInHand.Add(newTile.GetComponent<TileHandler>());

        handSize++;
        Console.WriteLine("Added tile");
    }

    void RemoveTile(int index)
    {
        Destroy(tilesInHand[index]);
        tilesInHand.RemoveAt(index);
        tileScriptsInHand.RemoveAt(index);
        handSize--;
    }
}
