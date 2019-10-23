using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundDeath : MonoBehaviour
{
    public AudioClip ghostDeath;
    public AudioClip petDeath;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        EventManager.StartListening(ParameterizedGameEvent.unitDead, deathSound);
    }

   private void deathSound(object rip)
    {
      if ( ((UnitBehaviour)rip).CompareTag("Ghost") == true){
            audioSource.PlayOneShot(ghostDeath);
        }

        else if (((UnitBehaviour)rip).CompareTag("Pet") == true)
        {
            audioSource.PlayOneShot(petDeath);
        }

    }


    private void OnDisable()
    {
        EventManager.StopListening(ParameterizedGameEvent.unitDead, deathSound);
    }

}
