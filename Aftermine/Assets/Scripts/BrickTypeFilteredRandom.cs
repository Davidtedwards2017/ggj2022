using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickTypeFilteredRandom : FilteredRandom<BrickType>
{
    private DifficultyController difficultyController;

    public BrickTypeFilteredRandom(IEnumerable<BrickType> collection, int historyLength, DifficultyController difficultyController) 
        : base (collection, historyLength)
    {
        this.PreventPattern = false;
        this.difficultyController = difficultyController;
    }

    protected override bool InvalidPick(BrickType pick)
    {
        return base.InvalidPick(pick) || !CanSpawn(pick);
    }

    private bool CanSpawn(BrickType brick)
    {
        return brick.CanSpawn(difficultyController.current);
    }

}
