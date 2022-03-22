using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class safeZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Baby"))
        {
            other.GetComponent<playerController>().inSafeZone = true;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Baby"))
        {
            other.GetComponent<playerController>().inSafeZone = true;
        }
    }    

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Baby"))
        {
            other.GetComponent<playerController>().inSafeZone = false;
        }
    }


}
