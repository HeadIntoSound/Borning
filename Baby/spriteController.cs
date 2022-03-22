using System.Collections;
using UnityEngine;

public class spriteController : MonoBehaviour
{

    public playerController baby;
    Animator anim;
    Vector2 direction;
    public int facing;
    public bool diaper;
    public bool grab;
    public bool fishing;
    public bool fishingIdle;
    public string idleName = "Idle_0";
    public string walkName = "Walk_0";
    public string fishName = "Fish_0";
    public string fishIdleName = "FishIdle_0";
    // Start is called before the first frame update
    void Start()
    {
        baby = GameObject.FindGameObjectWithTag("Baby").GetComponent<playerController>();
        anim = GetComponent<Animator>();
        baby.InputController.Keyboard.Movement.performed += ctx => StartCoroutine(keyFacing(ctx.ReadValue<Vector2>()));
        baby.InputController.Mouse.MouseRightClick.performed += _ => StartCoroutine(mouseFacing());
        StartCoroutine(animations());
    }

    IEnumerator keyFacing(Vector2 direction)
    {
        yield return new WaitForSeconds(0.05f);
        if (baby.isMoving && direction != Vector2.zero && baby.movementType == 1 && baby.speed != 0)
        {
            if (direction == Vector2.down)
            {
                facing = 0;
                //print("facing S");
                //baby.animator.SetInteger("Facing", numero de S);
            }
            if (direction.x <= -.6f && direction.x >= -.8f && direction.y <= -.6f && direction.y >= -.8f)
            {
                facing = 1;
                //print("facing SW");
                //baby.animator.SetInteger("Facing", 2);
            }
            if (direction == Vector2.left)
            {
                facing = 2;
                //print("facing W");
                //baby.animator.SetInteger("Facing", numero de W);
            }
            if (direction.x <= -.6f && direction.x >= -.8f && direction.y >= .6f && direction.y <= .8f)
            {
                facing = 3;
                //print("facing NW");
                //baby.animator.SetInteger("Facing", 3);
            }
            if (direction == Vector2.up)
            {
                facing = 4;
                //print("facing N");
                //baby.animator.SetInteger("Facing", numero de N);
            }
            if (direction.x >= .6f && direction.x <= .8f && direction.y >= .6f && direction.y <= .8f)
            {
                facing = 5;
                //print("facing NE");
                //baby.animator.SetInteger("Facing", 0);
            }
            if (direction == Vector2.right)
            {
                facing = 6;
                //print("facing E");
                //baby.animator.SetInteger("Facing", numero de E);
            }
            if (direction.x >= .6f && direction.x <= .8f && direction.y <= -.6f && direction.y >= -.8f)
            {
                facing = 7;
                //print("facing SE");
                //baby.animator.SetInteger("Facing", 1);
            }

        }
    }

    IEnumerator mouseFacing()
    {
        if (baby.movementType == 0 && baby.agent.speed != 0)
        {
            yield return new WaitForSeconds(0.15f);
            direction = baby.agent.destination - baby.transform.position;
            if (direction.x > -.3 && direction.x < .3 && direction.y < 0)
            {
                facing = 0;
                // faces S
            }
            if (direction.x < -.3 && direction.y < -.3)
            {
                facing = 1;
                //print("facing SW");
            }
            if (direction.x < 0 && direction.y > -.3 && direction.y < .3)
            {
                facing = 2;
                // faces W
            }
            if (direction.x < -.3 && direction.y > .3)
            {
                facing = 3;
                //print("facing NW");
            }
            if (direction.x >= -.3 && direction.x <= .3 && direction.y > 0)
            {
                facing = 4;
                // faces N
            }
            if (direction.x > .3 && direction.y > .3)
            {
                facing = 5;
                //print("facing NE");
            }
            if (direction.x > 0 && direction.y >= -.3 && direction.y <= .3)
            {
                facing = 6;
                // faces E
            }
            if (direction.x > .3 && direction.y < -.3)
            {
                facing = 7;
                //print("facing SE");
            }

        }
    }

    IEnumerator animations()
    {
        while (true)
        {
            walkName = walkName.Remove(walkName.Length - 1).Insert(walkName.Length - 1, facing.ToString());
            idleName = idleName.Remove(idleName.Length - 1).Insert(idleName.Length - 1, facing.ToString());
            if (diaper)
            {
                anim.Play("Diaper_" + facing.ToString());
            }
            if (grab)
            {
                anim.Play("Grab_" + facing.ToString());
                float iniSpeed = baby.speed;
                baby.speed = 0;
                yield return new WaitForSeconds(1f);
                grab = false;
                baby.speed = iniSpeed;
            }
            if (fishing)
            {
                float aux = baby.speed;
                baby.speed = 0;
                fishName = fishName.Remove(fishName.Length - 1).Insert(fishName.Length - 1, facing.ToString());
                fishIdleName = fishIdleName.Remove(fishIdleName.Length - 1).Insert(fishIdleName.Length - 1, facing.ToString());
                anim.Play(fishName);
                yield return new WaitForSeconds(1);
                fishing = false;
                fishingIdle = true;
                anim.Play(fishIdleName);
                baby.speed = aux;
            }
            if (fishingIdle)
            {
                anim.Play(fishIdleName);
            }
            if (baby.isMoving && baby.speed != 0 && !diaper && !grab && !fishingIdle && !fishing)
            {
                anim.Play(walkName);
            }
            if (!baby.isMoving && !diaper && !grab && !fishingIdle && !fishing)
            {
                anim.Play(idleName);
            }

            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
