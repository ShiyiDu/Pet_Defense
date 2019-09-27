using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pet : UnitStateMachine
{
    protected Bed myBed;
    public void OfferBed(Bed bed)
    {
        this.myBed = bed;
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
