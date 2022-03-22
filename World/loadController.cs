using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadController : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject eventSys;
    skillsController player;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Baby").GetComponent<skillsController>();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (mode == LoadSceneMode.Single)
        {
            canvas.SetActive(true);
            eventSys.SetActive(true);
        }
        if (mode == LoadSceneMode.Additive)
        {
            saveData loadedData = saveController.loadPlayerData();

            player.transform.position = new Vector3(loadedData.playerPosition[0], loadedData.playerPosition[1], player.transform.position.z);
            player.diaperCharges = loadedData.diapers;

            SceneManager.UnloadSceneAsync(0);

            canvas.SetActive(true);
            eventSys.SetActive(true);
        }
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
