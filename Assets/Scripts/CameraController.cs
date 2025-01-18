using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    const float movementSpeed = 8f;
    const float movementLerpSpeed = 4f;
    const float maxX = 15.0f;
    const float minX = -15.0f;
    const float maxZ = 15.0f;
    const float minZ = -15.0f;

    Vector3 targetPosition;

    float targetFov = 50.0f;
    const float minFov = 30;
    const float maxFov = 80;
    const float zoomLerpSpeed = 4.0f;
    const float zoomStrenght = 2.0f;


    void Start()
    {
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.UpArrow))
        {
            targetPosition += Vector3.forward * Time.deltaTime * movementSpeed;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            targetPosition += Vector3.back * Time.deltaTime * movementSpeed;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            targetPosition += Vector3.right * Time.deltaTime * movementSpeed;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            targetPosition += Vector3.left * Time.deltaTime * movementSpeed;
        }

        // Clamp camera in game space
        targetPosition = new Vector3(Mathf.Clamp(targetPosition.x, minX, maxX),
            targetPosition.y,
            Mathf.Clamp(targetPosition.z, minZ, maxZ));

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * movementLerpSpeed);

        targetFov = Mathf.Clamp(targetFov - (Input.mouseScrollDelta.y * zoomStrenght), minFov, maxFov);
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, targetFov, Time.deltaTime * zoomLerpSpeed);
    }
}
