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
    public Button menuB;

    private bool btnLastState = false; //the last state of the toggle, so we don't repeatly continue the game

    public void Update()
    {
        if (btn.isOn == true) {
            pause.SetActive(false);
            playText.SetActive(false);
            btnText.text = "Pause";
            FadePanel.SetActive(false);
            //resume all game content (ghosts, pets) (same as a method in the MenuPopup script)
            if (!btnLastState) PetUtility.ContinueGame();
            btnLastState = true;
            menuB.enabled = true;
        }


        if (btn.isOn == false) {

            FadePanel.SetActive(true);
            pause.SetActive(true);
            playText.SetActive(true);
            btnText.text = "Play";
            //pause all game content (ghosts, pets) (same as a method in the MenuPopup script)
            PetUtility.PauseGame();
            if (btnLastState) PetUtility.PauseGame();
            btnLastState = false;
            menuB.enabled = false;
        }
    }


}
