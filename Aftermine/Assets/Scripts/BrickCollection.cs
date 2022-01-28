using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BrickCollection : MonoBehaviour
{
    public BrickEventChannel BrickCreatedChannel;
    public BrickEventChannel BrickDestroyedChannel;

    public List<Brick> Bricks;

    public void Awake()
    {
        BrickCreatedChannel.Event.AddListener(() => Add(BrickCreatedChannel.GetValue()));
        BrickDestroyedChannel.Event.AddListener(() => Remove(BrickDestroyedChannel.GetValue()));
    }

    public void Add(Brick brick)
    {
        if (brick != null && !Bricks.Contains(brick))
        {
            Bricks.Add(brick);
        }
    }

    public List<BrickType> GetBrickTypesInColumn(int column)
    {
        return GetBricksInColumn(column).Select(b => b.type).Distinct().ToList();
    } 

    public void Remove(Brick brick)
    {
        if (brick != null && Bricks.Contains(brick))
        {
            Bricks.Remove(brick);
        }
    }

    public List<Brick> GetBricksInColumn(int column)
    {
        var columnBricks = new List<Brick>();

        foreach (var brick in Bricks)
        {
            if (brick.AtRest() && brick.gridObject.Column == column)
            {
                columnBricks.Add(brick);
            }
        }

        return columnBricks;
    }

    public void ClearBricks()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
