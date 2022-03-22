using UnityEngine;
[System.Serializable]
public class questItem
{
    //quest name
    public string title;
    //if the objective of the quest is to gather items
    public objectQuest[] obj;

    //if the objective of the quest is to change the active state of something
    public stateQuest state;
    //objective description to show on screen
    [TextArea]public string desc;
    public string[] currentValues;
    public int completedItems;
    public bool completed;
    public bool available = false;
    //if there's something else that doesn't match the criteria above, add something below

    //constructor
    public questItem(string title, objectQuest[] obj, stateQuest state, string desc, string[] currentValues, int completedItems,bool completed,bool available)
    {
        this.title = title;
        this.obj = obj;
        this.state = state;
        this.desc = desc;
        this.currentValues = currentValues;
        this.completedItems = completedItems;
        this.completed = completed;
        this.available = available;
    }
}
