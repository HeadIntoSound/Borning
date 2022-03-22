using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadowController : MonoBehaviour
{
    playerController baby;
    spriteController babySpirte;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        baby = GameObject.FindGameObjectWithTag("Baby").GetComponent<playerController>();
        babySpirte = GameObject.FindGameObjectWithTag("Baby").GetComponent<spriteController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            if (baby.isMoving)
            {
                anim.Play(babySpirte.walkName);
            }
            else
            {
                anim.Play(babySpirte.idleName);
            }
        }
    }
}
