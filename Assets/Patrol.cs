using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : StateMachineBehaviour
{
    public float tolerance = 0.1f;

    private static int nextPoint = 0;
    protected Vector2[] routePoints;
    private bool nearDoor;
    private Transform transform;
    private Rigidbody2D rigid;
    private UnitBehaviour unit;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        transform = animator.transform;
        routePoints = transform.GetComponent<UnitBehaviour>().GetRoute();
        rigid = transform.GetComponent<Rigidbody2D>();
        unit = animator.GetComponent<UnitBehaviour>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        DoorCheck();
        //control the patroling of the unit
        if (nearDoor && RouteRangeCheck()) {
            Debug.Log("try go next floor");
            nextPoint += 2;
            animator.SetBool("EnterDoor", true);
            rigid.velocity = Vector2.zero;
            return;
        } else if (RouteRangeCheck()) {
            nextPoint++;
            Vector2 next = routePoints[nextPoint];
        }

        rigid.velocity = routePoints[nextPoint].x > transform.position.x ?
            Vector2.right * unit.velocity : Vector2.left * unit.velocity;
    }

    protected void DoorCheck()
    {
        // cast a ray to next point see if it hits a door
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, Vector2.up, 0.01f);
        if (hit.collider.GetComponent<DoorControl>()) nearDoor = true;
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
