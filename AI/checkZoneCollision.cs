using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkZoneCollision : MonoBehaviour
{
    sensorController sensor;
    [SerializeField] int zoneNumber;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Baby"))
        {
            sensor.zones[zoneNumber] = true;
            sensor.collided = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Baby"))
        {
            sensor.zones[zoneNumber] = false;
            sensor.collided = false;
        }
    }

    void Start()
    {
        sensor = GetComponentInParent<sensorController>();
    }
}
