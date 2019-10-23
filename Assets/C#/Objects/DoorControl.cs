using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;
using UnityEngine.Events;

//this script should tell the ghost/pet that they entered
//the center of the object
//and be able to tell them where is the other end of the
//door.
public class DoorControl : MonoBehaviour
{
    public DoorControl targetDoor;
    private bool doorTaken = false;
    private int token = 0;
    //public bool onWayDoor = false;
    //public bool woodDoor = false;
    //public bool stairs = false;
    //private bool playerNearby = false;
    //private bool controlEnabled = true;
    //private static GameObject player;
    //private GameObject currentScene;

    //returns the door on the other end
    public DoorControl OtherEnd()
    {
        return targetDoor;
    }

    //returns the postion of the other end of the door
    public Vector2 OtherEndPos()
    {
        return (Vector2)targetDoor.transform.position;
    }

    ////return true if you can access the door
    public int AcquireAccess()
    {
        if (!doorTaken && !targetDoor.doorTaken) {
            doorTaken = true;
            targetDoor.doorTaken = true;
            token = Random.Range(1, 20);
            targetDoor.token = token;
            return token;
        }
        return 0;
    }

    ////return false if you can't access the door
    public void ReleaseAccess(int token)
    {
        if (token == this.token) {
            doorTaken = false;
            targetDoor.doorTaken = false;

            targetDoor.token = 0;
            this.token = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        if (targetDoor) {
            if (!targetDoor.targetDoor) targetDoor.targetDoor = this;
            else Gizmos.DrawLine(transform.position, targetDoor.transform.position);
        }
        //transform.position = new Vector3(transform.position.x, transform.position.y, 0);

    }

    void OnEnable()
    {
        //EventManager.StartListening(GameEvent.controlEnable, EnableControl);
        //EventManager.StartListening(GameEvent.controlDisable, DisableControl);
        //currentScene = transform.parent.gameObject;
    }
    // Use this for initialization
    void Start()
    {
        if (targetDoor) {
            if (!targetDoor.targetDoor) targetDoor.targetDoor = this;
            //else Gizmos.DrawLine(transform.position, targetDoor.transform.position);
        }

    }

    // Update is called once per frame
    void Update()
    {
        //this was used to control the animation being played
        //if (Input.GetButtonDown("Interact") && playerNearby && targetDoor && controlEnabled) {
        //if (woodDoor) {
        //    TransitionManager.StartTransition(TransitionManager.DoorType.WoodDoor, 0.75f);
        //} else if (stairs) {
        //    TransitionManager.StartTransition(TransitionManager.DoorType.Stair, 0.75f);
        //} else {
        //    TransitionManager.StartTransition(TransitionManager.DoorType.MetalDoor, 0.75f);
        //}
        //    StartCoroutine(Wait(0.1f, DoorInteract));
        //}
    }

    //void DisableControl()
    //{
    //    controlEnabled = false;
    //    //Debug.Log("Control Disabled for the door");
    //}

    //void EnableControl()
    //{
    //    controlEnabled = true;
    //    //Debug.Log("Control Enabled for the door");
    //}

    //IEnumerator Wait(float time, UnityAction method)
    //{
    //    yield return new WaitForSeconds(time);
    //    method.Invoke();
    //}

    //when the player push the interact button, this got called
    //private void DoorInteract()
    //{
    //    //the player have pushed the interact button near the door
    //    targetDoor.DoorOpen(player);
    //    PlayerExited();
    //    transform.parent.gameObject.SetActive(false);
    //    targetDoor.transform.parent.gameObject.SetActive(true);
    //}

    //public void DoorOpen(GameObject player)
    //{
    //    //make a sound, pause 0.5s, move the player position, etc.
    //    //Debug.Log("teleported");
    //    PlayerEntered();
    //    player.transform.position = transform.position;
    //}

    //
    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Ghost")) {
    //        other.GetComponent<Ghost>().DoorEntered(this);
    //        Debug.Log("Ghost entered");
    //    }
    //}

    //void PlayerEntered()
    //{
    //    playerNearby = true;
    //    //todo: activate the hint
    //    //Debug.Log("player entered");
    //    //EventManager.StartListening(GameEvent.interact, DoorInteract);
    //}

    //void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.CompareTag("Ghost")) {
    //        other.GetComponent<Ghost>().DoorExited(this);
    //        Debug.Log("Ghost exited");
    //    }
    //}

    //void PlayerExited()
    //{
    //    playerNearby = false;
    //    //todo: deactivate the hint
    //    //Debug.Log("player exited");
    //    //EventManager.StopListening(GameEvent.interact, DoorInteract);
    //}

    //void OnDisable()
    //{
    //    EventManager.StopListening(GameEvent.controlDisable, DisableControl);
    //    EventManager.StopListening(GameEvent.controlEnable, EnableControl);
    //    //EventManager.StopListening(GameEvent.interact, DoorInteract);
    //}
}
