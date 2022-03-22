using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.AI;

public class animalController : MonoBehaviour
{
    [Range(0, 1)] public float stoppingDistance;
    [Range(0, .5f)] public float facingGap = 0;
    public List<GameObject> waypoints = new List<GameObject>();
    public int goTo = 0;
    public NavMeshAgent agent;
    [HideInInspector] public Vector2 direction;
    [HideInInspector] public float initialSpeed;
    [HideInInspector] public int facing;
    [HideInInspector] public float ang;
    [HideInInspector] public bool reverse;
    // [SerializeField] bool ignoreFaces = true;
    Animator anim;
    string idleName = "Idle_1";
    string walkName = "Walk_1";

    IEnumerator face()
    {
        while (true)
        {
            if (agent.velocity.magnitude > 0)
            {
                yield return new WaitForSeconds(.25f);
                direction = agent.steeringTarget - transform.position;
                facing = charFacing.facing(direction, facingGap);
            }
            yield return null;
        }
    }

    IEnumerator walk()
    {
        if (waypoints.Count > 0)
        {
            while (true)
            {
                //yield return new WaitForSeconds(1.5f);
                agent.SetDestination(waypoints[goTo].transform.position);
                //print(Vector2.Distance(transform.position,waypoints[goTo].transform.position));
                if (Vector2.Distance(transform.position, waypoints[goTo].transform.position) < stoppingDistance)
                {
                    if (waypoints[goTo].GetComponent<waypointInfo>().waitTime > 0)
                    {
                        yield return new WaitForSeconds(waypoints[goTo].GetComponent<waypointInfo>().waitTime);
                    }
                    goTo = Random.Range(0, waypoints.Count);
                }

                yield return null;
            }
        }
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
        if (facing % 2 != 0)
        {
            walkName = walkName.Remove(walkName.Length - 1).Insert(walkName.Length - 1, facing.ToString());
            idleName = idleName.Remove(idleName.Length - 1).Insert(idleName.Length - 1, facing.ToString());
        }
        if (anim)
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
