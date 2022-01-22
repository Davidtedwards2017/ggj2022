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

    public void Init(Side side)
    {
        Side = side;
        OnRequestBrickInit?.Invoke(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
