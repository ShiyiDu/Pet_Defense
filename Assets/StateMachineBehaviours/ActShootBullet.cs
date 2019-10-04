using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActShootBullet : StateMachineBehaviour
{
    private Bullet bullet;
    private GameObject gameObject;
    private UnitBehaviour unit;
    private float attackInterval;
    private float timer;
    private GameObject newBullet;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        gameObject = animator.gameObject;
        unit = gameObject.GetComponent<UnitBehaviour>();
        bullet = unit.bullet;
        attackInterval = unit.attackInterval;
        timer = attackInterval;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer -= Time.deltaTime;
        if (timer <= 0) {
            newBullet = Instantiate(bullet.gameObject, unit.GetShootPosition(), Quaternion.identity);
            newBullet.GetComponent<Bullet>().Initialize(unit.damage, unit.bulletVelocity, unit.GetFaceDirection());
            timer = attackInterval;
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
