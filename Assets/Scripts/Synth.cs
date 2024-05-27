using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class NoteJob : IJob
{
    private NativeArray<float> samples;
    public void Execute()
    {
        var rnd = new Random();
        for (int i = 0; i < samples.Length; i++)
        {
            samples[i] = rnd.NextFloat(-1, 1);
        }
    }
}
    

[RequireComponent(typeof(AudioSource))]
public class Synth : MonoBehaviour
{
    private int sampleRate;
    public List<Note> notes;
    AudioSource source;
    public float spread = 1;
    private Func<float, float>[] forms;
    private Func<float, float> waveform;
    private Random random;
    public float[] currentSamples;
    public float spreadWidth = 0.001f;
    
    private void Awake()
    {
        sampleRate = AudioSettings.outputSampleRate;
        Application.targetFrameRate = 120;
        source = GetComponent<AudioSource>();
        forms = new Func<float, float>[] {SineWave, TriangleWave,SquareWave,SawtoothWave };
        waveform = SineWave;
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            waveform = forms.Choose();
        }
    }
    

    void OnAudioFilterRead(float[] data, int channels)
    {
        //if(currentSamples.Length == 0) currentSamples = new float[data.Length];
        //Debug.Log( data.Length);
        //data.CopyTo( currentSamples, 0 );
        
        
        for (int sample = 0; sample < data.Length; sample += channels)
        { 
            // synthesize active harmonics
            float value = 0;
            var notesPlaying = 0;
            foreach (var note in notes)
            {
                if(!note.isPlaying) continue;
                notesPlaying++;
                //value += waveform( note.phase ) * note.amplitude;
                float noteValue = 0;
                for (int i = 0; i < spread; i++)
                {
                    noteValue += waveform( note.phase * (1 +spreadWidth * i * math.sin( math.PI * i / spread)) ) * note.amplitude;
                }
                noteValue /= spread;
                value += noteValue;
                
                note.phase += note.frequency / sampleRate;
                if (note.phase > 1) note.phase -= 1;
                note.duration += 1f / sampleRate;
                note.amplitude = math.clamp(note.duration * 100, 0, 1);
            }
            
            // normalize sum amplitude
            if(notesPlaying > 0)value /= notesPlaying;
            
            // apply for all channels
            for (int channel = 0; channel < channels; channel++)
            {
                data [sample + channel] += value;
            }
        }
    }

    public void ChangeOctave(int amount)
    {
        foreach (var note in notes)
        {
            note.frequency *= math.pow( 2, amount);
        }
    }

    float SquareWave(float t) => t % 2 >= 1 ? 0 : 1;

    float TriangleWave(float t) => math.abs(t % 2 - 1) * 2 - 1;

    float SineWave(float t) => math.sin(t * math.PI * 2);
    
    float SawtoothWave(float t) => (t % 1) * 2 - 1;

    float NoiseWave(float t) => random.NextFloat(-1, 1);
}