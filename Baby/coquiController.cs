using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coquiController : MonoBehaviour
{

    [SerializeField] playerController baby;
    public Transform respawnPouint;
    public bool mounted;
    float initialSpeed;

    public void mount()
    {
        mounted = true;
        baby.GetComponent<SpriteRenderer>().enabled = false;
        baby.mountedObject.GetComponent<SpriteRenderer>().enabled = true;
        baby.mountedObject.GetComponent<spriteController>().enabled = true;
        baby.GetComponent<CircleCollider2D>().radius = .35f;
        gameObject.SetActive(!mounted);
        initialSpeed = baby.speed;
        baby.speed += 1.5f;
        //trigger animation
    }

    public void dismount()
    {
        mounted = false;
        transform.position = baby.transform.position;
        baby.mountedObject.GetComponent<SpriteRenderer>().enabled = false;
        baby.mountedObject.GetComponent<spriteController>().enabled = false;
        baby.GetComponent<SpriteRenderer>().enabled = true;
        baby.GetComponent<CircleCollider2D>().radius = .15f;
        baby.speed = initialSpeed;
        //trigger dismount animation
        gameObject.SetActive(!mounted);
    }

    void Awake()
    {
        baby = GameObject.FindGameObjectWithTag("Baby").GetComponent<playerController>();
    }

    void Start()
    {

    }
}
