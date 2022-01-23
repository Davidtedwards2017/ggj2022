using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MatchChecker : MonoBehaviour
{
    public BrickCollection Collection;
    public Grid Grid;

    public BrickMatchEvent OnBrickMatchDetected;

    private void Update()
    {
        CheckForMatch();
    }

    public void CheckForMatch()
    {
        var bricksInRows = GetBricksInRows();
       
        foreach (var row in bricksInRows.Keys)
        {
            var bricks = bricksInRows[row];
            if (bricks == null ) continue;

            if (bricks.Count >= Grid.ColumnCount)
            {
                var brickTypesInRow = bricks
                    .Select(b => b.type)
                    .Distinct()
                    .ToList();

                if (brickTypesInRow.Count == 1)
                {
                    DetectedMatch(row, bricks, brickTypesInRow[0]);
                }
            }
        }
    }

    private Dictionary<int, List<Brick>> GetBricksInRows()
    {
        var bricks = new Dictionary<int, List<Brick>>();
        var temp = new List<Brick>(Collection.Bricks);

        foreach (var b in temp)
        {
            if (IsValid(b))
            {
                var row = b.gridObject.Row;
                
                if (!bricks.ContainsKey(row))
                {
                    bricks.Add(row, new List<Brick>());
                }

                var rowBricks = bricks[row];
                if (!rowBricks.Contains(b))
                {
                    rowBricks.Add(b);
                }
            }
        }

        return bricks;
    }

    private bool IsValid(Brick brick)
    {
        return brick.AtRest();
    }


    private void DetectedMatch(int row, List<Brick> bricks, BrickType type)
    {
        OnBrickMatchDetected?.Invoke(
            new BrickMatchArgs()
            {
                Row = row,
                Bricks = new List<Brick>(bricks),
                Type = type
            }
        );
    }
}
