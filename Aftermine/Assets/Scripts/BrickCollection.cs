using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void Remove(Brick brick)
    {
        if (brick != null && Bricks.Contains(brick))
        {
            Bricks.Remove(brick);
        }
    }
}
