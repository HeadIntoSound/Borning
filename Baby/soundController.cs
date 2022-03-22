using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundController : MonoBehaviour
{
    //playerController baby;
    audioManager aManager;
    public bool canPlay;
    // Start is called before the first frame update
    void Start()
    {
        //baby = GetComponent<playerController>();
        aManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<audioManager>();
    }

    public void playClip(string name)
    {
        if(!aManager.isPlaying(name))
        {
            aManager.play(name);
        }
    }
}
