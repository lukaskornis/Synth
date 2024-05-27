using Unity.Mathematics;
using UnityEngine;

public static class AudioExtensions
{
    public static float AverageVolume(this AudioSource source)
    {
        float sum = 0;
        float[] samples = new float[1024];
        source.clip.GetData(samples, source.timeSamples);

        foreach (var t in samples)
        {
            sum += math.abs(t);
        }
        
        return sum / samples.Length;
    }
}