using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class menuController : MonoBehaviour
{
    InputController inputController;
    [SerializeField] VideoPlayer cinematic;
    [SerializeField] GameObject cinematicObject;
    [SerializeField] GameObject blackscreen;
    [SerializeField] GameObject menu;
    [SerializeField] GameObject credits;
    [SerializeField] GameObject settings;
    [SerializeField] GameObject loadingScreen;
    bool runing;

    List<AsyncOperation> loadingScenes = new List<AsyncOperation>();

    IEnumerator loadProgress()
    {
        for(int i = 0; i<loadingScenes.Count;i++)
        {
            while(!loadingScenes[i].isDone)
            {
                yield return null;
            }
        }
        loadingScreen.SetActive(false);
        yield break;
    }

    public void newGame()
    {
        loadingScreen.SetActive(true);
        loadingScenes.Add(SceneManager.LoadSceneAsync(1, LoadSceneMode.Single));
        StartCoroutine(loadProgress());
    }
    public void loadGame()
    {
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }

    public void showCredits()
    {
        credits.SetActive(true);
        menu.SetActive(false);
    }

    public void showSettings()
    {
        settings.SetActive(true);
        menu.SetActive(false);
    }

    public void back()
    {
        menu.SetActive(true);
        credits.SetActive(false);
        settings.SetActive(false);
    }

    IEnumerator playCinamatic()
    {
        yield return new WaitForSeconds(.75f);
        cinematicObject.SetActive(true);
        blackscreen.SetActive(false);
        cinematic.Play();
        runing = true;
        yield return new WaitForSeconds((float)cinematic.length);
        cinematic.Stop();
        menu.SetActive(true);
        cinematicObject.SetActive(false);
        yield break;
    }

    void skipCinematic()
    {
        if (runing)
        {
            StopCoroutine(playCinamatic());
            cinematic.Stop();
            menu.SetActive(true);
            cinematicObject.SetActive(false);
            runing = false;
        }
    }

    void Awake()
    {
        inputController = new InputController();
    }

    void Start()
    {
        StartCoroutine(playCinamatic());
        inputController.Keyboard.Skills.performed += _ => skipCinematic();
        inputController.Mouse.MouseLeftClick.performed += _ => skipCinematic();

    }

    private void OnEnable()
    {
        inputController.Enable();
    }

    private void OnDisable()
    {
        inputController.Disable();
    }
}
