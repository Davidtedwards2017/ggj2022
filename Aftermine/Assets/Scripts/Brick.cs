using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public GlobalPropertiesSO globalProperties;

    public Side Side;

    public BrickMover mover;

    public BrickEvent OnRequestBrickInit;

    public BrickGroup group;

    public BrickType type;

    public BrickTypeEvent OnSetBrickType;

    public void Init(Side side, BrickType type)
    {
        Side = side;
        this.type = type;
        OnRequestBrickInit?.Invoke(this);
        OnSetBrickType?.Invoke(this.type);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
