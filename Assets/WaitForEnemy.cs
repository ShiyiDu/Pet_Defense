using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//wait for enemy updates the destination based on the current state.
//this is a pet specific behaviour
public class WaitForEnemy : StateMachineBehaviour
{
    private float updateFrequence = 0.1f;
    private float timer;
    private Pet unit;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = updateFrequence;
        unit = animator.gameObject.GetComponent<Pet>();
        if (unit == null) Debug.LogWarning("did not find a pet component");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0) {
            UpdateDestination(animator);
            timer = updateFrequence;
        }
        timer -= Time.deltaTime;
    }

    void UpdateDestination(Animator animator)
    {
        if (unit == null) {
            Debug.LogError("the unit was null, trying to get it again");
            unit = animator.gameObject.GetComponent<Pet>();
        }
        Ghost nearest;
        if ((nearest = PetUtility.GetNearestGhost(unit.transform.position)) != null) {
            unit.destination = nearest.transform.position;
            animator.SetBool("GoToEnemy", true);
        } else {
            unit.destination = unit.GetBed().transform.position;
            animator.SetBool("GoToEnemy", false);
        }
        //change the destination to bed if no enemy
        //change it to enemy if found one
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

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
