using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BrickGroup : MonoBehaviour
{
    public BrickGroupEventChannel OnCreatedChannel;
    public BrickGroupEventChannel OnDestroyedChannel;
    public BrickGroupEventChannel OnStopped;

    public List<Brick> Bricks;
    public Side Side;

    public BrickGroupStateController stateController;

    public void Init(Side side, BrickType type)
    {
        Side = side;

        var bricks = GetComponentsInChildren<Brick>();
        foreach (var brick in bricks)
        {
            Add(brick);
            brick.Init(side, type);
        }

        RequestMovement();
    }

    private void OnEnable()
    {
        OnCreatedChannel.Set(this);
    }

    private void OnDisable()
    {
        OnDestroyedChannel.Set(this);
    }

    public void Add(Brick brick)
    {
        if (Bricks.Contains(brick)) return;
        brick.group = this;
        Bricks.Add(brick);
    }

    public void Remove(Brick brick)
    {
        if (brick != null && Bricks.Contains(brick))
        {
            Bricks.Remove(brick);
        }

        CheckIfShouldDestroy();
    }

    public void RequestStartSlide(BrickSlidingEventArgs args)
    {
        stateController.StartSliding(args);
    }

    public void RequestStopSliding()
    {

    }

    public void RequestMovement()
    {
        stateController.StartMovement();
    }

    public void RequestStop()
    {
        OnStopped.Set(this);
    }

    private void CheckIfShouldDestroy()
    {
        if (!Bricks.Any())
        {
            Destroy(gameObject);
        }
    }
}
