using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{
    public static int counter;

    private void OnEnable()
    {
           EventManager.StartListening(GameEvent.levelFinished, updateScore); //when level is completed, add 100 to score
    }


    // Update is called once per frame
    void Update()
    {


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
