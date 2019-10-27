using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{

    public GameObject FadePanel;
    public GameObject pauseCentreText;
    public Toggle toggle;
    public Button menuB;

    public Sprite pauseImage;
    public Sprite playImage;
    public Image background;
    

    private bool btnLastState = false; //the last state of the toggle, so we don't repeatly continue the game


    public void Update()
    {
        if (toggle.isOn == true) {

            background.sprite = pauseImage;
            
            pauseCentreText.SetActive(false);
            FadePanel.SetActive(false);

            //resume all game content (ghosts, pets) (same as a method in the MenuPopup script)
            if (!btnLastState) PetUtility.ContinueGame();
            btnLastState = true;
            menuB.enabled = true;
        }


        if (toggle.isOn == false) {

            background.sprite = playImage;

            FadePanel.SetActive(true);
            pauseCentreText.SetActive(true);
 
            //pause all game content (ghosts, pets) (same as a method in the MenuPopup script)
            PetUtility.PauseGame();
            if (btnLastState) PetUtility.PauseGame();
            btnLastState = false;
            menuB.enabled = false;
        }
    }


}
