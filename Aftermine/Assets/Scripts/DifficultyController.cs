using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using gamedev.utilities;
using System;

public class DifficultyController : MonoBehaviour
{
    [ReadOnly]
    public int brickClearCount = 0;
    [ReadOnly]
    public int difficulty = 0;
    public List<DifficultySetting> difficultyLevels;

    public DifficultySetting current => difficultyLevels[difficulty];

    private void OnValidate()
    {
        difficultyLevels = difficultyLevels.OrderBy(b => b.BricksCleared).ToList();
    }

    public void Reset()
    {
        difficulty = 0;
    }

    public void BrickCleared(Brick brick)
    {
        if (brick == null) return;

        brickClearCount++;

        if (difficulty + 1 >= difficultyLevels.Count) return;


        var nextDifficulty = difficultyLevels[difficulty + 1];
        if (nextDifficulty == null) return;

        if (brickClearCount >= nextDifficulty.BricksCleared)
        {
            difficulty++;
        }
    }
}
