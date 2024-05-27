using UnityEngine;

public class DrumMachine : MonoBehaviour
{
    AudioSource audioSource;
    public float bpm = 120;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating(nameof(Play), 0, 60f / bpm);
    }

    private void Play()
    {
        audioSource.PlayOneShot(audioSource.clip);
        print ("play");
    }
}
