﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pommy : Pet
{
    protected override void OnStart()
    {
        GetFaceDirection();
    }

    public override Vector2 GetFaceDirection()
    {
        facingDirection = PetUtility.GetFloorDirection(transform.position);
        facingDirection = -facingDirection;
        transform.rotation = Quaternion.Euler(0, facingDirection == Vector2.left ? 180 : 0, 0);
        return facingDirection;
    }

    //protected override void Attack()
    //{
    //    timer -= Time.deltaTime;
    //    if (timer <= 0) {
    //        Debug.Log("ghost attacking");
    //        StartCoroutine(LaunchAttack());
    //        if (enemy != null) enemy.GetComponent<Ghost>().TakeDamage(damage);
    //        timer = attackInterval;
    //        if (!enemyEntered) state = UnitState.idle;
    //    }
    //}

    //IEnumerator LaunchAttack()
    //{
    //    if (enemy.transform.position.x - transform.position.x > 0) {
    //        enemyDirection = Direction.right;
    //    } else {
    //        enemyDirection = Direction.left;
    //    }

    //    Vector2 current = transform.position;
    //    Vector2 newPosition = enemyDirection == Direction.left ? current + Vector2.left * 0.2f : current + Vector2.right * 0.2f;
    //    StartCoroutine(PetUtility.LinearMove(current, newPosition, 0.15f, transform));
    //    yield return new WaitForSeconds(0.15f);
    //    StartCoroutine(PetUtility.LinearMove(newPosition, current, 0.15f, transform));
    //    yield return new WaitForSeconds(0.15f);
    //    yield return null;
    //}

}
