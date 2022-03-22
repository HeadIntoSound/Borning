using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missionController : MonoBehaviour
{
    public Camera cam;
    public playerController baby;
    public float time;
    public float camMaxSize = 20;
    [SerializeField] Animator giraffe;

    void Start()
    {
        baby = GameObject.FindGameObjectWithTag("Baby").GetComponent<playerController>();
        cam = Camera.main;
    }

    IEnumerator moveBlock()
    {
        float aux = baby.speed;
        baby.speed = 0;
        yield return new WaitForSeconds(time + 3);
        baby.speed = aux;
        yield break;
    }

    IEnumerator cameraSize()
    {
        float ogSize = cam.orthographicSize;
        float t = 0;

        while (cam.orthographicSize < camMaxSize)
        {
            cam.orthographicSize = Mathf.Lerp(ogSize, camMaxSize, t);
            t += .5f * Time.deltaTime;
            if (t >= 1)
            {
                cam.orthographicSize = camMaxSize;
                t=0;
            }
            yield return null;
        }

        yield return new WaitForSeconds(time);

        while (cam.orthographicSize > ogSize)
        {
            cam.orthographicSize = Mathf.Lerp(camMaxSize, ogSize, t);
            t += .5f * Time.deltaTime;
            if (t >= 1)
            {
                cam.orthographicSize = ogSize;
                t=0;
            }
            yield return null;
        }

        yield break;
    }

    IEnumerator neckMove(){
        giraffe.Play("Neck_1");
        yield return new WaitForSeconds(1);
        giraffe.Play("Idle_1");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Baby"))
        {
            // run animation of the baby climbing to the giraffe's head
            StartCoroutine(moveBlock());
            StartCoroutine(cameraSize());
            StartCoroutine(neckMove());
        }
    }
}
