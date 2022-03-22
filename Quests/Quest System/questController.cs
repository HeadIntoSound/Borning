using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class questController : MonoBehaviour
{

    public questItem[] quest;
    public bool updated;

    int indexSearch(string questTitle)
    {
        for(int i = 0; i>quest.Length;i++)
        {
            if(quest[i].title == questTitle)
            {
                return i;
            }
        }
        return -1;
    }

    void updateQuests(string questTitle)
    {
        //update for turtle rescue
        if(questTitle == "Help the Turtle!" && indexSearch("Rescue the Turtle!")!=-1)
        {
            quest[indexSearch(questTitle)].available = true;
        }
        
    }

    public void progress(string item)
    {
        int aux = 0;
        foreach (questItem mission in quest)
        {
            foreach(objectQuest objct in mission.obj)
            { 
                if(item == objct.name)
                {
                    objct.currentCount++;
                    updated = true;
                }
            }
            for(int i = 0;i<mission.obj.Length;i++)
            {
                if(mission.obj[i].currentCount >= mission.obj[i].maxCount)
                {
                    aux++;
                }
            }
            if(aux >= mission.obj.Length)
            {
                mission.completed = true;
                updateQuests(mission.title);
            }   
        }
    }



    

    void Start()
    {
        quest = GameObject.FindGameObjectWithTag("QuestCreator").GetComponent<questCreator>().loadExistingQuests().quests;
    }
}
