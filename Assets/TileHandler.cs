using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TileHandler : MonoBehaviour
{
    // Can be used for placement logic
    int rotationOffset = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            RotateClockwise();
        }

        else if (Input.GetKeyDown(KeyCode.A))
        {
            RotateCounterClockwise();
        }
    }

    void RotateClockwise()
    {
        // Rotate clockwise (if 6 -> 0)
        rotationOffset++;
        rotationOffset = rotationOffset >= 6 ? 0 : rotationOffset;

        transform.Rotate(new Vector3(0, 60, 0));
    }

    void RotateCounterClockwise()
    {
        // Rotate counter clockwise (if -1 -> 5)
        rotationOffset--;
        rotationOffset = rotationOffset <= -1 ? 5 : rotationOffset;

        transform.Rotate(new Vector3(0, -60, 0));
    }
}
