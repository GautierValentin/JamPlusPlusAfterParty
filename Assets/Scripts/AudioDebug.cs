using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDebug : MonoBehaviour
{
    [SerializeField] Camera cam;
    AudioScript audioScript;
    void Start()
    {
        audioScript = cam.GetComponent<AudioScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            audioScript.PlayClip(SFXClips.ATTACK);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            audioScript.PlayClip(SFXClips.BUTTONCLICK);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            audioScript.PlayClip(SFXClips.BUTTONHOVER);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            audioScript.PlayClip(SFXClips.DEATH);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            audioScript.PlayClip(SFXClips.LOOT);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            audioScript.PlayClip(SFXClips.TILEACTION);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            audioScript.PlayClip(SFXClips.TILESELECT);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            audioScript.PlayClip(SFXClips.TILESET);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            audioScript.PlayClip(SFXClips.VICTORY);
        }
    }
}
