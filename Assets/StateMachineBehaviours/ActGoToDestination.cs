using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this one get access to the destination of the unit behavior
//and figure out where to go on his own
public class ActGoToDestination : StateMachineBehaviour
{
    public float tolerance = 0.1f;

    private Vector2 nextPoint;
    private bool nearDoor;
    private Transform transform;
    private Rigidbody2D rigid;
    private UnitBehaviour unit;
    private bool enterDoor = false;
    private Vector2 oldValue = new Vector2();
    private bool arrive;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        transform = animator.transform;
        rigid = transform.GetComponent<Rigidbody2D>();
        unit = animator.GetComponent<UnitBehaviour>();
        nextPoint = PetUtility.FindNextWayPoint(unit.transform.position, unit.destination);
        enterDoor = false; unit.enterDoor = false;
        UpdataRoute();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        UpdataRoute();
    }

    private void UpdataRoute()
    {
        if (RouteRangeCheck() && nextPoint.Equals(unit.destination)) {
            arrive = true;
            rigid.velocity = Vector2.zero;
        } else arrive = false;

        if (arrive) {
            return;
        }

        if (!oldValue.Equals(unit.destination)) {
            nextPoint = PetUtility.FindNextWayPoint(unit.transform.position, unit.destination);
            oldValue.Set(unit.destination.x, unit.destination.y);
        }

        DoorCheck();
        if (nearDoor) {
            nextPoint = PetUtility.FindNextWayPoint(unit.transform.position, unit.destination);
            //Debug.Log(nextPoint);
        }
        nextPoint = PetUtility.FindNextWayPoint(unit.transform.position, unit.destination);

        //control the patroling of the unit
        if (nearDoor && RouteRangeCheck()) {
            Debug.Log("try go next floor");
            rigid.velocity = Vector2.zero;
            enterDoor = true;
            unit.enterDoor = true;
            return;
        } else if (RouteRangeCheck()) {
            nextPoint = PetUtility.FindNextWayPoint(unit.transform.position, unit.destination);
        }

        if (!enterDoor) {
            if (nextPoint.x > transform.position.x) {
                rigid.velocity = Vector2.right * unit.velocity;
                //todo:maybe the flip could be done by unitbehaviour
                transform.rotation = Quaternion.Euler(0, 0, 0);
            } else {
                rigid.velocity = Vector2.left * unit.velocity;
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    protected void DoorCheck()
    {
        // cast a ray to next point see if it hits a door

        RaycastHit2D hit;

        Vector2 origin = (Vector2)transform.position;
        hit = Physics2D.Raycast(origin, Vector2.right, 0.01f, LayerMask.GetMask("Door"));

        //only set it if the other end is actually worth going to
        if (hit && hit.collider.GetComponent<DoorControl>() != null) {
            if ((hit.collider.GetComponent<DoorControl>().OtherEndPos() - unit.destination).magnitude
                < ((Vector2)hit.collider.GetComponent<DoorControl>().transform.position - unit.destination).magnitude) {
                nearDoor = true;
                unit.door = hit.collider.GetComponent<DoorControl>();
            } else {
                nearDoor = false;
                unit.door = null;
            }
        } else {
            nearDoor = false;
            unit.door = null;
        }

        //nearDoor |= hit && hit.collider.GetComponent<DoorControl>() != null;
    }

    protected bool RouteRangeCheck()
    {
        //you only need to compare x-axies
        return
            Mathf.Abs(transform.position.x - nextPoint.x) <= tolerance;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rigid.velocity = Vector2.zero;
    }
}
