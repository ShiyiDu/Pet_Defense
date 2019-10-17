using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActRollingAttack : StateMachineBehaviour
{
    private Vector2 start;
    private float stopTime = 0f; //rollingGap seconds away from stop time can the fish
    private Fish fish;
    private float initDirection;
    private float direction; //positive for right, negative for left
    private bool rolling = false;
    private HashSet<Ghost> passedEnemies;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Init(animator);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (fish.launchingAttack) {
            //checking hit
            RaycastHit2D hit2D;
            //if hit a wall, change direction
            hit2D = Cast("Wall");
            if (hit2D) {
                direction = -direction;
                PetUtility.FlipTransform(fish.transform);
                passedEnemies.Clear();
            }

            //if hit an enemy, attack enemy
            hit2D = Cast("Ghost");
            if (hit2D) {
                Ghost ghost = hit2D.collider.GetComponent<Ghost>();
                if (!passedEnemies.Contains(ghost)) {
                    ghost.TakeDamage(fish.damage);
                    passedEnemies.Add(ghost);
                }
            }

            //if comeback to origin position, stop launching attack and set the timer for next attack
            if (Mathf.Abs(fish.transform.position.x - start.x) <= fish.rollingSpeed / 29 && (PetUtility.GetSameFloorGhost(fish.transform.position) == null) && rolling) {
                fish.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                //Debug.Log("Back To Start!");
                rolling = false;
                void FinishAttack()
                {
                    fish.launchingAttack = false;
                    stopTime = Time.time;
                }
                PetUtility.WaitAndDo(fish.rollingGap, FinishAttack);
                return;
            } else if (rolling) {
                //setting velocity
                Vector2 velocity = fish.GetComponent<Rigidbody2D>().velocity;
                velocity.x = fish.rollingSpeed * direction;
                fish.GetComponent<Rigidbody2D>().velocity = velocity;
            }
        } else if (Time.time - fish.rollingGap >= stopTime) {
            //start Rolling
            fish.launchingAttack = true;
            rolling = true;
            Init(animator);
        }
    }

    private void Init(Animator animator)
    {
        fish = animator.GetComponent<Fish>();
        start = animator.transform.position;
        direction = (fish.enemy.transform.position - fish.transform.position).normalized.x;
        initDirection = direction;
        rolling = true;
        passedEnemies = new HashSet<Ghost>();
        fish.launchingAttack = true;
    }

    private RaycastHit2D Cast(string layer)
    {
        return Physics2D.Raycast(fish.transform.position, new Vector2(direction, 0), 0.5f, LayerMask.GetMask(layer));
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
