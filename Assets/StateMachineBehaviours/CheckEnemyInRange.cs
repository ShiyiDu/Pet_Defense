using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnemyInRange : StateMachineBehaviour
{
    private bool iAmPet = false;
    private UnitBehaviour unit;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        unit = animator.gameObject.GetComponent<Pet>();
        if (unit != null) iAmPet = true;
        else {
            iAmPet = false;
            unit = animator.gameObject.GetComponent<Ghost>();
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 direction = unit.GetFaceDirection();
        Vector2 target = direction.normalized;
        RaycastHit2D hit;
        int layerMask = LayerMask.GetMask(iAmPet ? "Ghost" : "Pet");
        hit = Physics2D.Raycast(unit.transform.position, target, unit.attackRange, layerMask);
        if (hit) {
            unit.enemyInRange = true;
            //unit.enemy = hit.collider.gameObject;
        } else {
            unit.enemyInRange = false;
            //unit.enemy = null;
        }

    }

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
