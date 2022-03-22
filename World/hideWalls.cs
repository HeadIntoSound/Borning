using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class hideWalls : MonoBehaviour
{
    public Tilemap toHide;
    private Color transparent;
    public GameObject[] objectToHide;
    Color initialColor;
    playerController baby;


    void colorControl(Color color)
    {

        if (toHide)
        {
            toHide.color = color;
        }
        if (objectToHide.Length > 0)
        {
            foreach (GameObject obj in objectToHide)
            {
                obj.GetComponent<SpriteRenderer>().color = color;
            }
        }
    }

    void Start()
    {
        initialColor = new Color(1, 1, 1, 1);
        transparent = new Color(1, 1, 1, 0.33f);
        baby = GameObject.FindGameObjectWithTag("Baby").GetComponent<playerController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Baby"))
        {
            //colorControl(transparent);
            baby.showShadow = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Baby"))
        {
            //colorControl(initialColor);
            baby.showShadow = false;
        }
    }
}
