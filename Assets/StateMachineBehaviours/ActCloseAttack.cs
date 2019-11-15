using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActCloseAttack : StateMachineBehaviour
{
    public bool attackImmediately = false;
    public float attackDistance = 0.2f;

    GameObject gameObject;
    Rigidbody2D rigid;
    float lastAttack = 0f;
    UnitBehaviour unit;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        gameObject = animator.gameObject;
        rigid = gameObject.GetComponent<Rigidbody2D>();
        unit = gameObject.GetComponent<UnitBehaviour>();

        if (!attackImmediately) {
            lastAttack = Time.time;
        } else {
            unit.launchingAttack = true;
            CheckAttack();
        }
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CheckAttack();
    }

    protected virtual void Attack()
    {
        unit.launchingAttack = true;
        PetUtility.Coroutine(LaunchAttack());
        //Debug.Log("try get enemy");
        UnitBehaviour enemy = unit.enemy.GetComponent<UnitBehaviour>();
        //Debug.Log(enemy != null);
        int damage = unit.damage;
        enemy.TakeDamage(damage);
    }

    protected virtual void CheckAttack()
    {
        rigid.velocity = Vector2.zero;
        if (Time.time - lastAttack >= unit.attackInterval) {

            //Debug.Log("ghost attacking");
            if (unit.enemy != null && unit.enemyInRange) {
                Attack();
                lastAttack = Time.time;
            } else {
                unit.launchingAttack = false;
            }
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
        unit.launchingAttack = true;
        Vector2 current = gameObject.transform.position;
        //todo: get direction needs to be updated when chasing from behind
        Vector2 newPosition = unit.GetFaceDirection().normalized * attackDistance + current;
        if (gameObject != null) PetUtility.Coroutine(PetUtility.LinearMove(current, newPosition, 0.15f, gameObject.transform));
        yield return new WaitForSeconds(0.15f);
        if (gameObject != null) PetUtility.Coroutine(PetUtility.LinearMove(newPosition, current, 0.15f, gameObject.transform));
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
