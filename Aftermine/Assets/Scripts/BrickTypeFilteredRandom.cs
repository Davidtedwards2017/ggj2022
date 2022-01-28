using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BrickTypeFilteredRandom : FilteredRandom<BrickType>
{
    private DifficultyController difficultyController;
    private BrickCollection brickCollection;
    private int column;

    public BrickTypeFilteredRandom(IEnumerable<BrickType> collection, int historyLength, 
        DifficultyController difficultyController, BrickCollection brickCollection, int column) 
        : base (collection, historyLength)
    {
        this.AllowInvalidAfterExaustedAttempts = false;
        this.PreventPattern = false;
        this.difficultyController = difficultyController;
        this.brickCollection = brickCollection;
        this.column = column;
    }

    public override BrickType GetNextRandom()
    {
        var typesNotInList = GetBrickTypeNotInColumn();

        if (typesNotInList.Any())
        {
            return typesNotInList.PickRandom();
        }
        else
        {
            return base.GetNextRandom();
        }
    }

    protected override bool InvalidPick(BrickType pick)
    {
        return base.InvalidPick(pick) || !CanSpawn(pick);
    }

    private bool CanSpawn(BrickType brick)
    {
        return brick.CanSpawn(difficultyController.current);
    }

    private List<BrickType> GetBrickTypeNotInColumn()
    {
        var typesInColumm = brickCollection.GetBrickTypesInColumn(column);
        var typesAvailable = difficultyController.current.BrickTypes;

        return typesAvailable.Except(typesInColumm).ToList();
    }

}
