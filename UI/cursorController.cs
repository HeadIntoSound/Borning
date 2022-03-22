using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursorController : MonoBehaviour
{
    public InputController InputController;
    RectTransform rectTransform;
    public RectTransform otherObject;
    public Camera cam;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        InputController = GameObject.FindGameObjectWithTag("Baby").GetComponent<grabController>().InputController;
        rectTransform = GetComponent<RectTransform>();
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 pos = cam.ScreenToWorldPoint(InputController.Mouse.MousePosition.ReadValue<Vector2>()) + offset;
        pos.z = otherObject.position.z;
        rectTransform.position = pos;

    }
}
