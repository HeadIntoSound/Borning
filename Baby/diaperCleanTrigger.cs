using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diaperCleanTrigger : MonoBehaviour
{

    public IEnumerator initialMove(Vector3 target)
    {
        yield return new WaitForSeconds(.3f);
        while(transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position,target,Time.deltaTime * 1.5f);
            yield return null;
        }
        yield break;
    }

    public IEnumerator instanciateCollider()
    {
        yield return new WaitForSeconds(.5f);
        GetComponent<Collider2D>().enabled = true;
        yield break;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.transform.CompareTag("AI") || other.transform.CompareTag("Boss"))
        {
            StartCoroutine(other.transform.GetComponent<AIController>().cleanDiaper(gameObject));
        }
    }
}
