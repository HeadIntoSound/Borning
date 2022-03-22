using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turtleRescue : MonoBehaviour
{
    [SerializeField]questController questController;
    [SerializeField]string questTitle = "Rescue the Turtle!";
    [SerializeField] GameObject turtle;
    // Start is called before the first frame update
    void Start()
    {
        questController = GameObject.FindGameObjectWithTag("GameController").GetComponent<questController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.CompareTag("Baby"))
        {
            foreach(questItem q in questController.quest)
            {
                if(q.title == questTitle)
                {
                    turtle.SetActive(false);
                    StartCoroutine(GameObject.FindGameObjectWithTag("GameController").GetComponent<displayMessage>().completed());
                    //questController.quest[1].currentValues[0] = "1/1";
                }
            }
        }
    }
}
