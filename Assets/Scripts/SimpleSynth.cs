using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SimpleSynth : MonoBehaviour
{
    public static UnityEvent<NativeArray<float>> OnSamplesAvailable = new();
    NativeArray<float> samples;
    public int l;


    void OnAudioFilterRead(float[] data, int channels)
    {
        if (samples.Length != data.Length)
        {
            if(samples.IsCreated ) samples.Dispose();
            samples = new NativeArray<float>(data.Length, Allocator.Persistent);
        }
        
        OnSamplesAvailable.Invoke( samples );

        for (int i = 0; i < data.Length; i++)
        {
            data[i] = samples[i];
        }
    }

    private void OnDestroy()
    {
        if(samples.IsCreated ) samples.Dispose();
    }
}
