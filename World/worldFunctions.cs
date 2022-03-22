using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class worldFunctions : MonoBehaviour
{

    public Light2D worldLight;
    public cameraController cam;

    public IEnumerator transition(float startTime, float duration)
    {
        do
        {
            worldLight.intensity = Mathf.SmoothStep(0, 1, (Time.time - startTime) / duration);
            yield return null;
        } while (worldLight.intensity != 1);
    }

    public void pause()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
    }

    public IEnumerator cameraSpeed()
    {
        float aux = cam.camSpeed;
        cam.camSpeed = 10;
        yield return new WaitForSeconds(1.5f);
        cam.camSpeed = aux;
    }

    // Start is called before the first frame update
    void Start()
    {

    }
}
