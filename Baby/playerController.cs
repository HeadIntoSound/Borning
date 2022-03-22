using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.AI;

public class playerController : MonoBehaviour
{
    [Tooltip("0 for mouse / 1 for keyboard")]
    public int movementType;
    // 0 = mouse
    // 1 = keyboard
    // 2 = joystick

    public NavMeshAgent agent;
    public Tilemap map;
    public InputController InputController;
    public Camera cam;
    public Animator animator;
    public float speed;
    Vector2 mousePosition;
    public bool check = false;
    public bool inSafeZone;

    public Vector3 move;
    public bool isMoving = false;
    [HideInInspector] public GameObject activeMenu;

    //reference to the gameobject containing the player + mount model
    public GameObject mountedObject;
    coquiController coqui;

    // shadow variables. The shadow is ON when the baby is not visible by the camera
    public bool showShadow;
    [SerializeField] GameObject shadow;

    void keyboardMovement(int controlType, Vector2 axis)
    {
        //keyboard based movement
        if (controlType == 1)
        {

            if (Mathf.Abs(axis.x) < 1)
            {
                axis.y /= 2;
            }
            move = axis;
        }
    }

    void mouseMovement(int controlType)
    {
        //mouse based movement (walk only)
        if (controlType == 0)
        {
            mousePosition = InputController.Mouse.MousePosition.ReadValue<Vector2>();
            mousePosition = cam.ScreenToWorldPoint(mousePosition);
            GetComponent<NavMeshAgent>().SetDestination(mousePosition);
        }
    }

    void walk()
    {
        if (movementType == 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + move, Time.deltaTime * speed);
            if (move != Vector3.zero)
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }
        }
    }

    public IEnumerator freeze(float time)
    {
        float aux = speed;
        speed = 0;
        yield return new WaitForSeconds(time);
        speed = aux;
    }

    public void busted(Vector3 respawn)
    {
        if(coqui.mounted)
        {
            coqui.dismount();
            coqui.transform.position = coqui.respawnPouint.position;
        }
        transform.position = respawn;
    }

    void Awake()
    {
        InputController = new InputController();
        if (movementType == 0)
        {
            move = transform.position;
        }
        if (GetComponent<NavMeshAgent>())
        {
            agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            agent.updateUpAxis = false;
            agent.speed = speed;
        }
    }
    private void OnEnable()
    {
        InputController.Enable();
    }

    private void OnDisable()
    {
        InputController.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (movementType != 0 && GetComponent<NavMeshAgent>())
        {
            agent.enabled = false;
        }
        coqui = GameObject.FindGameObjectWithTag("Coqui").GetComponent<coquiController>();
        InputController.Mouse.MouseRightClick.performed += _ => mouseMovement(movementType);
        InputController.Keyboard.Movement.performed += ctx => keyboardMovement(movementType, ctx.ReadValue<Vector2>());
    }

    // Update is called once per frame
    void Update()
    {
        shadow.SetActive(showShadow);
        walk();
        if (movementType == 0)
        {
            if (agent.velocity.magnitude > .01f)
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }
        }
    }
}