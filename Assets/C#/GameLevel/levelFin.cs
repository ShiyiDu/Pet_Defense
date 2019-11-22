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

    public static int levelNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        //sceneNumber = 0;
        DontDestroyOnLoad(this.gameObject);

        nightTime.SetActive(true);
        dayTime.SetActive(false);

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

    public void levelWon()
    {
        winScreen.SetActive(true);
        toTown.gameObject.SetActive(true);
        nextLvl.gameObject.SetActive(true);
        nightTime.SetActive(false);
        dayTime.SetActive(true);

        Pet[] pets = FindObjectsOfType<Pet>();
        for (int i = 0; i <= pets.Length - 1; i++) {
            Destroy(pets[i].gameObject);
        }
    }

    public void levelLost()
    {
        loseScreen.SetActive(true);
        toTown.gameObject.SetActive(true);
        retry.gameObject.SetActive(true);
        nightTime.SetActive(false);
        dayTime.SetActive(true);

        Ghost[] ghosts = FindObjectsOfType<Ghost>();
        for (int i = 0; i <= ghosts.Length - 1; i++) {
            Destroy(ghosts[i].gameObject);
        }
    }

    public void goToTown()
    {
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        toTown.gameObject.SetActive(false);
        nextLvl.gameObject.SetActive(false);
        retry.gameObject.SetActive(false);

        SceneManager.LoadScene(1);
    }

    public void nextLevel() //DOUBLE CHECK THIS
    {
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        toTown.gameObject.SetActive(false);
        nextLvl.gameObject.SetActive(false);
        retry.gameObject.SetActive(false);

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

        SceneManager.LoadScene(levelNumber); //relaods current scene
    }

    private void OnDisable()
    {
        EventManager.StopListening(GameEvent.playerLost, levelLost);
        EventManager.StopListening(GameEvent.playerWon, levelWon);
    }
}
