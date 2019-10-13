using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//check if i'm near a bed and set nearbed variable
public class CheckBedNearby : StateMachineBehaviour
{
    public float tolerence = 0.05f;
    Pet pet;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pet = animator.gameObject.GetComponent<Pet>();
        //Debug.Log("bed position: " + pet.GetBed().transform.position);
        bool close = Mathf.Abs(pet.GetBed().transform.position.x - pet.transform.position.x) <= tolerence;
        bool sameFloor = PetUtility.GetFloorNumber(pet.GetBed().transform.position) == PetUtility.GetFloorNumber(pet.transform.position);

        if (close && sameFloor)
            pet.bedNearby = true;
        else
            pet.bedNearby = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bool close = Mathf.Abs(pet.GetBed().transform.position.x - pet.transform.position.x) <= tolerence;
        bool sameFloor = PetUtility.GetFloorNumber(pet.GetBed().transform.position) == PetUtility.GetFloorNumber(pet.transform.position);

        if (close && sameFloor)
            pet.bedNearby = true;
        else
            pet.bedNearby = false;
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
