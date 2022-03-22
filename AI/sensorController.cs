using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sensorController : MonoBehaviour
{

    playerController baby;
    [SerializeField] checkZoneCollision zone;
    AIController AI;
    [Range(0, 15)] public float distance;
    public bool[] zones;
    public bool collided;
    public float scanTime = 1;

    IEnumerator cast()
    {
        while (true)
        {
            Vector3 dir = baby.transform.position - transform.position;
            if (collided && compareFacing())
            {
                RaycastHit2D obj = Physics2D.Raycast(transform.position, dir, distance, Physics2D.DefaultRaycastLayers, 0);
                if (obj && obj.transform.CompareTag("Baby"))
                {
                    AI.GetComponent<followController>().follow();
                    yield return new WaitForSeconds(4);
                }
                yield return new WaitForSeconds(scanTime);
            }
            if (!collided && AI.following)
            {
                yield return new WaitForSeconds(1.5f);
                if (!collided)
                {
                    AI.GetComponent<followController>().release();
                }
            }
            yield return null;
        }
    }

    bool compareFacing()
    {
        if (AI.facing == 0)
        {
            if (zones[0] || zones[7])
            {
                return true;
            }
        }
        if (AI.facing > 0)
        {
            if (zones[AI.facing] || zones[AI.facing - 1])
            {
                return true;
            }
        }
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        baby = GameObject.FindGameObjectWithTag("Baby").GetComponent<playerController>();
        AI = GetComponent<AIController>();
        zone = GetComponentInChildren<checkZoneCollision>();
        zones = new bool[8];
        StartCoroutine(cast());
    }

    // Update is called once per frame
    void Update()
    {

    }
}
