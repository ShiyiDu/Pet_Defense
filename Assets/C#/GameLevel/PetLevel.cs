using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetLevel
{
    List<SpawnInfo> spawns;
    int current;//what is the next ghost to generate?

    //a list contain ghost type, generate time, generate spot
    [Serializable]
    public class SpawnInfo
    {
        public SpawnInfo(float time, int type, int spot)
        {
            this.time = time;
            this.type = type;
            this.spot = spot;
        }
        public float time; //when does the ghost generate
        public int type; //what type of ghost to generate
        public int spot; //where does this ghost being generated
    }

    public PetLevel()
    {
        spawns = new List<SpawnInfo>();
        current = 0;
    }

    public void AddSpawn(float spawnTime, int ghostType, int spawnSpot)
    {
        SpawnInfo newInfo = new SpawnInfo(spawnTime, ghostType, spawnSpot);
        spawns.Add(newInfo);
        //do an insertion sort, make sure the list is sorted based on time
        int newPos = spawns.Count - 1;
        while (newPos > 0 && spawns[newPos].time < spawns[newPos - 1].time) {
            SpawnInfo temp = spawns[newPos - 1];
            spawns[newPos - 1] = spawns[newPos];
            spawns[newPos] = temp;
        }
    }

    /// <summary>
    /// return the next spawn and move cursor to the next spawn
    /// </summary>
    public SpawnInfo NextSpawn()
    {
        if (current == spawns.Count) return null;
        return spawns[current++];
    }
}
