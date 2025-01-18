using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int attackPower;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddAttackPower(int amount) {
        attackPower += amount;
    }
}
