using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{

    [SerializeField] GameObject[] warpPoint;
    [SerializeField] GameObject[] warpButton;
    [SerializeField] playerController baby;
    [SerializeField] GameObject menu;
    [SerializeField] GameObject diaperIndicator;
    [SerializeField] GameObject heldItem;
    [SerializeField] GameObject fishingRod;
    [SerializeField] GameObject[] qElement;
    [SerializeField] GameObject[] qItem0;
    [SerializeField] GameObject[] qItem1;
    [SerializeField] worldFunctions gameController;
    [SerializeField] Sprite[] diapers;
    questController questController;
    int diaperCharges;

    public void warp(string point)
    {
        foreach (GameObject warp in warpPoint)
        {
            if (warp.name == point)
            {
                StartCoroutine(gameController.transition(Time.time, 1.5f));
                StartCoroutine(gameController.cameraSpeed());
                baby.transform.position = warp.transform.position;
                Time.timeScale = 1;
                menu.SetActive(false);
            }
        }
    }

    void diaperCount()
    {
        diaperIndicator.GetComponent<Image>().sprite = diapers[diaperCharges];
    }

    void showItem()
    {
        if (baby.gameObject.GetComponent<grabController>().holding)
        {
            heldItem.GetComponent<Image>().sprite = baby.gameObject.GetComponent<grabController>().babyItems.handObject.GetComponent<SpriteRenderer>().sprite;
            heldItem.SetActive(true);
        }
        else
        {
            heldItem.SetActive(false);
        }
    }

    void showQuests()
    {
        for (int i = 0; i < questController.quest.Length; i++)
        {
            if (questController.quest[i].available)
            {
                qElement[i].GetComponentInChildren<Text>().text = questController.quest[i].title;
                if (questController.quest[i].obj.Length > 0)
                {
                    for (int j = 0; j < questController.quest[i].obj.Length; j++)
                    {
                        if (questController.quest[i].obj[j] != null)
                        {
                            qItem0[j].GetComponentsInChildren<Text>()[1].text = questController.quest[i].obj[j].name;
                            qItem0[j].GetComponentsInChildren<Text>()[0].text = questController.quest[i].obj[j].currentCount.ToString();
                        }
                        else
                        {
                            qItem0[j].GetComponentsInChildren<Text>()[1].text = "";
                            qItem0[j].GetComponentsInChildren<Text>()[0].text = "";
                            qItem1[j].GetComponentsInChildren<Text>()[1].text = "";
                            qItem1[j].GetComponentsInChildren<Text>()[0].text = "";
                        }
                    }
                }
                if (questController.quest[i].state.isState)
                {
                    qItem1[0].GetComponentsInChildren<Text>()[1].text = questController.quest[i].state.desc;
                    qItem1[0].GetComponentsInChildren<Text>()[0].text = questController.quest[i].currentValues[0];
                }
            }
            else
            {
                qElement[i].GetComponentInChildren<Text>().text = "";

            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        baby = GameObject.FindGameObjectWithTag("Baby").GetComponent<playerController>();
        diaperCharges = baby.gameObject.GetComponent<skillsController>().diaperCharges;
        questController = GameObject.FindGameObjectWithTag("GameController").GetComponent<questController>();
        warpPoint = GameObject.FindGameObjectsWithTag("WarpPoint");
        for (int i = 0; i < warpPoint.Length; i++)
        {
            warpButton[i].GetComponentInChildren<Text>().text = warpPoint[i].name;
        }
        showQuests();
    }

    // Update is called once per frame
    void Update()
    {
        diaperCharges = baby.gameObject.GetComponent<skillsController>().diaperCharges;
        diaperCount();
        //showItem();
        if (questController.updated)
        {
            foreach (questItem q in questController.quest)
            {
                if (q.obj.Length > 0)
                {
                    for (int i = 0; i < q.currentValues.Length; i++)
                    {
                        q.currentValues[i] = q.obj[i].currentCount.ToString() + "/" + q.obj[i].maxCount.ToString();
                        if (q.obj[i].currentCount == q.obj[i].maxCount)
                        {
                            q.completedItems++;
                        }
                    }
                    if (q.completedItems == q.obj.Length)
                    {
                        q.completed = true;
                    }
                }
            }
            showQuests();
            questController.updated = false;
        }
    }
}
