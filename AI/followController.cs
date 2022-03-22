using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followController : MonoBehaviour
{
    [Range(0, 1)] public float speedModifier = .75f;
    public int zone;
    public playerController baby;
    public AIController AI;
    [HideInInspector] public bool canReset;
    public float distance;
    public float scanTime;
    public Vector3 destination;
    bossController boss;


    public void follow()
    {
        StartCoroutine(AI.callBoss(AI.initialSpeed + speedModifier));
        //AI.agent.speed = AI.initialSpeed + speedModifier;
        boss.changed = true;
        boss.currentPath = zone;
        AI.following = true;

    }

    public void release()
    {
        AI.following = false;
        AI.agent.SetDestination(transform.position);
        AI.agent.speed = AI.initialSpeed;
    }

    void point()
    {
        if (Vector3.Distance(AI.agent.steeringTarget, transform.position) > .5f)
        {
            float ang = Mathf.Atan2(AI.agent.steeringTarget.y - transform.position.y, AI.agent.steeringTarget.x - transform.position.x);
            destination.x = transform.position.x + distance * Mathf.Cos(ang);
            destination.y = transform.position.y + distance * Mathf.Sin(ang);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        baby = GameObject.FindGameObjectWithTag("Baby").GetComponent<playerController>();
        AI = GetComponent<AIController>();
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponentInParent<bossController>();
    }

    void Update()
    {
        transform.Find("Alert").gameObject.SetActive(AI.following);
        if (!AI.agent.isStopped)
        {
            point();
        }
        if (AI.following)
        {
            AI.agent.SetDestination(baby.transform.position);
            destination = baby.transform.position;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        if (Application.isPlaying)
        {
            Gizmos.DrawLine(transform.position, AI.agent.destination);
            Gizmos.DrawSphere(AI.agent.destination, .05f);
            Gizmos.DrawSphere(transform.position, .05f);
            Gizmos.DrawSphere(destination, .2f);
        }
    }
}