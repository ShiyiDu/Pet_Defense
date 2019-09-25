using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pommy : UnitStateMachine, Pet
{
    public float attackInterval = 2f;
    public int damage = 5;

    private GameObject enemy;
    private bool enemyEntered = false;
    private Direction enemyDirection = Direction.right;
    private float timer = 1f;

    protected override void attack()
    {
        timer -= Time.deltaTime;
        if (timer <= 0) {
            Debug.Log("ghost attacking");
            StartCoroutine(LaunchAttack());
            if (enemy != null) enemy.GetComponent<Ghost>().TakeDamage(damage);
            timer = attackInterval;
            if (!enemyEntered) state = UnitState.idle;
        }
    }

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

    protected override void die()
    {
    }

    protected override void EnterDoor()
    {
        //throw new System.NotImplementedException();
    }

    protected override void ExitDoor()
    {
        //throw new System.NotImplementedException();
    }

    protected override void idle()
    {
        if (enemyEntered) state = UnitState.attack;
    }

    protected override void walk()
    {
        //throw new System.NotImplementedException();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ghost")) {
            enemy = collision.gameObject;
            enemyEntered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ghost")) {
            enemy = null;
            enemyEntered = false;
        }
    }

}
