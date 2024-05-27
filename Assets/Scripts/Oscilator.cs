using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class Oscilator : MonoBehaviour
{
    public float attack = 0.1f;
    public float release = 0.1f;
    public float phase = 0;
    public float value;
    public bool isOn;
    public float time;
    private int sampleRate;
    public float frequency = 440;
    private float phaseStep;
    
    private void Awake()
    {
        sampleRate = AudioSettings.outputSampleRate;
    }

    private void Update()
    {
        if (isOn)
        {
            value += Time.deltaTime / attack;
            if (value >= 1) value = 1;
        }
        else
        {
            value -= Time.deltaTime / release;
            if (value <= 0) value = 0;
        }
        
        phaseStep = frequency / sampleRate;
    }


    private Random rnd;
    
    private void Start()
    {
        SimpleSynth.OnSamplesAvailable.AddListener( OnSamplesAvailable );
        rnd = new Random(1);
    }
    
    private void OnSamplesAvailable(NativeArray<float> samples)
    {
        if( value == 0 ) return;
        
        for (int i = 0; i < samples.Length; i++)
        {
            //var average
            //samples[i] = rnd.NextFloat(-1, 1) * value;
            samples [i] = math.sin( phase * math.PI * 2 ) * value;
            
            phase += phaseStep;
            if (phase > 1) phase -= 1;
            
        }
    }
}
