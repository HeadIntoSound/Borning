using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class captureController : MonoBehaviour
{

    public playerController baby;
    public AIController AI;
    float startTime;
    float originalSpeed;
    [SerializeField] worldFunctions gameController;
    [SerializeField] Transform respawnPoint;


    IEnumerator bust()
    {
        if (!baby.inSafeZone)
        {
            AI.GetComponent<followController>().follow();
            AI.following = false;
            float aux = AI.agent.speed;
            AI.agent.speed = 0.1f;
            startTime = Time.time;
            StartCoroutine(gameController.transition(startTime, 5));
            yield return new WaitForSeconds(0.75f);
            StartCoroutine(gameController.cameraSpeed());
            baby.busted(respawnPoint.position);
            yield return new WaitForSeconds(2);
            AI.agent.speed = aux;
            baby.speed = originalSpeed;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<worldFunctions>();
        AI = GetComponentInParent<AIController>();
        baby = GameObject.FindGameObjectWithTag("Baby").GetComponent<playerController>();
        originalSpeed = baby.speed;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Baby")
        {
            StartCoroutine(baby.freeze(4));
            StartCoroutine(bust());
        }
    }


}
