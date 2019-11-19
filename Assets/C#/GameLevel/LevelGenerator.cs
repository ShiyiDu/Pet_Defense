using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private PetLevel currentLevel;
    private PetLevel.SpawnInfo nextSpawn;
    // Start is called before the first frame update
    void Start()
    {
        currentLevel = LevelReader.GetCurrentLevel();
        nextSpawn = currentLevel.NextSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (nextSpawn == null) {
            return;
        }
        if (Time.timeSinceLevelLoad >= nextSpawn.time) {
            SpawnGhost();
            nextSpawn = currentLevel.NextSpawn();
        }
    }

    void SpawnGhost()
    {
        GameObject newGhost = Instantiate(LevelReader.GetGhostOfType(nextSpawn.type).gameObject);
        newGhost.transform.position = LevelReader.GetSpawnPoint(nextSpawn.spot);
    }
}
