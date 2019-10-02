using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hamster : Pet
{
    public GameObject bullet;

    public float bulletVelocity = 5f;

    GameObject createBullet()
    {
        return Instantiate(bullet, transform.position, Quaternion.identity);
    }

    protected override void Attack()
    {
        timer -= Time.deltaTime;
        if (timer < 0) {
            if (enemy.transform.position.x - transform.position.x > 0) {
                enemyDirection = Direction.right;
            } else {
                enemyDirection = Direction.left;
            }
            GameObject newBullet = createBullet();
            newBullet.GetComponent<HamBullet>().Initialize(damage, bulletVelocity, enemyDirection == Direction.right);

            timer = attackInterval;
        }

        if (!enemyEntered) state = UnitState.idle;
    }

    protected override void Die()
    {
        throw new System.NotImplementedException();
    }

    protected override void EnterDoor()
    {
        //throw new System.NotImplementedException();
    }

    protected override void ExitDoor()
    {
        //throw new System.NotImplementedException();
    }

    protected override void Idle()
    {
        if (enemyEntered) state = UnitState.attack;
    }

    protected override void Walk()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnStart()
    {
        timer = attackInterval;
    }
}
