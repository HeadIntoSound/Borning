using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabController : MonoBehaviour
{
    public playerController baby;
    public InputController InputController;
    public zebraGame zebraGame;
    public bool holding;
    public bool placing;
    string prefabName;
    string prefabPath;
    [SerializeField] coquiController coqui;
    public babyItems babyItems;
    questController questController;
    [SerializeField] GameObject giraffeTrigger;
    [SerializeField] inventoryController inventory;
    [SerializeField] GameObject handItem;

    void deleteHandItem()
    {
        babyItems.handObject = null;
        handItem.SetActive(false);
        holding = false;
    }

    void action(Vector2 mousePos)
    {
        if (!placing && !coqui.mounted)
        {
            Collider2D obj = clickController.clickedObject(mousePos);

            
            if (obj && clickController.canInteract(mousePos, transform.position))
            {
                obj.name = obj.name.Replace("(Clone)", "");
                switch (obj.tag)
                {

                    case "Prop":
                        baby.GetComponent<spriteController>().grab = true;
                        // if (!holding)
                        // {
                        //     babyItems.handObject = (GameObject)Resources.Load("Prefabs/" + OGname, typeof(GameObject));
                        //     holding = true;
                        // }
                        inventory.addItem(obj.gameObject,.6f);
                        break;

                    case "MissionItem":
                        inventory.addItem(obj.gameObject,.6f);
                        baby.GetComponent<spriteController>().grab = true;
                        questController.progress(obj.name);
                        break;

                    case "Food":
                        baby.GetComponent<spriteController>().grab = true;
                        inventory.addItem(obj.gameObject,.25f);
                        break;

                    case "Coqui":
                        coqui.mount();
                        break;

                    case "Feather":
                        baby.GetComponent<spriteController>().grab = true;
                        babyItems.feathers++;
                        Destroy(obj.gameObject, 0.6f);
                        break;

                    case "Rubbish":
                        baby.GetComponent<spriteController>().grab = true;
                        babyItems.rubbish++;
                        inventory.addItem(obj.gameObject,.6f);
                        break;

                    case "Zebra":
                        if(obj.GetComponent<chosenZebra>().chosen)
                        {
                            StartCoroutine(GameObject.FindGameObjectWithTag("GameController").GetComponent<displayMessage>().completed());
                            questController.quest[1].currentValues[0] = "1/1";
                            questController.updated = true;
                        }
                        else
                        {
                            zebraGame.pickZebra(obj.GetComponent<chosenZebra>().idxNumber);
                        }
                        break;

                    case "Giraffe":
                        print(babyItems.handObject.name);
                        if(babyItems.handObject.name == "Apple")
                        {
                            deleteHandItem();
                            giraffeTrigger.SetActive(true);
                            print("done");
                        }
                        break;

                    case "TalkInteraction":
                        itemDeliveryController delivery = obj.GetComponent<itemDeliveryController>();
                        if (!obj.GetComponent<talkController>().cd)
                        {
                            StartCoroutine(obj.GetComponent<talkController>().talk());
                        }
                        if (babyItems.handObject != null || babyItems.missionItems.Count > 0 && delivery.wantsItem)
                        {
                            foreach (GameObject item in delivery.desiredItems)
                            {
                                if (item.CompareTag(babyItems.handObject.tag))
                                {
                                    print("yes");
                                }
                                else
                                {
                                    foreach (GameObject babyItem in babyItems.missionItems)
                                    {
                                        if (babyItems.CompareTag(item.tag))
                                        {
                                            print("yes");
                                        }
                                    }
                                }
                            }
                        }
                        break;

                }
            }
        }
        
        if (placing && babyItems.handObject)
        {
            //print("hello");
            mousePos = clickController.worldPoint(mousePos);
            if (!Physics2D.OverlapCircle(mousePos, .1f) && (mousePos-new Vector2(transform.position.x,transform.position.y)).sqrMagnitude < .66f)
            {
                babyItems.handObject = Object.Instantiate(babyItems.handObject, mousePos, Quaternion.identity);
                babyItems.handObject.transform.position -= new Vector3(0, 0, babyItems.handObject.transform.position.z);
                //show that the item's been placed
                placing = false;
                holding = false;
                babyItems.handObject = null;
            }
        }
    }

    void select(float inputInfo)
    {
        if (inputInfo == 3 && holding && !coqui.mounted)
        {
            //show that is placing
            placing = true;
        }
    }

    void Awake()
    {
        baby = GetComponent<playerController>();
        InputController = new InputController();
        coqui = GameObject.FindGameObjectWithTag("Coqui").GetComponent<coquiController>();
        babyItems = GetComponent<babyItems>();
    }

    // Start is called before the first frame update
    void Start()
    {
        InputController.Keyboard.Skills.performed += ctx => select(ctx.ReadValue<float>());
        InputController.Mouse.MouseLeftClick.performed += _ => action(InputController.Mouse.MousePosition.ReadValue<Vector2>());
        InputController.Mouse.MouseRightClick.performed += _ => placing = false;
        questController = GameObject.FindGameObjectWithTag("GameController").GetComponent<questController>();
    }

    // Update is called once per frame
    void Update()
    {
        handItem.SetActive(holding);
    }
    private void OnEnable()
    {
        InputController.Enable();
    }

    private void OnDisable()
    {
        InputController.Disable();
    }
}