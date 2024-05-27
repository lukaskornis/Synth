using System;
using Unity.Mathematics;

[Serializable]
public class Note
{
    public float frequency = 440;
    public float amplitude = 1;
    public float phase;
    public bool isPlaying = true;
    public float duration;
    
    // note to frequency
    float HzToMidi(float hz) => math.log(hz / 440) / math.log(2) * 12 + 69;
    float MidiToHz(float midi) => math.pow(2, (midi - 69) / 12) * 440;

    float NameToFrequency(string name)
    {
        var names = new[]{"C","C#","D","D#","E","F","F#","G","G#","A","A#","B"};
        // frequencies of first octave
        var frequencies = new[]{27.5f,29.135f,30.868f,32.703f,34.648f,36.708f,38.891f,41.203f,43.654f,46.249f,48.999f,51.913f};
        
        var letter  = name[0];
        var octave  = int.Parse(name.Substring(1));
        
        var index = Array.IndexOf(names, letter);
        return frequencies[index] * math.pow(2, octave);
    }
    
    float VolumeToDB(float volume) => math.log10(volume) * 20;
    float DBToVolume(float db) => math.pow(10, db / 20);
}