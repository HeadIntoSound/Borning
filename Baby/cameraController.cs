using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    // Start is called before the first frame update

    public playerController baby;
    public Camera cam;
    Vector3 initalPos;
    Vector3 offset;
    public float maxDist;
    public float minDist;
    public float camSpeed;
    public float camDelay;
    void Start()
    {
        baby = GameObject.FindGameObjectWithTag("Baby").GetComponent<playerController>();
        initalPos = baby.transform.position;
        offset = new Vector3(0, 0, -10);
        cam.transform.position = baby.transform.position + offset;
        StartCoroutine(afkCamera());
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(initalPos,.075f);
    }

    IEnumerator afkCamera()
    {
        while(true)
        {
            yield return new WaitForSeconds(camDelay);
            if(!baby.isMoving)
            {
                initalPos = baby.transform.position;
                if(initalPos == baby.transform.position)
                {
                    
                }

            }
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(baby.transform.position, initalPos) > minDist && Vector3Int.Distance(Vector3Int.FloorToInt(baby.transform.position), Vector3Int.FloorToInt(initalPos)) < maxDist)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, baby.transform.position + offset, Time.deltaTime * camSpeed);
        }
        if (Vector3Int.Distance(Vector3Int.FloorToInt(baby.transform.position), Vector3Int.FloorToInt(initalPos)) >= maxDist)
        {
            initalPos = baby.transform.position;
        }
        if(!baby.isMoving && initalPos == baby.transform.position)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, baby.transform.position + offset, Time.deltaTime * camSpeed);
        }
    }
}
