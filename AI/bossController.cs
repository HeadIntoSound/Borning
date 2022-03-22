using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossController : MonoBehaviour
{

    [Range(0, 2)] public int currentPath;
    public List<GameObject> path0 = new List<GameObject>();
    public List<GameObject> path1 = new List<GameObject>();
    public List<GameObject> path2 = new List<GameObject>();
    AIController AI;
    public bool changed;

    [SerializeField] public List<GameObject>[] paths = new List<GameObject>[3];

    void changePath()
    {
        if (changed)
        {
            if (AI.reverse)
            {
                AI.waypoints = new List<GameObject>(paths[currentPath]);
                AI.waypoints.Reverse();
            }
            else
            {
                AI.waypoints = new List<GameObject>(paths[currentPath]);
            }

            changed = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        AI = GetComponent<AIController>();
        paths[0] = new List<GameObject>(path0);
        paths[1] = new List<GameObject>(path1);
        paths[2] = new List<GameObject>(path2);
    }

    // Update is called once per frame
    void Update()
    {
        changePath();
    }
}
