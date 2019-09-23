using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

public class WhiteGhost : MonoBehaviour, Ghost
{
    public Vector2[] routePoints;
    private bool nearDoor = false;

    public void Kill()
    {

    }

    public int GetHealth()
    {

        return 0;
    }

    public void TakeDamage(int damage)
    {

    }

    //this is called when the ghost enterd a door
    public void DoorEntered(DoorControl door)
    {
        //just change a bool value maybe?
        nearDoor = true;
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
    }

    void PlayAnim()
    {
        //todo: play the animation based on the type of action.
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

    }
}
