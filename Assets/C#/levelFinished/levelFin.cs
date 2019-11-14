using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class levelFin : MonoBehaviour
{

    public GameObject winScreen;
    public GameObject loseScreen;

    public Button toTown;
    public Button nextLvl;
    public Button retry;

    public LevelReader levelReader;
    public static int sceneNumber;

    public bool playerWin;

    // Start is called before the first frame update
    void Start()
    {
        playerWin = false;
        sceneNumber = 3;

        //screens
        winScreen.SetActive(false);
        loseScreen.SetActive(false);

        //buttons
        toTown.gameObject.SetActive(false);
        nextLvl.gameObject.SetActive(false);
        retry.gameObject.SetActive(false);
    }

    void Update()
    {
        //keep the boolean up to date here
    }

    private void OnEnable()
    {
        EventManager.StartListening(GameEvent.playerWon, levelWon);
        EventManager.StartListening(GameEvent.levelFinished, popUp);  
    }

    public void levelWon(){
        playerWin = true;
    }

    public void popUp()
    {
        if (playerWin == true)
        {
            winScreen.SetActive(true);
            toTown.gameObject.SetActive(true);
            nextLvl.gameObject.SetActive(true);
        }
        else
        {
            loseScreen.SetActive(true);
            toTown.gameObject.SetActive(true);
            retry.gameObject.SetActive(true);
        }
    }

    public void goToTown()
    {
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        toTown.gameObject.SetActive(false);
        nextLvl.gameObject.SetActive(false);
        retry.gameObject.SetActive(false);

        sceneChanges.changeScene(1);
    }

    public void nextLevel() //DOUBLE CHECK THIS
    {
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        toTown.gameObject.SetActive(false);
        nextLvl.gameObject.SetActive(false);
        retry.gameObject.SetActive(false);

        sceneNumber ++;
        sceneChanges.changeScene(sceneNumber);
    }

    public void tryAgain()
    {
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        toTown.gameObject.SetActive(false);
        nextLvl.gameObject.SetActive(false);
        retry.gameObject.SetActive(false);

        sceneChanges.changeScene(sceneNumber); //relaods current scene
    }

    private void OnDisable()
    {
        EventManager.StopListening(GameEvent.levelFinished, popUp);
        EventManager.StopListening(GameEvent.playerWon, popUp);
    }
}
