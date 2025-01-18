using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{

    public MoveToIsle movements;

    public void Pathfind()
    {
    }

    private void SendPath(List<Vector3> Path)
    {
        for (int i = 0; i < Path.Count; i++)
        {
            movements.AddTilePosition(Path[i]);
        }
    }
}
