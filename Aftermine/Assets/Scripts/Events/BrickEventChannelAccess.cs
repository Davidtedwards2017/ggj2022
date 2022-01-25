using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickEventChannelAccess : MonoBehaviour
{
    public BrickEventChannel Channel;

    public void Raise(Brick brick)
    {
        Channel.Set(brick);
    }
}
