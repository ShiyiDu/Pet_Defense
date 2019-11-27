using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class disableMusic : MonoBehaviour
{

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "LevelScene" || SceneManager.GetActiveScene().name == "MainMenu")
        {
            Destroy(GameObject.Find("townMusic"));
        }
      
    }

}
