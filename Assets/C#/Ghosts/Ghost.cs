using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ghost : UnitStateMachine
{
    public Vector2[] routePoints;

    //patroling
    protected int nextPoint = 0;
    protected bool entering = false;//entering the door
    protected bool exiting = false;//exiting the door

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pet")) {
            enemy = collision.gameObject;
            enemyEntered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Pet")) {
            enemy = null;
            enemyEntered = false;
        }
    }

    //check if the current position is close enough to next point
    protected bool RouteRangeCheck()
    {
        //you only need to compare x-axies
        return
            Mathf.Abs(transform.position.x - routePoints[nextPoint].x) <= tolerance;
    }
}