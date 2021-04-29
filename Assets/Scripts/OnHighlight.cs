using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnHighlight : MonoBehaviour, IPointerEnterHandler
{
    public AudioSource audio;
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        audio.Play();
    }
}
