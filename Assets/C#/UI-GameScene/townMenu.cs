using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class townMenu : MonoBehaviour
{
    public GameObject Menu;

    public Toggle toggle; 
    public Text toggleText;
    public AudioListener listener;


    public void Update()
    {
        if (toggle.isOn == true)
        {
            toggleText.text = "Sound On";
            listener.enabled = true;
        }


        if (toggle.isOn == false)
        {
            toggleText.text = "Sound Off";
            listener.enabled = false;
        }
    }


    public void OpenMenu()
    {
        Menu.SetActive(true);

        //pause all game content (ghosts, pets)
        PetUtility.PauseGame();
    }

    public void BackToGame()
    {
        Menu.SetActive(false);
        //game resumes, same as method in the PauseGame script
        PetUtility.ContinueGame();
    }
}
