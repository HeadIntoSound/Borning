using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeControllType : MonoBehaviour
{
    public playerController baby;
    void Start() 
    {
        baby = GameObject.FindGameObjectWithTag("Baby").GetComponent<playerController>();    
    }
    public void changeControl()
    {
        if(baby.movementType == 0)
        {
            baby.movementType = 1;
        }
        else
        {
            baby.movementType = 0;
            baby.move = baby.transform.position;
        }
    }
}
