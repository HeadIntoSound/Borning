using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onAwake : MonoBehaviour
{
    public GameObject canvas;
    public GameObject eventSys;
    // Start is called before the first frame update
    void Start()
    {
        canvas.SetActive(true);
        eventSys.SetActive(true);
    }
}
