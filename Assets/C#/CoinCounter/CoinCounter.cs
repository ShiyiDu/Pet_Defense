using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{
    public Text coinDisplay;
    private int counter;

    private void OnEnable()
    {
        EventManager.StartListening(GameEvent.levelFinished, updateScore); //when level is completed, add 100 to score
    }

    void Start()
    {
        counter = 0;
        coinDisplay.text = "";
        coinDisplay.text = counter.ToString();
        //EventManager.StartListening(EventManager.levelFinished, updateScore()); //when level is completed, add 100 to score
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD

=======
>>>>>>> bacf6c4a5255d93fe4ee32468f44270553fa6b40

    }

    private void updateScore()
    {
        counter = counter + 100;
        coinDisplay.text = counter.ToString();
    }

    private void OnDisable()
    {
        EventManager.StopListening(GameEvent.levelFinished, updateScore);
    }
}
