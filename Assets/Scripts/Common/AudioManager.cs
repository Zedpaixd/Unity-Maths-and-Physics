using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : GenericSingleton<AudioManager>
{
    AudioSource sfxSource;
    //AudioSource bgMusicSource;
    
    public override void Awake()
    {
        base.Awake();
        sfxSource = gameObject.AddComponent<AudioSource>();
        //bgMusicSource = gameObject.AddComponent<AudioSource>();

        //bgMusicSource.clip = ...
        //bgMusicSource.loop = true;
    }

    public void PlayEffect(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
