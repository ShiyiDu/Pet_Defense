using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;
using UnityEngine.SceneManagement;

//access the level data through this class
public class LevelReader : MonoBehaviour
{
    public GhostTypeSheet ghostSheet;
    public LevelData[] allLevels;
    private static LevelData levelData;
    private static bool levelUpdated; //did the level just got updated and not effective yet?
    private static int currentLevelNumber = 0;
    private static PetLevel currentLevel;

    private static LevelReader levelReader;
    private static LevelReader instance
    {
        get
        {
            if (!levelReader) {
                levelReader = FindObjectOfType<LevelReader>();

                if (!levelReader) {
                    Debug.LogError("no level reader");
                }
            }

            return levelReader;
        }
    }

    public static int GetCurrentLevelNum()
    {
        return currentLevelNumber;
    }

    public static void LoadLevel(int levelNum)
    {
        currentLevelNumber = levelNum;
        levelData = instance.allLevels[levelNum];
        levelUpdated = true;
        PetUtility.instance.LevelRestart();
    }

    public static PetLevel GetCurrentLevel()
    {
        if (currentLevel == null || levelUpdated) {
            currentLevel = new PetLevel();
            //Debug.Log("ghost to be spawn: " + instance.levelData.spawnInfos.Length);
            foreach (PetLevel.SpawnInfo info in levelData.spawnInfos) {
                currentLevel.AddSpawn(info.time, info.type, info.spot);
                //Debug.Log("add ghost");
            }
            levelUpdated = false;
        }
        return currentLevel;
    }

    public static Vector2 GetSpawnPoint(int index)
    {
        return PetUtility.instance.spawnPoints[index];
    }

    public static Ghost GetGhostOfType(int type)
    {
        return instance.ghostSheet.allGhosts[type];
    }

    // Start is called before the first frame update
    void Start()
    {
        //destroy other instances
        if (!levelUpdated) {
            levelData = allLevels[currentLevelNumber];
            levelUpdated = true;
            PetDistributor.Distribute(currentLevelNumber);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
