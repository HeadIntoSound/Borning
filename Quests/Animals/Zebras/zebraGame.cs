using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zebraGame : MonoBehaviour
{
    public GameObject[] zebra;
    [SerializeField] int idx;
    [SerializeField] string salt;

    public void pickZebra(int forbidden)
    {
        foreach (GameObject z in zebra)
        {
            z.GetComponent<chosenZebra>().chosen = false;
        }
        do
        {
            idx = Random.Range(0, zebra.Length - 1);
        } while (idx == forbidden);
        zebra[idx].GetComponent<chosenZebra>().chosen = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        pickZebra(zebra.Length);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
