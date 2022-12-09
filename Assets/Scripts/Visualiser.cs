using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Visualiser : MonoBehaviour
{
    public static int circleSize = 64;
    AudioSource _audioSource;
    public static float[] _samples = new float[circleSize];
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();   
    }
    void Update()
    {
        GetSpectrumAudioSource();
    }

    void GetSpectrumAudioSource()
    {
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
    }
}
