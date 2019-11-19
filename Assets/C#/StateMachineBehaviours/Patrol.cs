using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//patrol makes the character patroling around based on the route points
//while move makes the character walk to the destination
public class Patrol : StateMachineBehaviour
{
    public float tolerance = 0.1f;

    private int nextPoint = 0;
    protected Vector2[] routePoints;
    private bool nearDoor;
    private Transform transform;
    private Rigidbody2D rigid;
    private UnitBehaviour unit;
    private bool enterDoor = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        transform = animator.transform;
        routePoints = transform.GetComponent<UnitBehaviour>().GetRoute();
        rigid = transform.GetComponent<Rigidbody2D>();
        unit = animator.GetComponent<UnitBehaviour>();
        enterDoor = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        DoorCheck();
        //control the patroling of the unit
        if (nearDoor && RouteRangeCheck()) {
            Debug.Log("try go next floor");
            nextPoint += 2;
            nextPoint %= routePoints.Length;
            rigid.velocity = Vector2.zero;
            animator.SetBool("EnterDoor", true);
            enterDoor = true;
            return;
        } else if (RouteRangeCheck()) {
            nextPoint++;
            nextPoint %= routePoints.Length;
            Vector2 next = routePoints[nextPoint];
        }

        if (!enterDoor) {
            if (routePoints[nextPoint].x > transform.position.x) {
                rigid.velocity = Vector2.right * unit.velocity;
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

        nearDoor |= hit && hit.collider.GetComponent<DoorControl>() != null;
    }

    protected bool RouteRangeCheck()
    {
        //you only need to compare x-axies
        return
            Mathf.Abs(transform.position.x - routePoints[nextPoint].x) <= tolerance;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
