using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;
using UnityEngine.Events;

public class WhiteGhost : Ghost
{
    private Vector3 originScale = new Vector3();

    //protected override void EnterDoor()
    //{
    //    Debug.Log("trying to enter door");
    //    //you want play the animation, wait, teleport and change state
    //    if (!entering) {
    //        Debug.Log("trying to enter door");
    //        entering = true;
    //        UnityAction exitDoor = delegate
    //        {
    //            transform.position = door.OtherEndPos();
    //            state = UnitState.exitDoor;
    //            entering = false;
    //        };
    //        StartCoroutine(PetUtility.LinearScaleFade(originScale, Vector3.zero, enterDoorTime, transform));
    //        PetUtility.WaitAndDo(enterDoorTime, exitDoor);
    //    }
    //}

    //protected override void ExitDoor()
    //{
    //    if (!exiting) {
    //        exiting = true;
    //        nextPoint++;
    //        UnityAction startWalking = delegate
    //        {
    //            state = UnitState.walk;
    //            exiting = false;
    //        };
    //        StartCoroutine(PetUtility.LinearScaleFade(Vector3.zero, originScale, exitDoorTime, transform));
    //        PetUtility.WaitAndDo(exitDoorTime, startWalking);
    //    }
    //}

    //protected override void Attack()
    //{
    //    timer -= Time.deltaTime;
    //    if (timer <= 0) {
    //        Debug.Log("ghost attacking");
    //        StartCoroutine(LaunchAttack());
    //        if (enemy != null) enemy.GetComponent<Pet>().TakeDamage(damage);
    //        timer = attackInterval;
    //        if (!enemyEntered) state = UnitState.walk;
    //    }
    //    //what does attack look like?
    //    //move to the direction and comback
    //}

    IEnumerator LaunchAttack()
    {
        if (enemy.transform.position.x - transform.position.x > 0) {
            enemyDirection = Direction.right;
        } else {
            enemyDirection = Direction.left;
        }

        Vector2 current = transform.position;
        Vector2 newPosition = enemyDirection == Direction.left ? current + Vector2.left * 0.2f : current + Vector2.right * 0.2f;
        StartCoroutine(PetUtility.LinearMove(current, newPosition, 0.15f, transform));
        yield return new WaitForSeconds(0.15f);
        StartCoroutine(PetUtility.LinearMove(newPosition, current, 0.15f, transform));
        yield return new WaitForSeconds(0.15f);
        yield return null;
    }

    void OnDrawGizmos()
    {
        //if (routePoints.Length == 0) return;
        //Gizmos.color = Color.red;
        //Gizmos.DrawLine(transform.position, routePoints[0]);
        //for (int i = 0; i < routePoints.Length - 1; i++) {
        //    Gizmos.DrawLine(routePoints[i], routePoints[i + 1]);
        //}
    }

    protected override void OnStart()
    {
        originScale = transform.localScale;
        state = UnitState.walk;
        timer = attackInterval;
    }

}
