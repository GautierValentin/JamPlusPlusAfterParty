using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : Character
{
    ///  ----
    /// FIELDS
    ///  ----
    public int powerReward;
    public bool isEnemyTheBoss;
    [SerializeField] TextMeshProUGUI powerText;
    [SerializeField] TextMeshProUGUI powerRewardText;
    [SerializeField] TextMeshProUGUI tileRewardText;


    ///  -----------
    /// UNITY METHODS
    ///  -----------
    public override void Start()
    {
        powerText.text = attackPower.ToString();
        powerRewardText.text = powerReward.ToString();
        tileRewardText.text = "?";

        GetComponent<Animator>().Play("Enemy_Idle");
    }
}
