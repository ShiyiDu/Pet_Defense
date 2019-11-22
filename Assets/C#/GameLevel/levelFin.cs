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

    // Start is called before the first frame update
    void Start()
    {
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
    }

    public void levelWon()
    {
        winScreen.SetActive(true);
        toTown.gameObject.SetActive(true);
        nextLvl.gameObject.SetActive(true);
        nightTime.SetActive(false);
        dayTime.SetActive(true);
        PetUtility.PauseGame();
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

        SceneManager.LoadScene(1);
    }

    public void nextLevel() //DOUBLE CHECK THIS
    {
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        toTown.gameObject.SetActive(false);
        nextLvl.gameObject.SetActive(false);
        retry.gameObject.SetActive(false);

        //**update level data here (only line that needs to be added, I updated the other things)
        SceneManager.LoadScene(3);
    }

    public void tryAgain()
    {
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        toTown.gameObject.SetActive(false);
        nextLvl.gameObject.SetActive(false);
        retry.gameObject.SetActive(false);

        SceneManager.LoadScene(3);
    }

    private void OnDisable()
    {
        EventManager.StopListening(GameEvent.playerLost, levelLost);
        EventManager.StopListening(GameEvent.playerWon, levelWon);
    }
}
