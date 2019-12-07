using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class levelFin : MonoBehaviour
{

    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject dayTime;
    public GameObject nightTime;

    public GameObject toTown;
    public GameObject nextLvl;
    public GameObject retry;
    public GameObject resetButton;

    public int startLevel; //to fast test levels
    private static bool startLevelSet = false;
    public static int levelNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (!startLevelSet) {
            LevelReader.LoadLevel(startLevel);
            levelNumber = startLevel;
            startLevelSet = true;
        }

        nightTime.SetActive(true);
        dayTime.SetActive(false);

        //screens
        winScreen.SetActive(false);
        loseScreen.SetActive(false);

        //buttons
        toTown.gameObject.SetActive(false);
        nextLvl.gameObject.SetActive(false);
        retry.gameObject.SetActive(false);
        resetButton.SetActive(false);

    }

    void Update()
    {
        //keep the boolean up to date here
    }

    private void OnEnable()
    {
        EventManager.StartListening(GameEvent.playerWon, levelWon);
        EventManager.StartListening(GameEvent.playerLost, levelLost);
    }

    public void levelWon()
    {
        if (levelNumber < 6)
        {
            winScreen.SetActive(true);
            toTown.gameObject.SetActive(true);
            nextLvl.gameObject.SetActive(true);
            nightTime.SetActive(false);
            dayTime.SetActive(true);
            PetUtility.PauseGame();
        }
        else
        {
            //all levels are finished
            winScreen.SetActive(true);
            resetButton.SetActive(true);
            nightTime.SetActive(false);
            dayTime.SetActive(true);
            PetUtility.PauseGame();
        }
        
    }

    public void levelLost()
    {
        loseScreen.SetActive(true);
        toTown.gameObject.SetActive(true);
        retry.gameObject.SetActive(true);
        nightTime.SetActive(false);
        dayTime.SetActive(true);
        PetUtility.PauseGame();

    }

    public void goToTown()
    {
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        toTown.gameObject.SetActive(false);
        nextLvl.gameObject.SetActive(false);
        retry.gameObject.SetActive(false);
        Debug.Log("town");

        SceneManager.LoadScene(1);
    }

    public void nextLevel()
    {
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        toTown.gameObject.SetActive(false);
        nextLvl.gameObject.SetActive(false);
        retry.gameObject.SetActive(false);
        Debug.Log("nL");

        //update level data here
        levelNumber++;
        LevelReader.LoadLevel(levelNumber);
    }

    public void tryAgain()
    {
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        toTown.gameObject.SetActive(false);
        nextLvl.gameObject.SetActive(false);
        retry.gameObject.SetActive(false);
        Debug.Log("again");

        LevelReader.LoadLevel(levelNumber);
    }

    public void restartGame()
    {
        //reset level data
        levelNumber = 0;
        LevelReader.LoadLevel(levelNumber);
        SceneManager.LoadScene(0);
    }

    private void OnDisable()
    {
        EventManager.StopListening(GameEvent.playerLost, levelLost);
        EventManager.StopListening(GameEvent.playerWon, levelWon);
    }
}
