using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SideEventRouter : MonoBehaviour
{
    public UnityEvent OnUpperSide;
    public UnityEvent OnLowerSide;

    public void Raise(Brick brick)
    {
        Raise(brick.Side);
    }

    public void Raise(Side side)
    {
        switch (side)
        {
            case Side.Lower:
                OnLowerSide?.Invoke();
                break;
            case Side.Upper:
                OnUpperSide?.Invoke();
                break;
        }
    }
}
