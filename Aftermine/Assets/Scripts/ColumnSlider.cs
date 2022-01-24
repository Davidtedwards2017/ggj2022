using gamedev.utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ColumnSlider : MonoBehaviour
{
    public Grid Grid;
    public BrickCollection Collection;
    [ReadOnly]
    public int Column;


    public SlideGroup SlidingGroupPrefab;

    public UnityEvent OnSuccess;
    public UnityEvent OnFailed;

    public void SelectColumn(int column)
    {
        Column = column;
    } 
    
    public void RequestSlideUp(int spaces = 1)
    {
        SpawnSlidingGroup(Column, Side.Lower, spaces);
    }

    public void RequestSlideDown(int spaces = 1)
    {
        SpawnSlidingGroup(Column, Side.Upper, spaces);
    }

    private void SpawnSlidingGroup(int column, Side fromSide, int spaces)
    {
        var brickGroups = Collection.GetBricksInColumn(column)
            .Select(b => b.group)
            .ToList();

        bool canSlideFromSide = brickGroups.SelectMany(group => group.Bricks).Any(b => b.Side == fromSide);

        if (canSlideFromSide)
        {
            var slidingGroup = Instantiate(SlidingGroupPrefab);
            slidingGroup.Init(column, brickGroups, fromSide, spaces);
            OnSuccess?.Invoke();
        }
        else
        {
            OnFailed?.Invoke();
        }

    }
}
