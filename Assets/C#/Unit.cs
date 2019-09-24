using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this is the interface for both ghosts and pets
public interface Unit
{
    //return the current health remaining
    int GetHealth();

    UnitState GetState();

    void TakeDamage(int damage);

    //kill the ghost
    void Kill();

    void DoorEntered(DoorControl door);

    void DoorExited(DoorControl door);
}
