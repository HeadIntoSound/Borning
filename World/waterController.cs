using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class waterController : MonoBehaviour
{
    Tilemap water;
    float min;
    float max = -1.99f;
    float t;
    // Start is called before the first frame update
    void Start()
    {
        water = GetComponent<Tilemap>(); 
        min = water.tileAnchor.z;
    }

    // Update is called once per frame
    void Update()
    {
        water.tileAnchor = new Vector3(0,0,Mathf.Lerp(min,max,t));
        t += 0.4f * Time.deltaTime;
        if(t>=1.0f)
        {
            float aux = max;
            max = min;
            min = aux;
            t = 0;
        }
    }
}
