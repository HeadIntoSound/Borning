using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chosenZebra : MonoBehaviour
{
    public bool chosen;
    public int idxNumber;
    Animator anim;

    void Start() {
        anim = GetComponent<Animator>();
    }

    void Update() {
        if(chosen)
        {
            anim.Play("Z2_Idle_1");
        }
        else
        {
            anim.Play("Z1_Idle_1");
        }
    }
}
