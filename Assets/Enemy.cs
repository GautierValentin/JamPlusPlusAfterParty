using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    // Start is called before the first frame update
    public override void Start()
    {
        GetComponent<Animator>().Play("Enemy_Idle");
    }

    // Update is called once per frame
    public override void Update() 
    {
    }
}
