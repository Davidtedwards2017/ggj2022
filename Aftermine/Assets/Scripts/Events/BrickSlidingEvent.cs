using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class BrickSlidingEvent : UnityEvent<BrickSlidingEventArgs>
{

}


[System.Serializable]
public class BrickSlidingEventArgs
{
    public SlideGroup SlideGroup;
}
