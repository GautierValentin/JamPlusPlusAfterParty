using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [NonSerialized] public List<Transform> _accessibleTiles = new();
    [NonSerialized] public Transform _playerPosition;
    [NonSerialized] public List<Transform> _mostAccuratePath;
}
