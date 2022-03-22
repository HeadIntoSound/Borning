using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class talkController : MonoBehaviour
{
    [SerializeField]string salt;
    [SerializeField] Sprite[] symbol;
    [SerializeField] [Range(0, 3)] float time;
    [SerializeField] float cdTime = 2;
    SpriteRenderer bubble;
    SpriteRenderer emoji;
    public bool cd;

    private void Awake()
    {
        emoji = transform.Find("Speech Bubble").transform.Find("Emoji").GetComponent<SpriteRenderer>();
        bubble = transform.Find("Speech Bubble").GetComponent<SpriteRenderer>();
        emoji.color = Color.clear;
        bubble.color = Color.clear;
        cd = false;
    }

    public IEnumerator talk()
    {
        int index = randomGenerator.randomInt(0,symbol.Length-1,salt);
        emoji.sprite = symbol[index];
        bubble.color = Color.white;
        emoji.color = Color.white;
        cd = true;
        yield return new WaitForSeconds(time);
        bubble.color = Color.clear;
        emoji.color = Color.clear;
        yield return new WaitForSeconds(cdTime);
        cd = false;
    }


}
