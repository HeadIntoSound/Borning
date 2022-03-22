using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundTrigger : MonoBehaviour
{
    audioManager aManager;
    [SerializeField] string[] clips;
    string clipName;
    [SerializeField] float cdTime = 2;
    bool canPlay = true;
    [SerializeField] GameObject bubble;
    [SerializeField] bool scrambleSound;
    [SerializeField] float scrambleCD;


    void Start()
    {
        aManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<audioManager>();
        if (transform.Find("Speech Bubble") != null)
        {
            bubble = transform.Find("Speech Bubble").gameObject;
            bubble.SetActive(false);
        }
        if(clips.Length == 1)
        {
            clipName = clips[0];
        }
        if(scrambleSound)
        {
            StartCoroutine(scramble());
        }
    }

    IEnumerator scramble()
    {
        int idx = 0;
        while(true)
        {
            idx = Random.Range(0,clips.Length);
            clipName = clips[idx];
            yield return new WaitForSeconds(scrambleCD);
        }
        
    }

    IEnumerator cd()
    {
        canPlay = false;
        yield return new WaitForSeconds(cdTime);
        canPlay = true;
        yield break;
    }

    IEnumerator decay()
    {
        float iniVol = aManager.clipVolume(clipName);
        float volume = iniVol;
        float t = 0;
        while (volume != 0)
        {
            volume = Mathf.Lerp(iniVol, 0, t);
            aManager.setVolume(clipName, volume);
            t += .7f * Time.deltaTime;
            yield return null;
        }
        aManager.stop(clipName);
        yield return new WaitForSeconds(.5f);
        aManager.setVolume(clipName, iniVol);
        yield break;
    }

    void switchBubble(bool state)
    {
        if (bubble)
        {
            bubble.SetActive(state);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        StopCoroutine(decay());
        if (other.CompareTag("Baby") && canPlay)
        {
            switchBubble(true);
            aManager.play(clipName);
            StartCoroutine(cd());
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Baby"))
        {
            switchBubble(false);
            if (aManager.isLoop(clipName))
            {
                StartCoroutine(decay());
            }
        }
    }
}
