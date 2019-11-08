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

    public int levelCounter;
    public LevelReader levelReader;
    public List<PetLevel.SpawnInfo[]> levelDat = new List<PetLevel.SpawnInfo[]>(); //a list of the spawnInfos lists (this needs to be editable in the inspecter)

    public bool playerWin;


    // Start is called before the first frame update
    void Start()
    {
        levelCounter = 1; //current level player is playing
        playerWin = false;

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

        sceneChanges.changeScene(2);
    }

    public void nextLevel() //DOUBLE CHECK THIS
    {
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        toTown.gameObject.SetActive(false);
        nextLvl.gameObject.SetActive(false);
        retry.gameObject.SetActive(false);

        levelCounter ++;

        //this may be wrong, but I couldn't find a way to change the levelData variable in LevelReader without making it static
        //Basically, I wont to change Utilities > levelReader > Level Data to the spawn list fo rthe next level
        LevelData lev = FindObjectOfType<LevelData>();

        lev.spawnInfos = levelDat[levelCounter]; //updates the spawn info list to the next level



        //change LevelData to next one (maybe have a coutner) (it's inside the utilities object)
        //update time variable in checkIfFinished class
    }

            public void tryAgain()
            {
                winScreen.SetActive(false);
                loseScreen.SetActive(false);
                toTown.gameObject.SetActive(false);
                nextLvl.gameObject.SetActive(false);
                retry.gameObject.SetActive(false);

                //restart level (resets timer)
            }

    private void OnDisable()
    {
        EventManager.StopListening(GameEvent.levelFinished, popUp);
        EventManager.StopListening(GameEvent.playerWon, popUp);
    }
}
