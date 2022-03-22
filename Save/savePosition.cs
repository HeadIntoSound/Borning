using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class savePosition
{
    public int ID;
    public float[] position;
    public savePosition(Transform NPC)
    {
        ID = NPC.GetInstanceID();

        position = new float[2];
        position[0] = NPC.position.x;
        position[1] = NPC.position.y;
    }
}
