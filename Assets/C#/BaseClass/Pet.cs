using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pet : UnitBehaviour, Selectable
{
    protected bool sleep;
    protected Bed myBed;

    //check if this one is in sleep or not
    public bool IsSleep()
    {
        return sleep;
    }

    public void OfferBed(Bed bed)
    {
        this.myBed = bed;
    }

    public Bed GetBed()
    {
        return myBed;
    }

    public void Selected()
    {
        sleep = !sleep;
        if (sleep) destination = myBed.transform.position;
        //you update the current route
    }

    public void Unselected()
    {
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
