using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();

            sound.source.clip = sound.clip;
            sound.source.loop = sound.loop;
        }

        Play("Theme");
    }
    
    public void Play(string name)
    {
        Sound targetedSound = Array.Find(sounds, sound => sound.name == name);

        if (targetedSound == null)
        {
            return;
        }

        targetedSound.source.Play();
    }
}
