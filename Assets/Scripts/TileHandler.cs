using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class TileHandler : MonoBehaviour
{
    [SerializeField]
    Tile tileScript;

    // Can be used for placement logic
    public int rotationOffset = 0;

    Quaternion targetRotation;
    public bool hovered;
    public bool selected;

    public bool pickable = true;

    TileInventory inventory;

    const float rotationSpeed = 8.0f;

    void Start()
    {
        hovered = false;
        selected = false;
        targetRotation = transform.localRotation;
        inventory = GameObject.Find("Inventory").GetComponent<TileInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    public void RotateClockwise()
    {
        // Rotate clockwise (if 6 -> 0)
        rotationOffset++;
        rotationOffset = rotationOffset >= 6 ? 0 : rotationOffset;

        targetRotation *= Quaternion.Euler(Vector3.up * 60);

        tileScript.RotateRight();
    }

    public void RotateCounterClockwise()
    {
        // Rotate counter clockwise (if -1 -> 5)
        rotationOffset--;
        rotationOffset = rotationOffset <= -1 ? 5 : rotationOffset;

        targetRotation *= Quaternion.Euler(Vector3.up * -60);

        tileScript.RotateLeft();
    }

    public void Place()
    {
        pickable = false;
        selected = false;

        Transform model = transform.GetChild(0);

        // Single model
        if (model.GetComponent<MeshRenderer>() != null)
        {
            model.GetComponent<MeshRenderer>().shadowCastingMode = ShadowCastingMode.On;
        }

        // Multiple models
        else
        {
            int modelChildCount = model.transform.childCount;
            for (int i = 0; i < modelChildCount; i++)
            {
                model.transform.GetChild(i).GetComponent<MeshRenderer>().shadowCastingMode = ShadowCastingMode.On;     
            }
        }

        inventory.SetTilePlaced(gameObject);
    }
}
