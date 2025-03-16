using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Changeable dans les settings
    public float mouseDragAndDropSensitivity = 2.0f;

    const float movementSpeed = 8f;
    const float movementLerpSpeed = 4f;
    const float maxX = 15.0f;
    const float minX = -15.0f;

    const float maxY = 15.0f;
    const float minY = 5.0f;

    const float maxZ = 15.0f;
    const float minZ = -15.0f;

    [NonSerialized] public Vector3 targetPosition;

    const float zoomStrenght = 2.0f;

    public bool isPaused = false;


    void Start()
    {
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }

        if (!isPaused)
        { 
            if (Input.GetMouseButton(2) || Input.GetMouseButton(1))
            {
                Vector3 inputToCamera = new Vector3(Input.mousePositionDelta.x, 0, Input.mousePositionDelta.y);

                // THIS DOESN'T WORK WELL WITH OTHER INPUTS LIKE ZOOM
                //transform.position -= inputToCamera * Time.deltaTime * mouseDragAndDropSensitivity;
                //targetPosition = transform.position;

                targetPosition -= inputToCamera * Time.deltaTime * mouseDragAndDropSensitivity;

                Cursor.visible = false;
            }

            else
            {
                Cursor.visible = true;
            }

            if (Input.GetKey(KeyCode.W))
            {
                targetPosition += Vector3.forward * Time.deltaTime * movementSpeed;
            }

            if (Input.GetKey(KeyCode.S))
            {
                targetPosition += Vector3.back * Time.deltaTime * movementSpeed;
            }

            if (Input.GetKey(KeyCode.D))
            {
                targetPosition += Vector3.right * Time.deltaTime * movementSpeed;
            }

            if (Input.GetKey(KeyCode.A))
            {
                targetPosition += Vector3.left * Time.deltaTime * movementSpeed;
            }
        }

        targetPosition += Vector3.down * Input.mouseScrollDelta.y * zoomStrenght;

        // Clamp camera in game space
        targetPosition = new Vector3(
            Mathf.Clamp(targetPosition.x, minX, maxX),
            Mathf.Clamp(targetPosition.y, minY, maxY),
            Mathf.Clamp(targetPosition.z, minZ, maxZ));

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * movementLerpSpeed);
    }
}
