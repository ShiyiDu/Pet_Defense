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



    //ghost dies
    
     //   audioSource.PlayOneShot(ghostDeath);
    

    //pet dies
    
      //  audioSource.PlayOneShot(petDeath);
    
}
