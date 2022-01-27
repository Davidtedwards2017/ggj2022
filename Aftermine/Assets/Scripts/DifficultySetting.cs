using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "GGJ/Difficulty Data")]
public class DifficultySetting : ScriptableObject
{
    public int BricksCleared = 0;

    public float TimeBetweenSpawns = 1.0f;
    public int BricksPerSpawn = 1;
    public float BrickFallSpeed = 10;
    public float BrickJiggleDuration = 2.0f;

    public List<BrickType> BrickTypes;
}
