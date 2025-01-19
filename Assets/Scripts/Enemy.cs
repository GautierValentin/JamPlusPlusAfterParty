using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    ///  ----
    /// FIELDS
    ///  ----
    public int amount;
    public bool isEnemyTheBoss;


    public override void Start()
    {
        GetComponent<Animator>().Play("Enemy_Idle");
    }
}
