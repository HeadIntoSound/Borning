using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rectMovement : MonoBehaviour
{
    public float speed;
    public float time;
    RectTransform rectTransform;
    Vector2 point;
    bool hooked;
    [SerializeField] GameObject indicator;
    [SerializeField] RectTransform nextStop;
    float initialSpeed;
    float startTime;
    bool caught;
    void move()
    {
        if(!caught)
        {
            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, point, Time.deltaTime * speed);
        }
        if (Vector2.Distance(rectTransform.anchoredPosition, point) < .1f)
        {
            point = new Vector2(Random.Range(-205, 205), Random.Range(-105, 105));
            nextStop.anchoredPosition = point;
        }
        if(Vector2.Distance(rectTransform.anchoredPosition,point)<1)
        {
            speed *= .75f; 
        }
        else
        {
            speed = initialSpeed;
        }
    }

    void winCondition()
    {
        if(indicator.activeSelf)
        {
            if(Time.time >= startTime + time)
            {
                caught = true;
            }
        }
        else
        {
            startTime = 0;
        }
    }

    IEnumerator fishing()
    {
        yield return new WaitForSeconds(.15f);
        while (hooked)
        {
            indicator.SetActive(true);
            yield return null;
        }
        yield return new WaitForSeconds(.25f);
        indicator.SetActive(false);

    }

    IEnumerator setTime()
    {
        startTime = Time.time;
        yield return new WaitForSeconds(time+0.5f);
    }

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        point = Vector2.zero;
        initialSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        move();
        if (hooked)
        {
            indicator.SetActive(true);
        }
        else
        {
            indicator.SetActive(false);
        }
        winCondition();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Hook")
        {
            hooked = true;
            StartCoroutine(setTime());
        }

    }
    private void OnTriggerStay2D(Collider2D other)
    {
        //StartCoroutine(fishing());
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Hook")
        {
            hooked = false;
        }
    }
}
