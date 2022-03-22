using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diaperController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("AI") || other.CompareTag("Boss"))
        {
            StartCoroutine(other.GetComponent<AIController>().detectDiaper(transform.GetComponentInParent<Transform>()));
        }
    }
}
