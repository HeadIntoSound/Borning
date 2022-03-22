using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
[System.Serializable]
public class questCreator : MonoBehaviour
{
    [SerializeField] string title;
    [SerializeField] bool isObj;
    [SerializeField] objectQuest[] objects;
    // [SerializeField] GameObject[] obj;
    // [SerializeField] int maxCount;
    // [SerializeField] int currentCount;
    [SerializeField] bool isState;
    [SerializeField] GameObject obj1;
    [SerializeField] bool desiredState;
    [SerializeField] bool currentState;
    [SerializeField] [TextArea] string stateDescription;
    [SerializeField] [TextArea] string description;
    [SerializeField] string[] currentValues;
    [SerializeField] int completedItems;
    [SerializeField] bool available;
    [SerializeField] bool completed;
    // [System.Serializable]public struct questWrapperClass.questsWrapper{
    //     public questItem[] quests;
    // }
    string path;

    questItem createItem()
    {
        currentValues = new string[objects.Length];
        for (int i = 0; i < objects.Length; i++)
        {
            currentValues[i] = objects[i].currentCount.ToString() + "/" + objects[i].maxCount.ToString();
        }
        if (isObj && !isState)
        {
            return new questItem(title, objects, null, description, currentValues, completedItems, completed, available);
        }
        if (isState && !isObj)
        {
            stateQuest stItem = new stateQuest(isState, obj1, desiredState, currentState, stateDescription);
            return new questItem(title, null, stItem, description, currentValues, completedItems, completed, available);
        }
        if (isObj && isState)
        {
            // objectQuest objItem = new objectQuest(isObj,obj,maxCount,currentCount);
            stateQuest stItem = new stateQuest(isState, obj1, desiredState, currentState, stateDescription);
            return new questItem(title, objects, stItem, description, currentValues, completedItems, completed, available);
        }
        else
        {
            return null;
        }
    }

    public questWrapperClass.questsWrapper loadExistingQuests()
    {
        var jsonTextFile = Resources.Load<TextAsset>("QuestData/quests");
        return JsonUtility.FromJson<questWrapperClass.questsWrapper>(jsonTextFile.ToString());
    }

    public void writeFile()
    {
        path = Application.dataPath + "/Resources/QuestData/quests.json";
        questWrapperClass.questsWrapper toJson = new questWrapperClass.questsWrapper();
        if (File.Exists(path))
        {
            questWrapperClass.questsWrapper existingQuests = loadExistingQuests();
            toJson.quests = new questItem[existingQuests.quests.Length + 1];
            for (int i = 0; i < existingQuests.quests.Length; i++)
            {
                toJson.quests[i] = existingQuests.quests[i];
            }
            toJson.quests[toJson.quests.Length - 1] = createItem();
        }
        else
        {
            toJson.quests = new questItem[1];
            toJson.quests[0] = createItem();
        }

        string json = JsonUtility.ToJson(toJson, true);

        File.WriteAllText(path, json);
        print(path);
    }

}
