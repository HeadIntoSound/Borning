using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class warpMenuController : MonoBehaviour
{
    [SerializeField]playerController baby;
    Vector3 mouseClick;
    Vector3 menuPos;
    [SerializeField]GameObject menu;
    Camera cam;

    void showMenu()
    {
        
        mouseClick = baby.InputController.Mouse.MousePosition.ReadValue<Vector2>();
        mouseClick = cam.ScreenToWorldPoint(mouseClick);
        if(Vector3.Distance(baby.transform.position,transform.position)<0.4f && Vector2.Distance(mouseClick,transform.position)<.15f && menu.activeSelf == false)
        {
            Time.timeScale = 0;
            menu.SetActive(true);
            baby.activeMenu = menu;
            menuPos = Camera.main.ScreenToWorldPoint(menu.transform.position);
        }
        // else if(menu.activeSelf && Vector2.Distance(mouseClick,menuPos)>1)
        // {
        //     Time.timeScale = 1;
        //     menu.SetActive(false);
        // }
    }

    void Awake()
    {
        baby = GameObject.FindGameObjectWithTag("Baby").GetComponent<playerController>();
        cam = Camera.main;
    }
    // Start is called before the first frame update
    void Start()
    {
        //menu = GameObject.FindGameObjectWithTag("WarpMenu");
        //menu.SetActive(false);
        baby.InputController.Mouse.MouseLeftClick.performed += _ => showMenu();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
