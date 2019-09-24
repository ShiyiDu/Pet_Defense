using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Pet
{
    //kill the pet
    void Kill();

    //returns the current state of the pet
    PetState GetState();

    //return the current health remaining
    int GetHealth();

    void TakeDamage(int damage);
}
