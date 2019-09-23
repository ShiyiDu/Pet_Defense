using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Ghost
{
    //kill the ghost
    void Kill();

    //return the current health remaining
    int GetHealth();

    void TakeDamage(int damage);

    void DoorEntered(DoorControl door);

    void DoorExited(DoorControl door);
}
