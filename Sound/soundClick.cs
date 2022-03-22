using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundClick : MonoBehaviour
{
    audioManager aManager;
    [SerializeField] string clipName;
    [SerializeField] float cdTime = 2;
    InputController InputController;
    GameObject baby;
    bool canTrigger = true;

    IEnumerator cd()
    {
        canTrigger = false;
        yield return new WaitForSeconds(cdTime);
        canTrigger = true;
        yield break;
    }

    void playSound(Vector2 mousePos)
    {
        if (Vector2.Distance(transform.position,baby.transform.position)<1.5f)
        {
            Collider2D obj = clickController.clickedObject(mousePos);
            if (obj && obj.name == transform.name && canTrigger)
            {
                aManager.play(clipName);
                StartCoroutine(cd());
            }
        }
    }

    void Awake()
    {
        InputController = new InputController();
    }

    // Start is called before the first frame update
    void Start()
    {
        aManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<audioManager>();
        InputController.Mouse.MouseLeftClick.performed += _ => playSound(InputController.Mouse.MousePosition.ReadValue<Vector2>());
        baby = GameObject.FindGameObjectWithTag("Baby");
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
