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
    public Text btnText;


    public void Update()
    {
        if (btn.isOn == true)
        {
            pause.SetActive(false);
            playText.SetActive(false);
            btnText.text = "Pause";
            FadePanel.SetActive(false);
            //resume all game content (ghosts, pets) (same as a method in the MenuPopup script)
        }


        if (btn.isOn == false)
        {
           
            FadePanel.SetActive(true);
            pause.SetActive(true);
            playText.SetActive(true);
            btnText.text = "Play";
            //pause all game content (ghosts, pets) (same as a method in the MenuPopup script)
        }
    }


}
