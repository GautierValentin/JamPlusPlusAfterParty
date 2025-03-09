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
    public int tileReward;
    public bool isEnemyTheBoss;

    public Transform rewardCanvas;

    [SerializeField] TextMeshProUGUI powerText;
    [SerializeField] TextMeshProUGUI powerRewardText;
    [SerializeField] TextMeshProUGUI tileRewardText;


    ///  -----------
    /// UNITY METHODS
    ///  -----------
    public override void Start()
    {
        powerText.text = attackPower.ToString();

        if (!isEnemyTheBoss)
        {
            powerRewardText.text = "+ " + powerReward.ToString();
            tileRewardText.text = "+ " + tileReward.ToString();
            GetComponent<Animator>().Play("Enemy_Idle");
        }
        else
        {
            powerRewardText.text = "";
            tileRewardText.text = "";
        }


        base.Start();

        powerCanvas.localRotation = Quaternion.Euler(90, -dictDirection[lookDirection], 0);
    }

    public void CommitSuicide()
    {
        Destroy(gameObject);
    }
}
