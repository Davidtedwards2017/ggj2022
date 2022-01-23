using gamedev.utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SlideGroup : MonoBehaviour
{
    public GlobalPropertiesSO GlobalProperties;
    [ReadOnly]
    public Side FromSide;
    [ReadOnly]
    public List<BrickGroup> BrickGroups;
    [ReadOnly]
    public int Column;

    [ReadOnly]
    public int Distance;

    public Vector2 Direction => GlobalProperties.GetBrickMovementDirection(FromSide);


    public List<Brick> Bricks => 
        BrickGroups.SelectMany(group => group.Bricks)
        .ToList();

    public float Duration = 0.3f;

    public BrickSlidingEvent OnRequestBrickSlidingStart;
    public BrickSlidingEvent OnRequestBrickSlidingEnd;

    public void Init(int column, List<BrickGroup> brickGroups, Side fromSide, int distance = 1)
    {
        Column = column;
        FromSide = fromSide;
        BrickGroups = new List<BrickGroup>(brickGroups);
        Distance = distance;

        StartCoroutine(Sequence());
    }

    private IEnumerator Sequence()
    {
        RequestBrickSlidingStart();
        yield return new WaitWhile(() => AnyBricksSliding());
        RequestBrickSlidingEnd();
        Destroy(gameObject);
    }

    private bool AnyBricksSliding()
    {
        var groups = new List<BrickGroup>(BrickGroups);
        return groups.Any(group => group.stateController.IsSliding());
    }

    private void RequestBrickSlidingStart()
    {
        var args = new BrickSlidingEventArgs()
        {
            SlideGroup = this
        };

        OnRequestBrickSlidingStart?.Invoke(args);

        foreach (var group in BrickGroups)
        {
            group.RequestStartSlide(args);
        }
    }

    private void RequestBrickSlidingEnd()
    {
        var args = new BrickSlidingEventArgs()
        {
            SlideGroup = this
        };

        OnRequestBrickSlidingEnd?.Invoke(args);
    }
}