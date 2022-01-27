using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickShakeOnLandEffect : PunchEffect
{
    public Brick brick;

    public void RequestPeform(Brick brick)
    {
        if (this.brick == brick) return;
        if (!this.brick.AtRest()) return;

        Peform();
    }
}
