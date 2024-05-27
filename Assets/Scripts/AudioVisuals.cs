using System;
using UnityEngine;

public class AudioVisuals : MonoBehaviour
{
    private LineRenderer line;
    Synth synth;
    public float scale;
    
    private void Awake()
    {
        synth = FindObjectOfType<Synth>();
        
        line = GetComponent<LineRenderer>();
        line.positionCount = synth.currentSamples.Length;
    }

    private void Update()
    {
        var positions = new Vector3[synth.currentSamples.Length];
        for (int i = 0; i < synth.currentSamples.Length; i++)
        {
            positions[i] = new Vector3(i * scale, synth.currentSamples[i], 0);
        }
        line.SetPositions(positions);
    }
}
