using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UsingPower
{
    NONE = 0,
    ROTATETILE,
    REPLACETILE,
    MOVETILE
}

public class PowerManager : MonoBehaviour
{
    //int mana = 10;
    //int manaUsedOnCast = 0;
    public UsingPower currentPower = 0;

    [SerializeField] TileInventory inventory;

    Ray ray; 
    RaycastHit hitTile;

    bool selectedPowerTile = false;

    GameObject selectedTile;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!inventory.isTileSelected)
        {
            if (Input.GetMouseButtonDown(1))
            {
                currentPower = UsingPower.NONE;
                inventory.canSelect = true;

                if (selectedTile != null)
                {
                    selectedTile.transform.localScale = Vector3.one * 0.5f;
                    selectedTile = null;
                }
                selectedPowerTile = false;
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                currentPower = UsingPower.ROTATETILE;
                inventory.canSelect = false;
            }

            if (Input.GetKeyDown(KeyCode.Space) && selectedPowerTile == true)
            {
                // Confirm power
                currentPower = UsingPower.NONE;
                
                inventory.canSelect = true;

                selectedPowerTile = false;

                if (selectedTile != null)
                {
                    GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();

                    gm._accessibleTiles.Clear();
                    gm._accessibleTiles.Add(gm._playerPosition);

                    selectedTile.GetComponent<Tile>().OnSet();
                    selectedTile.transform.localScale = Vector3.one * 0.5f;
                    selectedTile = null;
                }

                GameObject.Find("Player").GetComponent<MoveToIsle>().GetPath();
            }

            if (Input.GetKeyDown(KeyCode.E) && currentPower == UsingPower.ROTATETILE)
            {
                selectedTile.GetComponent<TileHandler>().RotateClockwise();
            }

            if (Input.GetKeyDown(KeyCode.Q) && currentPower == UsingPower.ROTATETILE)
            {
                selectedTile.GetComponent<TileHandler>().RotateCounterClockwise();
            }

            // Use current activated power
            if (Input.GetMouseButtonDown(0))
            {
                switch (currentPower)
                {
                    case UsingPower.NONE:
                        // Nothing happens
                        selectedTile = null;
                        break;

                    case UsingPower.ROTATETILE:
                        selectedPowerTile = true;
                        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                        if (Physics.Raycast(ray, out hitTile, 100, 1 << 6))
                        {
                            selectedTile = hitTile.collider.gameObject;
                            selectedTile.transform.localScale = Vector3.one * 0.4f;
                        }

                        break;

                        //case UsingPower.REPLACETILE:

                        //    ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                        //    if (Physics.Raycast(ray, out hitTile, 100, 1 << 6))
                        //    {
                        //        selectedTile = hitTile.collider.gameObject;
                        //    }
                        //    break;

                        //case UsingPower.MOVETILE:

                        //    ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                        //    if (Physics.Raycast(ray, out hitTile, 100, 1 << 6))
                        //    {
                        //        selectedTile = hitTile.collider.gameObject;

                        //    }
                        //    break;
                }
            }
        }

        //if (Input.GetKeyDown(KeyCode.G) && mana >= 5)
        //{
        //    currentPower = UsingPower.REPLACETILE;
        //    manaUsedOnCast = 5;
        //    inventory.canSelect = false;

        //}

        //if (Input.GetKeyDown(KeyCode.H) && mana >= 8)
        //{
        //    currentPower = UsingPower.MOVETILE;
        //    manaUsedOnCast = 8;
        //    inventory.canSelect = false;

        //}

        

    }
}
