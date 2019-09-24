using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hamster : MonoBehaviour, Pet
{


    public void Kill()
    {

    }

    public PetState GetState()
    {
        return PetState.idle;
    }

    public int GetHealth()
    {
        return 0;
    }

    public void TakeDamage(int damage)
    {

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
