using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public GlobalPropertiesSO globalProperties;
    public Side Side;
    public BrickMover mover;
    public BrickSlider slider;
    public BrickGroup group;
    public BrickType type;
    public GridObject gridObject;

    public BrickEvent OnRequestBrickInit;
    public BrickTypeEvent OnSetBrickType;

    public BrickEventChannel BrickCreatedChannel;
    public BrickEventChannel BrickDestroyedChannel;

    bool beingCleared = false;

    public void Init(Side side, BrickType type)
    {
        Side = side;
        this.type = type;
        OnRequestBrickInit?.Invoke(this);
        OnSetBrickType?.Invoke(this.type);
    }

    public bool AtRest()
    {
        return !beingCleared && 
            group.stateController.IsStopped();
    }

    public void RequestClearStart()
    {
        beingCleared = true;
    }

    public void RequestClearEnd()
    {
        Destroy(gameObject);
    }

    private void OnEnable()
    {
        BrickCreatedChannel.Set(this);
    }

    private void OnDisable()
    {
        BrickDestroyedChannel.Set(this);
    }
}
