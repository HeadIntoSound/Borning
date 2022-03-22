using UnityEngine;
[System.Serializable]
public class objectQuest
{
    public bool isObject;
    public GameObject item;
    public int maxCount;
    public int currentCount;
    public string name;

    //constructor
    public objectQuest(bool isObject, GameObject item, int maxCount, int currentCount)
    {
        this.isObject = isObject;
        this.item = item;
        this.maxCount = maxCount;
        this.currentCount = currentCount;
        this.name = item.name;
    }
}
