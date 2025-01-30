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
            audioScript.PlaySFXClip(SFXClips.ATTACK);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            audioScript.PlaySFXClip(SFXClips.BUTTONCLICK);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            audioScript.PlaySFXClip(SFXClips.BUTTONHOVER);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            audioScript.PlaySFXClip(SFXClips.DEATH);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            audioScript.PlaySFXClip(SFXClips.LOOT);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            audioScript.PlaySFXClip(SFXClips.TILEACTION);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            audioScript.PlaySFXClip(SFXClips.TILESELECT);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            audioScript.PlaySFXClip(SFXClips.TILESET);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            audioScript.PlaySFXClip(SFXClips.VICTORY);
        }
    }
}
