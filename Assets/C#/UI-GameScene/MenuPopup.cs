using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPopup : MonoBehaviour
{

    public GameObject FadePanel;
    public GameObject Menu;
    public GameObject SettingsPanel;

    public void OpenMenu()
    {

        FadePanel.SetActive(true);
        Menu.SetActive(true);

        //pause all game content (ghosts, pets)
        PetUtility.PauseGame();

    }

    //Exit to Town Scene
    public void ExitLevel()
    {
        SceneManager.LoadScene(2);
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

        //game resumes, same as method in the PauseGame script
        PetUtility.ContinueGame();

    }

}
