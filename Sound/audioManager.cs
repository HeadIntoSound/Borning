using UnityEngine.Audio;
using System;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static audioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if(!transform.parent)
        {
            DontDestroyOnLoad(gameObject);
        }

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.priority = s.priority;
        }
    }

    public void setVolume(string name,float volume)
    {
        Sound s = Array.Find(sounds,sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound " + name + " could not be found");
            return;
        }
        s.source.volume = volume;
    }

    public float clipVolume(string name)
    {
        Sound s = Array.Find(sounds,sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound " + name + " could not be found");
            return 0;
        }
        return s.volume;
    }

    public bool isLoop(string name)
    {
        Sound s = Array.Find(sounds,sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound " + name + " could not be found");
            return false;
        }
        return s.loop;
    }

    public void play(string name)
    {
        Sound s = Array.Find(sounds,sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound " + name + " could not be found");
            return;
        }
        s.source.Play();
    }

    public void stop(string name)
    {
        Sound s = Array.Find(sounds,sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound " + name + " could not be found");
            return;
        }
        s.source.Stop();   
    }

    public bool isPlaying(string name)
    {
        Sound s = Array.Find(sounds,sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound " + name + " could not be found");
            return false;
        }
        if (s.source.isPlaying)
        {
            return true;
        }
        return false;
    }
}
