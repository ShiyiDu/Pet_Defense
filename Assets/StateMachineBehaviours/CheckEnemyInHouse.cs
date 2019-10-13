using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//check if enemy is in house and set the bool variable
public class CheckEnemyInHouse : StateMachineBehaviour
{
    //todo: move this to unit behaviour is a better idea
    Pet pet;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pet = animator.GetComponent<Pet>();
        CheckGhost();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CheckGhost();
    }

    private void CheckGhost()
    {
        Ghost ghost = null;
        if (!pet.upstairFirst) {
            if (pet.checkDownstair) ghost = PetUtility.GetDownstairGhost(pet.transform.position);
            if (pet.checkUpstair && (ghost == null)) {
                PetUtility.GetUpstairGhost(pet.transform.position);
                //Debug.Log("check downstair");
            }
        } else {
            if (pet.checkUpstair) ghost = PetUtility.GetUpstairGhost(pet.transform.position);
            if (pet.checkDownstair && (ghost == null)) {
                PetUtility.GetDownstairGhost(pet.transform.position);
                //Debug.Log("check downstair");
            }
        }
        if (ghost != null) {
            //Debug.Log("we found ghost: " + ghost.name);
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
