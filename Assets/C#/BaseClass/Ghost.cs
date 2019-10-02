using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ghost : UnitBehaviour
{
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
}