using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    [Range(0, 1)] public float stoppingDistance;
    [Range(0, .5f)] public float facingGap = .15f;
    public List<GameObject> waypoints = new List<GameObject>();
    public int goTo = 0;
    [HideInInspector] public NavMeshAgent agent;
    public float diaperStunTime = 2;
    [HideInInspector] public Vector2 direction;
    [HideInInspector] public bool following = false;
    [HideInInspector] public float initialSpeed;
    [HideInInspector] public int facing;
    [HideInInspector] public float ang;
    [HideInInspector] public bool reverse;
    [SerializeField] bool ignoreFaces;
    [SerializeField] bool shouldReverse = true;
    bool facingControl = true; //if this is true, the face function will control the facing, otherwise it's open for another controller
    bool cleaning;
    bool call;
    Animator anim;
    string idleName = "Idle_1";
    string walkName = "Walk_1";
    string callName = "Walkie_1";

    IEnumerator face()
    {
        while (true)
        {
            if (agent.velocity.magnitude > 0)
            {
                yield return new WaitForSeconds(.25f);
                direction = agent.steeringTarget - transform.position;
                if (facingControl)
                {
                    if (ignoreFaces)
                    {
                        facing = charFacing.facing(direction, 0);
                    }
                    else
                    {
                        facing = charFacing.facing(direction, facingGap);
                    }
                }
            }
            yield return null;
        }
    }

    public IEnumerator callBoss(float speed)
    {
        call = true;
        agent.speed = 0;
        callName = callName.Remove(callName.Length - 1).Insert(callName.Length - 1, facing.ToString());
        anim.Play(callName);
        yield return new WaitForSeconds(1.05f);
        agent.speed = speed;
        call = false;
        yield break;
    }

    IEnumerator walk()
    {
        if (waypoints.Count > 0)
        {
            while (true)
            {
                if (!following && !cleaning)
                {
                    //yield return new WaitForSeconds(1.5f);
                    agent.SetDestination(waypoints[goTo].transform.position);
                    //print(Vector2.Distance(transform.position,waypoints[goTo].transform.position));
                    if (Vector2.Distance(transform.position, waypoints[goTo].transform.position) < stoppingDistance)
                    {
                        waypointInfo wpInfo = waypoints[goTo].GetComponent<waypointInfo>();
                        if (wpInfo.waitTime > 0)
                        {
                            if (wpInfo.changeFacing)
                            {
                                facingControl = false;
                                yield return new WaitForEndOfFrame();
                                facing = wpInfo.facing;
                            }
                            yield return new WaitForSeconds(wpInfo.waitTime);
                            facingControl = true;
                        }
                        goTo++;
                    }
                    if (goTo == waypoints.Count - 1)
                    {
                        if (shouldReverse)
                        {
                            waypoints.Reverse();
                            reverse = !reverse;
                        }
                        goTo = 0;
                    }
                }
                yield return null;
            }
        }
    }

    public IEnumerator detectDiaper(Transform target)
    {
        following = false;
        cleaning = true;
        transform.GetComponent<followController>().destination = target.position;
        StopCoroutine(walk());
        agent.SetDestination(target.position);
        agent.speed = 0;
        yield return new WaitForSeconds(diaperStunTime);
        // play diaper reaction emote here
        agent.speed = initialSpeed;
        yield break;
    }

    public IEnumerator cleanDiaper(GameObject diaper)
    {
        agent.speed = 0;
        // play clean diaper animation here
        yield return new WaitForSeconds(diaperStunTime);
        diaper.SetActive(false);
        cleaning = false;
        agent.speed = initialSpeed;
        goTo++;
        StartCoroutine(walk());
        yield break;
    }

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        initialSpeed = agent.speed;
    }

    // Start is called before the first frame update
    void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        anim = GetComponent<Animator>();
        //print(waypoints.Count);
        StartCoroutine(walk());
        if (anim != null)
        {
            StartCoroutine(face());
        }
    }

    // Update is called once per frame
    void Update()
    {

        ang = Mathf.Acos(agent.steeringTarget.x / agent.steeringTarget.magnitude) * 180 / Mathf.PI - 90;
        if (!ignoreFaces)
        {
            walkName = walkName.Remove(walkName.Length - 1).Insert(walkName.Length - 1, facing.ToString());
            idleName = idleName.Remove(idleName.Length - 1).Insert(idleName.Length - 1, facing.ToString());
        }
        else
        {
            if (facing % 2 != 0)
            {
                walkName = walkName.Remove(walkName.Length - 1).Insert(walkName.Length - 1, facing.ToString());
                idleName = idleName.Remove(idleName.Length - 1).Insert(idleName.Length - 1, facing.ToString());
            }
        }
        if (anim && !call)
        {
            if (agent.velocity.magnitude > 0)
            {
                anim.Play(walkName);
            }
            else
            {
                anim.Play(idleName);
            }
        }
    }
}
