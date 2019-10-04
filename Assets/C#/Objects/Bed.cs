using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    public float upBound = 3f;
    public float lowBound = 1f;
    public float leftBound = 3f;
    public float rightBound = 3f;
    public Vector2 offset = Vector2.zero;

    private GameObject pet;

    public void Initialize(GameObject pet)
    {
        this.pet = pet;
    }

    public bool RequestBed(Vector2 position)
    {
        //Debug.Log("requesting bed...");
        if (pet != null) return false;
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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
