using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Level")]
public class LevelData : ScriptableObject
{

    public PetLevel.SpawnInfo[] spawnInfos;
}
