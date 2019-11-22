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
           EventManager.StartListening(GameEvent.playerWon, updateScore); //when level is completed, add 100 to score
    }

    void Start()
    {
        counter = 0;
        coinDisplay.text = "";
        coinDisplay.text = counter.ToString();
    }

    // Update is called once per frame
    void Update()
    {


    }

    private void updateScore()
    {
        counter = counter + 100;
        coinDisplay.text = counter.ToString();
    }

    private void OnDisable()
    {
          EventManager.StopListening(GameEvent.playerWon, updateScore);
    }
}
