using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetect : StateMachineBehaviour
{
    //this script will try to detect enemy and go to attack once found
    public bool iAmPet = false;
    private UnitBehaviour unit;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        unit = animator.gameObject.GetComponent<UnitBehaviour>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Direction direction = unit.GetFaceDirection();
        Vector2 target = direction == Direction.left ? Vector2.left : Vector2.right;
        RaycastHit2D hit;
        int layerMask = LayerMask.GetMask(iAmPet ? "Ghost" : "Pet");
        hit = Physics2D.Raycast(unit.transform.position, target, unit.attackRange, layerMask);
        if (hit) {
            animator.SetBool("EnemySpoted", true);
            unit.enemy = hit.collider.gameObject;
        } else {
            animator.SetBool("EnemySpoted", false);
            unit.enemy = null;
        }

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
