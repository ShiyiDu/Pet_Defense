using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{
    public static int counter;


    private void OnLevelWasLoaded(int level)
    {
        EventManager.StartListening(GameEvent.levelFinished, updateScore); //when level is completed, add 100 to score
    }

    private void updateScore()
    {
        counter = counter + 100;
        this.GetComponent<UnityEngine.UI.Text>().text = counter.ToString();
    }

    private void OnDisable()
    {
          EventManager.StopListening(GameEvent.levelFinished, updateScore);
    }
}
