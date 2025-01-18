using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    const float rotationSpeed = 8.0f;

    void Start()
    {
        hovered = false;
        selected = false;
        targetRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    public void RotateClockwise()
    {
        // Rotate clockwise (if 6 -> 0)
        rotationOffset++;
        rotationOffset = rotationOffset >= 6 ? 0 : rotationOffset;

        targetRotation*= Quaternion.Euler(Vector3.up * 60);

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
}
