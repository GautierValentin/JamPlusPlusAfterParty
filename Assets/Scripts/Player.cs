using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Player : Character
{
    [SerializeField] Tile startingTile;
    [SerializeField] TextMeshProUGUI txtComp;

    [SerializeField] List<UnityEvent> chestEvents;
    int chestLvl = 0;

    [NonSerialized] public bool _isDead = false;

    public override void Start()
    {
        //GetComponent<Animator>().Play("Player_Idle");
        GameObject.Find("GameManager").GetComponent<GameManager>()._playerPosition = startingTile.transform;
        GameObject.Find("GameManager").GetComponent<GameManager>()._accessibleTiles.Add(startingTile.transform);

        base.Start();
    }

    void FixedUpdate()
    {
        txtComp.text = attackPower.ToString();
    }

    public void PickChest()
    {
        if(chestLvl < chestEvents.Count)
            chestEvents[chestLvl].Invoke();
        chestLvl++;
    }

    public void CheckForGameOver()
    {
        if (_isDead)
        {
            GameObject.Find("Main Camera").GetComponent<AudioScript>().PlayClip(SFXClips.LOOT);
        }
    }

    public void TriggerGameOver()
    {
        GameObject.Find("Level Loader").GetComponent<LevelLoader>().LoadSpecificScene("MainMenu");
    }
}
