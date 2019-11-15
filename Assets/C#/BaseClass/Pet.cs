using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pet : UnitBehaviour, Selectable
{
    public bool enemyInHouse;
    public bool bedNearby;
    public bool rest;
    public bool upstairFirst = false;
    public bool checkUpstair = true;
    public bool checkDownstair = true;
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

    protected override void OnStart()
    {
        UpdataFaceDirection();
    }

    public virtual void UpdataFaceDirection()
    {
        facingDirection = PetUtility.GetFloorDirection(transform.position);
        facingDirection = -facingDirection;
        transform.rotation = Quaternion.Euler(0, facingDirection == Vector2.left ? 180 : 0, 0);
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
