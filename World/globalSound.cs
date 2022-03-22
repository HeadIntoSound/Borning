using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globalSound : MonoBehaviour
{
    audioManager aManager;
    [SerializeField] int ran;
    bool canPlay;
    // Start is called before the first frame update
    void Start()
    {
        aManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<audioManager>();
        aManager.play("AmbienceSound");
        StartCoroutine(randomSound());
    }

    IEnumerator randomSound()
    {
        yield return new WaitForSeconds(380);
        if (canPlay)
        {
            while (true)
            {
                ran = randomGenerator.randomInt(0, 1000, "ElEfAnTe");
                if (ran == 33)
                {
                    aManager.play("Elephant");
                    yield return new WaitForSeconds(120);

                }
                yield return null;
            }
        }
    }

    void Update()
    {

    }
}
