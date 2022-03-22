using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishing : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Baby"))
        {
            // other.GetComponent<spriteController>().fishing = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Baby"))
        {
            // other.GetComponent<spriteController>().fishing = false;
            // other.GetComponent<spriteController>().fishingIdle = false;
        }
    }
}
