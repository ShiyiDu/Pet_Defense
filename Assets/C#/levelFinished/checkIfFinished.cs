using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkIfFinished : MonoBehaviour
{ 
    public LevelReader levelReader;
    public float time;
    public int length;

    private bool afterTime;

    // Start is called before the first frame update
    void Start()
    {
        afterTime = true;
    }

    void Update()
    {
        length = levelReader.levelData.spawnInfos.Length;  //get length of current spawn list
        //get length of current spawn list
        time = levelReader.levelData.spawnInfos[length - 1].time; //the time of the current final ghost spawn

        //CHANGE LATER
        if(time <= 200/*placeholder for current time*/){ //after final spawn time has passed

            if (afterTime == true)
            {
                EventManager.StartListening(ParameterizedGameEvent.unitDead, checkForUnits);
                afterTime = false;
            }

        }
    }

    //Every time a unit dies, check if there are any ghosts or pets left
    public void checkForUnits(object obj)
    {
        var ghosts = FindObjectsOfType<Ghost>();
        var pets = FindObjectsOfType<Pet>();

        if (ghosts.Length < 1)
        {
            EventManager.TriggerEvent("levelFinished");
            EventManager.TriggerEvent("playerWon");
        }
        else if (pets.Length < 1)
        {
            EventManager.TriggerEvent("levelFinished");
            EventManager.TriggerEvent("playerLost");
        }
    }

}


