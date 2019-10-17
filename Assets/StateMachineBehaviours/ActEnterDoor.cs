using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActEnterDoor : StateMachineBehaviour
{
    public float enterTime = 0.5f;
    public float exitTime = 0.5f;

    private Vector3 originScale;
    private bool entering = false;
    private GameObject gameObject;
    private DoorControl door;
    private Animator animator;
    private UnitBehaviour unit;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        this.animator = animator;
        gameObject = animator.gameObject;
        originScale = gameObject.transform.localScale;
        unit = gameObject.GetComponent<UnitBehaviour>();

        RaycastHit2D hit;
        hit = Physics2D.Raycast(gameObject.transform.position, Vector2.up, 0.01f, LayerMask.GetMask("Door"));
        door = hit.collider.GetComponent<DoorControl>();
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        Enter();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    private void Enter()
    {
        //Debug.Log("enter door");
        //you want play the animation, wait, teleport and change state
        if (!entering) {
            entering = true;
            void exitDoor()
            {
                gameObject.transform.position = door.OtherEndPos();
                entering = false;
                Exit();
            }
            PetUtility.Coroutine
                (PetUtility.LinearScaleFade(originScale, Vector3.zero, enterTime, gameObject.transform));
            PetUtility.WaitAndDo(enterTime, exitDoor);
        }
    }

    private void Exit()
    {
        void startWalking()
        {
            unit.enterDoor = false;
        }
        PetUtility.Coroutine(PetUtility.LinearScaleFade(Vector3.zero, originScale, exitTime, gameObject.transform));
        PetUtility.WaitAndDo(exitTime, startWalking);
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
