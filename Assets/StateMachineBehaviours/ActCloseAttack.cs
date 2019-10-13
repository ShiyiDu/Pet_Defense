using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActCloseAttack : StateMachineBehaviour
{
    GameObject gameObject;
    Rigidbody2D rigid;
    float timer;
    UnitBehaviour unit;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        gameObject = animator.gameObject;
        rigid = gameObject.GetComponent<Rigidbody2D>();
        timer = gameObject.GetComponent<UnitBehaviour>().attackInterval;
        unit = gameObject.GetComponent<UnitBehaviour>();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rigid.velocity = Vector2.zero;
        AttackAction();
    }

    protected void AttackAction()
    {
        timer -= Time.deltaTime;
        if (timer <= 0) {
            //Debug.Log("ghost attacking");
            PetUtility.Coroutine(LaunchAttack());
            if (unit.enemy != null) {
                //Debug.Log("try get enemy");
                UnitBehaviour enemy = unit.enemy.GetComponent<UnitBehaviour>();
                //Debug.Log(enemy != null);
                int damage = unit.damage;
                enemy.TakeDamage(damage);
            }
            timer = unit.attackInterval;
        }
        //what does attack look like?
        //move to the direction and comback
    }

    IEnumerator LaunchAttack()
    {
        //if (unit.enemy.transform.position.x - gameObject.transform.position.x > 0) {
        //    enemyDirection = Direction.right;
        //} else {
        //    enemyDirection = Direction.left;
        //}

        Vector2 current = gameObject.transform.position;
        Vector2 newPosition = unit.GetFaceDirection().normalized * 0.2f + current;
        PetUtility.Coroutine(PetUtility.LinearMove(current, newPosition, 0.15f, gameObject.transform));
        yield return new WaitForSeconds(0.15f);
        PetUtility.Coroutine(PetUtility.LinearMove(newPosition, current, 0.15f, gameObject.transform));
        yield return new WaitForSeconds(0.15f);
        yield return null;
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
