using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//this is a basic state machine for both pets and ghost
public class UnitStateMachine : MonoBehaviour, Unit
{
    public float velocity = 1f;
    public int health = 100;

    //how close is considered "at the point"
    public float tolerance = 0.1f;
    public float enterDoorTime = 0.5f;
    public float exitDoorTime = 0.5f;

    //the white ghost patroling through all these points
    public Vector2[] routePoints;

    private Vector3 originScale = new Vector3();
    private Rigidbody2D rigid;
    private int nextPoint = 0;
    private bool nearDoor = false;
    private bool entering = false;//entering the door
    private bool exiting = false;//exiting the door

    private DoorControl door = null;
    private UnitState state = UnitState.idle;

    public UnitState GetState()
    {
        return state;
    }

    public int GetHealth()
    {

        return 0;
    }

    public void TakeDamage(int damage)
    {

    }

    public void Kill()
    {

    }

    //this is called when the ghost enterd a door
    public void DoorEntered(DoorControl door)
    {
        //just change a bool value maybe?
        nearDoor = true;
        this.door = door;
        //play the animation
        //PlayAnim();
        //wait a few second
        //change the postion

        //play the animation
        //start walking again
    }

    public void DoorExited(DoorControl door)
    {
        nearDoor = false;
        if (this.door = door) this.door = null;
    }

    void PlayAnim()
    {
        //todo: play the animation based on the type of action.
    }

    void StateMachine()
    {
        switch (state) {
            case UnitState.walk:
                walk();
                break;
            case UnitState.attack:
                attack();
                break;
            case UnitState.enterDoor:
                EnterDoor();
                break;
            case UnitState.exitDoor:
                ExitDoor();
                break;
            case UnitState.idle:
                idle();
                break;
            case UnitState.die:
                die();
                break;
        }
    }

    void walk()
    {
        //control the patroling of the ghost
        if (nearDoor && RouteRangeCheck()) {
            Debug.Log("try go next floor");
            nextPoint++;
            rigid.velocity = Vector2.zero;
            state = UnitState.enterDoor;
        } else if (RouteRangeCheck()) {
            nextPoint++;
            Vector2 next = routePoints[nextPoint];
            rigid.velocity =
                (next.x - transform.position.x > 0 ? Vector2.right : Vector2.left) * velocity;
        } else {
            Vector2 next = routePoints[nextPoint];
            rigid.velocity =
                (next.x - transform.position.x > 0 ? Vector2.right : Vector2.left) * velocity;
        }
    }

    void EnterDoor()
    {
        Debug.Log("trying to enter door");
        //you want play the animation, wait, teleport and change state
        if (!entering) {
            Debug.Log("trying to enter door");
            entering = true;
            UnityAction exitDoor = delegate
            {
                transform.position = door.OtherEndPos();
                state = UnitState.exitDoor;
                entering = false;
            };
            StartCoroutine(PetUtility.LinearScaleFade(originScale, Vector3.zero, enterDoorTime, transform));
            PetUtility.WaitAndDo(enterDoorTime, exitDoor);
        }
    }

    void ExitDoor()
    {
        if (!exiting) {
            exiting = true;
            nextPoint++;
            UnityAction startWalking = delegate
            {
                state = UnitState.walk;
                exiting = false;
            };
            StartCoroutine(PetUtility.LinearScaleFade(Vector3.zero, originScale, exitDoorTime, transform));
            PetUtility.WaitAndDo(exitDoorTime, startWalking);
        }
    }

    void attack()
    {

    }

    void idle()
    {

    }

    void die()
    {

    }

    //check if the current position is close enough to next point
    bool RouteRangeCheck()
    {
        //you only need to compare x-axies
        return
            Mathf.Abs(transform.position.x - routePoints[nextPoint].x) <= tolerance;
    }

    //draw all the 
    void OnDrawGizmos()
    {
        if (routePoints.Length == 0) return;
        Gizmos.DrawLine(transform.position, routePoints[0]);
        for (int i = 0; i < routePoints.Length - 1; i++) {
            Gizmos.DrawLine(routePoints[i], routePoints[i + 1]);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        originScale = transform.localScale;
        state = UnitState.walk;
        rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine();
    }
}
