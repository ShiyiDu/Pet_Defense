﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkIfFinished : MonoBehaviour
{
    //public LevelReader levelReader;
    public float lastSpawn;
    public int length;

    private bool afterTime;

    // Start is called before the first frame update
    void Start()
    {
        afterTime = true;
    }

    void Update()
    {
        length = LevelReader.GetCurrentLevel().spawns.Count;  //get length of current spawn list
        lastSpawn = LevelReader.GetCurrentLevel().spawns[length - 1].time;

        if (lastSpawn <= Time.timeSinceLevelLoad) { //after final spawn time has passed

            if (afterTime == true) {
                EventManager.StartListening(ParameterizedGameEvent.unitDead, checkForUnits);
                afterTime = false;
                Debug.Log("last spawn has happened");
                Debug.Log("listening fo unitDead");
            }

        }
    }

    //Every time a unit dies, check if there are any ghosts or pets left
    public void checkForUnits(object obj)
    {
        void Check()
        {
            Ghost[] ghosts = FindObjectsOfType<Ghost>();
            Pet[] pets = FindObjectsOfType<Pet>();

            Debug.Log("ghost array:" + ghosts.Length);
            Debug.Log("pet array:" + pets.Length);

            if (ghosts.Length - 1 < 0) {
                EventManager.TriggerEvent(GameEvent.levelFinished);
                EventManager.TriggerEvent(GameEvent.playerWon);
                Debug.Log("player won triggered, level finsihed trigger");
            } else if (pets.Length - 1 < 0) {
                EventManager.TriggerEvent(GameEvent.levelFinished);
                EventManager.TriggerEvent(GameEvent.playerLost);
                Debug.Log("player lost triggered, level finish triggered");
            }
        }

        PetUtility.WaitAndDo(0.03f, Check); //because the unit is destroied later
    }

    private void OnDisable()
    {
        EventManager.StopListening(ParameterizedGameEvent.unitDead, checkForUnits);
        Debug.Log("stopped listening for unit dead");
    }

}


