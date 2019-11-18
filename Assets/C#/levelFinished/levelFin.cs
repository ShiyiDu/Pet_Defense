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

    public GameObject toTown;
    public GameObject nextLvl;
    public GameObject retry;

    public static int sceneNumber;

    // Start is called before the first frame update
    void Start()
    {
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
        EventManager.StartListening(GameEvent.playerLost, levelLost);
        Debug.Log("start listening for levelWon");
        Debug.Log("start listening for popUp");
    }

    public void levelWon(){
        winScreen.SetActive(true);
        toTown.gameObject.SetActive(true);
        nextLvl.gameObject.SetActive(true);

        
        Pet[] pets = FindObjectsOfType<Pet>();
        for(int i = 0; i <= pets.Length -1; i++)
        {
            Destroy(pets[i]);
        }
    }

    public void levelLost()
    {
        loseScreen.SetActive(true);
        toTown.gameObject.SetActive(true);
        retry.gameObject.SetActive(true);

        Ghost[] ghosts = FindObjectsOfType<Ghost>();
        for (int i = 0; i <= ghosts.Length - 1; i++)
        {
            Destroy(ghosts[i]);
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
        EventManager.StopListening(GameEvent.playerLost, levelLost);
        EventManager.StopListening(GameEvent.playerWon, levelWon);
    }
}
