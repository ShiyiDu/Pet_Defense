using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pet : UnitBehaviour, Selectable
{
    //I don't know if we can still get this one by string if its static
    public bool enemyInHouse;
    public bool bedNearby;
    public bool rest;
    public float healthRegeneration; //how fast you regenerate health on your rest?
    protected Bed myBed;

    //check if this one is in sleep or not
    public bool IsSleep()
    {
        return rest;
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
        rest = !rest;
        //if (sleep) destination = myBed.transform.position;
        //you update the current route
    }

    public void Unselected()
    {
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Ghost")) {
    //        enemy = collision.gameObject;
    //        enemyEntered = true;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Ghost")) {
    //        enemy = null;
    //        enemyEntered = false;
    //    }
    //}
}
