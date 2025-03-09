using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverableButton : MonoBehaviour, IPointerEnterHandler
{
    private AudioScript audioScript;

    void Start()
    {
        audioScript = GameObject.Find("Main Camera").GetComponent<AudioScript>();
    }

    void IPointerEnterHandler.OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData) 
    {
        audioScript.PlayClip(SFXClips.BUTTONHOVER);
    }
}
