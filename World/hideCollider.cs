using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class hideCollider : MonoBehaviour
{
    TilemapRenderer trend;
    // Start is called before the first frame update
    void Start()
    {
        trend = GetComponent<TilemapRenderer>();
        trend.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
