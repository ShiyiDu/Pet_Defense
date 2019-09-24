using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

public class WhiteGhost : MonoBehaviour, Ghost
{
    public float velocity = 1f;
    public int health = 100;

    //how close is considered "at the point"
    public float tolerance = 0.05f;
    //the white ghost patroling through all these points
    public Vector2[] routePoints;

    private int nextPoint = 0;
    private bool nearDoor = false;
    private DoorControl door = null;
    private GhostStatus status = GhostStatus.idle;

    public GhostStatus GetStatus()
    {
        return status;
    }

    public int GetHealth()
    {

        return 0;
    }

    public void TakeDamage(int damage)
    {

    }

    public void Kill()
    {

    }

    //this is called when the ghost enterd a door
    public void DoorEntered(DoorControl door)
    {
        //just change a bool value maybe?
        nearDoor = true;
        this.door = door;
        //play the animation
        //PlayAnim();
        //wait a few second
        //change the postion

        //play the animation
        //start walking again
    }

    public void DoorExited(DoorControl door)
    {
        nearDoor = false;
        if (this.door = door) this.door = null;
    }

    void PlayAnim()
    {
        //todo: play the animation based on the type of action.
    }

    void Patrol()
    {
        //control the patroling of the ghost
    }

    //check if the current position is close enough to next point
    bool RangeCheck()
    {
        //you only need to compare x-axies
        return
            Mathf.Abs(transform.position.x - routePoints[nextPoint].x) <= tolerance;
    }

    //draw all the 
    void OnDrawGizmos()
    {
        if (routePoints.Length == 0) return;
        Gizmos.DrawLine(transform.position, routePoints[0]);
        for (int i = 0; i < routePoints.Length - 1; i++) {
            Gizmos.DrawLine(routePoints[i], routePoints[i + 1]);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }
}
