using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public void OpenChest()
    {
        GetComponent<Animator>().CrossFade("Chest_Open", 0.2f);

        GameObject.Find("Player").GetComponent<Player>().PickChest();
    }

    public void StayOpen()
    {

    }

}
