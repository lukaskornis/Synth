using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CoolButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int note;
    Synth synth;
    
    void Start()
    {
        synth = FindObjectOfType<Synth>();
    }
    

    public void OnPointerEnter(PointerEventData eventData)
    {
        synth.notes[note].isPlaying = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        synth.notes[note].isPlaying = false;
    }
}
