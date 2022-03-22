using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class buttonController : MonoBehaviour
{
    audioManager aManager;
    skillsController player;
    public GameObject pauseMenu;
    playerController baby;
    public GameObject inventory;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Baby").GetComponent<skillsController>();
        baby = player.GetComponent<playerController>();
        aManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<audioManager>();
    }

    public void resume()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        player.pause = false;
        player.pauseGame();
    }

    public void save()
    {
        saveController.savePlayerData(player);
    }

    public void load()
    {
        saveData loadedData = saveController.loadPlayerData();

        player.transform.position = new Vector3(loadedData.playerPosition[0], loadedData.playerPosition[1], player.transform.position.z);
        player.diaperCharges = loadedData.diapers;
        player.transform.GetComponent<grabController>().babyItems.handObject = (GameObject)Resources.Load("Prefabs/" + loadedData.handObject, typeof(GameObject));
    }

    public void exit()
    {
        Application.Quit();
    }

    public void changeInput()
    {
        if(baby.movementType == 0)
        {
            baby.movementType = 1;
            baby.agent.enabled = false;
        }
        else if(baby.movementType == 1)
        {
            baby.movementType = 0;
            baby.agent.enabled = true;
        }
        resume();
    }

    public void openInventory()
    {
        if(!pauseMenu.activeSelf)
        {
            inventory.SetActive(true);
            aManager.play("Unzip");
        }
    }
}
