using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Ghost
{
    //return the current health remaining
    int GetHealth();

    GhostStatus GetStatus();

    void TakeDamage(int damage);

    //kill the ghost
    void Kill();

    void DoorEntered(DoorControl door);

    void DoorExited(DoorControl door);
}