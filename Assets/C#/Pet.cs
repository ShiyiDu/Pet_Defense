using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Pet
{
    //kill the pet
    void Kill();

    //return the current health remaining
    int GetHealth();

    void TakeDamage(int damage);
}
