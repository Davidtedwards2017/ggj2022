using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public GlobalPropertiesSO globalProperties;
    public Side Side;
    public BrickMover mover;
    public BrickSlider slider;
    public BrickGroundCheck groundCheck;
    public BrickGroup group;
    public BrickType type;
    public GridObject gridObject;
    public BrickJiggle Jiggle;


    public BrickEvent OnRequestBrickInit;
    public BrickTypeEvent OnSetBrickType;

    public BrickEventChannel BrickCreatedChannel;
    public BrickEventChannel BrickDestroyedChannel;

    bool beingCleared = false;

    public SideEvent OnInitFromSide;
    public BrickEvent OnBrickPositionUpdated;

    public void Init(Side side, BrickType type)
    {
        Side = side;
        this.type = type;
        OnRequestBrickInit?.Invoke(this);
        OnSetBrickType?.Invoke(this.type);

        OnInitFromSide?.Invoke(side);
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
        group.Remove(this);
        Destroy(gameObject);
    }

    public void UpdateCell(Cell cell)
    {
        Side = cell.Side;
        OnBrickPositionUpdated?.Invoke(this);
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
