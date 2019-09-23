using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class DoorControl : MonoBehaviour
{
    public DoorControl targetDoor;
    public bool onWayDoor = false;
    public bool woodDoor = false;
    public bool stairs = false;
    private bool playerNearby = false;
    private bool controlEnabled = true;
    private static GameObject player;
    //private GameObject currentScene;

    private void OnDrawGizmos()
    {
        if (targetDoor) {
            if (!targetDoor.targetDoor && !onWayDoor) targetDoor.targetDoor = this;
            else Gizmos.DrawLine(transform.position, targetDoor.transform.position);
        }
        //transform.position = new Vector3(transform.position.x, transform.position.y, 0);

    }

    void OnEnable()
    {
        EventManager.StartListening(GameEvent.controlEnable, EnableControl);
        EventManager.StartListening(GameEvent.controlDisable, DisableControl);
        //currentScene = transform.parent.gameObject;
    }
    // Use this for initialization
    void Start()
    {
        if (targetDoor) {
            if (!targetDoor.targetDoor && !onWayDoor) targetDoor.targetDoor = this;
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

    void DisableControl()
    {
        controlEnabled = false;
        //Debug.Log("Control Disabled for the door");
    }

    void EnableControl()
    {
        controlEnabled = true;
        //Debug.Log("Control Enabled for the door");
    }

    IEnumerator Wait(float time, UnityAction method)
    {
        yield return new WaitForSeconds(time);
        method.Invoke();
    }

    //when the player push the interact button, this got called
    //private void DoorInteract()
    //{
    //    //the player have pushed the interact button near the door
    //    targetDoor.DoorOpen(player);
    //    PlayerExited();
    //    transform.parent.gameObject.SetActive(false);
    //    targetDoor.transform.parent.gameObject.SetActive(true);
    //}

    public void DoorOpen(GameObject player)
    {
        //make a sound, pause 0.5s, move the player position, etc.
        //Debug.Log("teleported");
        PlayerEntered();
        player.transform.position = transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            player = other.gameObject;
            PlayerEntered();
        }
    }

    void PlayerEntered()
    {
        playerNearby = true;
        //todo: activate the hint
        //Debug.Log("player entered");
        //EventManager.StartListening(GameEvent.interact, DoorInteract);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && playerNearby) {
            playerNearby = false;
            //todo: deactivate the hint
            //Debug.Log("player exited");
        }
    }

    void PlayerExited()
    {
        playerNearby = false;
        //todo: deactivate the hint
        //Debug.Log("player exited");
        //EventManager.StopListening(GameEvent.interact, DoorInteract);
    }

    void OnDisable()
    {
        EventManager.StopListening(GameEvent.controlDisable, DisableControl);
        EventManager.StopListening(GameEvent.controlEnable, EnableControl);
        //EventManager.StopListening(GameEvent.interact, DoorInteract);
    }
}
