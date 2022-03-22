using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class saveData
{
    public float[] playerPosition;
    public int diapers;
    public string handObject;

    public saveData (skillsController player)
    {
        diapers = player.diaperCharges;
        handObject = player.transform.GetComponent<grabController>().babyItems.handObject.name;

        playerPosition = new float[2];
        playerPosition[0] = player.transform.position.x;
        playerPosition[1] = player.transform.position.y;
    }
    
}
