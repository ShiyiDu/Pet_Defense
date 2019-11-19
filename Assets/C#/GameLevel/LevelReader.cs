using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;

//access the level data through this class
public class LevelReader : MonoBehaviour
{
    public GhostTypeSheet ghostSheet;
    public LevelData levelData;

    private PetLevel currentLevel;

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

    public static PetLevel GetCurrentLevel()
    {
        if (instance.currentLevel == null) {
            instance.currentLevel = new PetLevel();
            //Debug.Log("ghost to be spawn: " + instance.levelData.spawnInfos.Length);
            foreach (PetLevel.SpawnInfo info in instance.levelData.spawnInfos) {
                instance.currentLevel.AddSpawn(info.time, info.type, info.spot);
                //Debug.Log("add ghost");
            }
        }
        return (FindObjectOfType<LevelReader>()).currentLevel;
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
    }

    // Update is called once per frame
    void Update()
    {

    }
}
