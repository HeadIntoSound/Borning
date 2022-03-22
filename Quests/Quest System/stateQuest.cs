using UnityEngine;
[System.Serializable]
public class stateQuest
{
    public bool isState;
    public GameObject obj;
    public bool desiredState;
    public bool currentState;
    public string desc;

    //constructor
    public stateQuest(bool isState, GameObject obj,bool desiredState, bool currentState, string desc)
    {
        this.isState = isState;
        this.obj = obj;
        this.desiredState = desiredState;
        this.currentState = currentState;
        this.desc = desc;
    }
}
