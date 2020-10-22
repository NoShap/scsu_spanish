using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Sound" + s + "Does Not Exist!");
            return;
        }
        s.source.Play();
    }

    public AudioClip GetClip(string name)
    {
        string n = name.ToLower();
        print(n);
        Sound s = Array.Find(sounds, sound => sound.name.ToLower() == n);
        return s.source.clip;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
