using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInventory : MonoBehaviour
{
    public Transform mainCameraTransform;
    public float distanceFromMainCamera;
    Vector3 inventoryOffset;

    List<TileHandler> tilesInHand;
    

    void Start()
    {
        inventoryOffset = new Vector3(10, 0, 0);
        distanceFromMainCamera = 10;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 resultingPosition = mainCameraTransform.position + mainCameraTransform.forward * distanceFromMainCamera;
        transform.position = resultingPosition;
        transform.eulerAngles = mainCameraTransform.rotation.eulerAngles + new Vector3(-90, 0, 0);
    }

    void AddTile(TileHandler tile)
    {
        tilesInHand.Add(tile);
    }
}
