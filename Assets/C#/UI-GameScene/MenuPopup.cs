using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPopup : MonoBehaviour
{

    public GameObject FadePanel;
    public GameObject Menu;
    public GameObject SettingsPanel;
    public Toggle pauseB;

    public void OpenMenu()
    {
        FadePanel.SetActive(true);
        Menu.SetActive(true);

        //pause all game content (ghosts, pets)
        PetUtility.PauseGame();
        pauseB.enabled = false;
    }

    //Exit to Town Scene
    public void ExitLevel()
    {
        SceneManager.LoadScene(1);
        //Since level isn't finished, it would automatically reset so player can start over
    }

    public void OpenSettings()
    {
        Menu.SetActive(false);
        SettingsPanel.SetActive(true);
    }

    public void BackToGame()
    {
        Menu.SetActive(false);
        FadePanel.SetActive(false);
        pauseB.enabled = true;
        //game resumes, same as method in the PauseGame script
        PetUtility.ContinueGame();
    }

}
