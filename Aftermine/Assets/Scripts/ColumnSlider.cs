using gamedev.utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ColumnSlider : MonoBehaviour
{
    public Grid Grid;
    public BrickCollection Collection;
    [ReadOnly]
    public int Column;


    public SlideGroup SlidingGroupPrefab;

    public void SelectColumn(int column)
    {
        Column = column;
    } 
    
    public void RequestSlideUp(int spaces = 1)
    {
        Debug.Log(string.Format("Requesting Slide Up column:{0} spaces:{1}", Column, spaces));
        SpawnSlidingGroup(Column, Side.Lower, spaces);
    }

    public void RequestSlideDown(int spaces = 1)
    {
        Debug.Log(string.Format("Requesting Slide Down column:{0} spaces:{1}", Column, spaces));
        SpawnSlidingGroup(Column, Side.Upper, spaces);
    }

    private void SpawnSlidingGroup(int column, Side fromSide, int spaces)
    {
        var brickGroups = Collection.GetBricksInColumn(column)
            .Select(b => b.group)
            .ToList();

        var slidingGroup = Instantiate(SlidingGroupPrefab);
        slidingGroup.Init(column, brickGroups, fromSide, spaces);
    }
}
