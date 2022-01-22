using gamedev.utilities.events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickGroupChannelListener : ChannelListener<BrickGroupEventChannel, BrickGroup>
{
    public BrickGroupEvent Event;

    protected override void RaiseEvent(BrickGroup value)
    {
        Event?.Invoke(value);
    }
}
