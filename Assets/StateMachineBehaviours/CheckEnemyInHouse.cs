using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//check if enemy is in house and set the bool variable
public class CheckEnemyInHouse : StateMachineBehaviour
{
    Pet pet;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pet = animator.GetComponent<Pet>();
        Ghost ghost = PetUtility.GetNearestGhost(pet.transform.position);
        if (ghost != null) {
            pet.enemyInHouse = true;
            pet.enemy = ghost.gameObject;
        } else {
            pet.enemyInHouse = false;
            pet.enemy = null;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Ghost ghost = PetUtility.GetNearestGhost(pet.transform.position);
        if (ghost != null) {
            pet.enemyInHouse = true;
            pet.enemy = ghost.gameObject;
        } else {
            pet.enemyInHouse = false;
            pet.enemy = null;
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
