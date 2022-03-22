using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillsController : MonoBehaviour
{
    audioManager aManager;
    public GameObject[] AI;
    public InputController InputController;
    bool held = false;
    playerController baby;
    public GameObject pauseMenu;
    public bool pause;
    public GameObject inventory;

    //animation variables
    spriteController spriteAnimation;

    //crawl variables
    float baseSpeed;

    //diaper variables
    bool canPoop = true;
    public GameObject caca;
    public float diaperDuration = 5;
    public int diaperCharges = 2;
    public float diaperRadius;
    Vector3 offset;
    Vector3 targetOffset;

    //mount variables
    coquiController coqui;

    void diaperOffset()
    {
        switch (spriteAnimation.facing)
        {
            case 0:
                offset = Vector3.up * 0.12f;
                targetOffset = new Vector3(0, .36f, 0);
                break;
            case 1:
            case 2:
                offset = new Vector3(.05f, .12f, 0);
                targetOffset = new Vector3(.45f, .1f, 0);
                break;
            case 3:
                offset = new Vector3(.075f, 0, 0);
                targetOffset = new Vector3(.425f, -.25f, 0);
                break;
            case 4:
                offset = Vector3.down * 0.12f;
                targetOffset = new Vector3(0, -.36f, 0);
                break;
            case 5:
                offset = new Vector3(-.075f, 0, 0);
                targetOffset = new Vector3(-.425f, -.25f, 0);
                break;
            case 6:
            case 7:
                offset = new Vector3(-.05f, .12f, 0);
                targetOffset = new Vector3(-.45f, .1f, 0);
                break;
        }
    }

    IEnumerator poopCD()
    {
        yield return new WaitForSeconds(2);
        canPoop = true;
    }

    IEnumerator placeDiaper()
    {
        canPoop = false;
        float initialSpeed = baby.speed;
        baby.speed = 0;
        spriteAnimation.diaper = true;
        diaperOffset();

        Vector3 target = transform.position + offset;
        GameObject diaper = Object.Instantiate(caca, target, Quaternion.identity);
        aManager.play("Diaper");
        StartCoroutine(diaper.GetComponent<diaperCleanTrigger>().instanciateCollider());
        StartCoroutine(diaper.GetComponent<diaperCleanTrigger>().initialMove(target + targetOffset));

        yield return new WaitForSeconds(.7f);
        baby.speed = initialSpeed;
        spriteAnimation.diaper = false;
        diaperCharges--;
        StartCoroutine(poopCD());

        foreach (GameObject NPC in AI)
        {
            if (Vector2.Distance(diaper.transform.position, NPC.transform.position) <= diaperRadius)
            {
                NPC.GetComponent<AIController>().agent.speed = 0;
            }
        }

        //might want to run an animation and display something here
        Destroy(diaper, diaperDuration);
        yield return new WaitForSeconds(diaperDuration);
        foreach (GameObject NPC in AI)
        {
            if (NPC.GetComponent<AIController>().agent.speed == 0)
            {
                NPC.GetComponent<AIController>().agent.speed = NPC.GetComponent<AIController>().initialSpeed;
            }
        }
    }

    IEnumerator crawl()
    {
        if (held)
        {
            baby.speed = .5f;
        }
        else
        {
            baby.speed = baseSpeed;
        }
        yield return new WaitForSeconds(.5f);
    }

    void skillSelector(float skill)
    {
        switch (skill)
        {
            case 0:
                held = !held;
                StartCoroutine(crawl());
                break;
            case 1:
                print("nothing here anymore");
                break;
            case 2:
                if (diaperCharges > 0 && canPoop)
                {
                    StartCoroutine(placeDiaper());

                }
                break;
            case 4:
                if (coqui.mounted)
                {
                    coqui.dismount();
                }
                else
                {
                    if (Vector3.Distance(transform.position, coqui.transform.position) < .66f)
                    {
                        coqui.mount();
                    }
                }
                break;
            case 5:
                if (baby.activeMenu != null && baby.activeMenu.CompareTag("WarpMenu"))
                {
                    baby.activeMenu.SetActive(false);
                    Time.timeScale = 1;
                }
                if (baby.activeMenu == null || !baby.activeMenu.activeSelf || !inventory.activeSelf)
                {
                    pauseMenu.SetActive(!pauseMenu.activeSelf);
                    pause = !pause;
                    pauseGame();
                }
                if (inventory.activeSelf)
                {
                    inventory.SetActive(false);
                }
                break;
            case 6:
                if (!pause)
                {
                    if (inventory.activeSelf)
                    {
                        inventory.SetActive(false);
                        aManager.play("Zip");
                    }
                    else
                    {
                        inventory.SetActive(true);
                        aManager.play("Unzip");
                    }
                }
                break;
        }
    }

    public void pauseGame()
    {
        if (pause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    void Awake()
    {
        InputController = new InputController();
        AI = GameObject.FindGameObjectsWithTag("AI");
        baby = GetComponent<playerController>();
        coqui = GameObject.FindGameObjectWithTag("Coqui").GetComponent<coquiController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        aManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<audioManager>();
        spriteAnimation = GetComponent<spriteController>();
        baseSpeed = baby.speed;
        InputController.Keyboard.Skills.performed += ctx => skillSelector(ctx.ReadValue<float>());
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //crawl();
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
