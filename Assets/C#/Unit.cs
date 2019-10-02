using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this is the interface for both ghosts and pets
public interface Unit
{
    //return the current health remaining
    int GetHealth();

    int GetMaxHealth();

    UnitState GetState();

    void TakeDamage(int damage);

    //kill the ghost
    void Kill();

    //if this unit is near a door, this get called
    void DoorEntered(DoorControl door);

    //if this unit just exited a door, this get called
    void DoorExited(DoorControl door);
}
