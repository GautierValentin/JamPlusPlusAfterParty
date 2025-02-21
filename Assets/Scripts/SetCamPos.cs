using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCamPos : MonoBehaviour
{
    void Start()
    {
        Camera.main.transform.position = transform.position;
        Camera.main.GetComponent<CameraController>().targetPosition = transform.position;
    }
}
