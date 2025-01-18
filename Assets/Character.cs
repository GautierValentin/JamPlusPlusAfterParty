using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int attackPower;

    // Update is called once per frame
    virtual public void Update()
    {
        
    }

    public void AddAttackPower(int amount)
    {
        attackPower += amount;
    }

}
