using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class displayMessage : MonoBehaviour
{
    [SerializeField]GameObject text;

    public IEnumerator completed()
    {
        text.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        text.SetActive(false);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
