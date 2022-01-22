using gamedev.utilities.events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickEventChannelListener : ChannelListener<BrickEventChannel, Brick>
{
    public BrickEvent Event;

    protected override void RaiseEvent(Brick value)
    {
        Event?.Invoke(value);
    }
}
