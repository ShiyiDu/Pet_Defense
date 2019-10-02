using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{

    public GameObject FadePanel;
    public GameObject pause;
    public Toggle btn;
    public GameObject playText;


    public void Update()
    {
        if (btn.isOn == true)
        {
        pause.SetActive(false);
            playText.SetActive(false);
            //resume all game content (ghosts, pets) (same as a method in the MenuPopup script)
        }


        if (btn.isOn == false)
        {
           
            FadePanel.SetActive(true);
            pause.SetActive(true);
            playText.SetActive(true);
            //pause all game content (ghosts, pets) (same as a method in the MenuPopup script)
        }
    }


    public void Unpause()
    {
        FadePanel.SetActive(false);
    }


}
