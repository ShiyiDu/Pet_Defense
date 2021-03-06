﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    public float upBound = 3f;
    public float lowBound = 1f;
    public float leftBound = 3f;
    public float rightBound = 3f;
    public Vector2 offset = Vector2.zero;

    //public Sprite unoccupiedBed;
    //public Sprite occupiedBed;

    public GameObject yellowOutline; //the outline for the unoccupied bed

    private SpriteRenderer spriteRenderer;

    private GameObject pet;

    public void Initialize(GameObject pet)
    {
        this.pet = pet;
    }

    public bool RequestBed(Vector2 position)
    {
        //Debug.Log("requesting bed...");
        if (pet != null || !gameObject.activeSelf) return false;
        Vector2 difference = position - (Vector2)transform.position;

        if (difference.x > rightBound || difference.x < -leftBound)
            return false;

        if (difference.y > upBound || difference.y < -lowBound)
            return false;

        return true;
    }

    public Vector2 RequestPos()
    {
        return offset + (Vector2)transform.position;
    }

    public bool IsOccupied()
    {
        return pet != null;
    }

    void OnDrawGizmos()
    {
        if (pet != null) Gizmos.DrawLine(transform.position, pet.transform.position);
        Vector3 center = new Vector3();
        Vector3 size = new Vector3();
        center.x = (rightBound - leftBound) / 2 + transform.position.x;
        center.y = (upBound - lowBound) / 2 + transform.position.y;
        size.x = leftBound + rightBound;
        size.y = upBound + lowBound;

        Gizmos.DrawWireCube(center, size);

        Gizmos.color = Color.red;

        Gizmos.DrawSphere(offset + (Vector2)transform.position, 0.05f);
    }

    void ShowFreeBed()
    {
        if (!IsOccupied()) yellowOutline.SetActive(true);
    }

    void UnshowFreeBed()
    {
        yellowOutline.SetActive(false);
    }

    private void OnEnable()
    {
        EventManager.StartListening(GameEvent.selectBedStart, ShowFreeBed);
        EventManager.StartListening(GameEvent.selectBedFinish, UnshowFreeBed);
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //spriteRenderer.sprite = unoccupiedBed;
    }

    // Update is called once per frame
    void Update()
    {
        //if (IsOccupied() == true) {
        //    spriteRenderer.sprite = occupiedBed;
        //} else {
        //    spriteRenderer.sprite = unoccupiedBed;
        //}
    }

    private void OnDisable()
    {
        EventManager.StopListening(GameEvent.selectBedStart, ShowFreeBed);
        EventManager.StopListening(GameEvent.selectBedFinish, UnshowFreeBed);
    }
}
