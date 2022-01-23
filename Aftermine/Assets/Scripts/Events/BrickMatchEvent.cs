using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class BrickMatchEvent : UnityEvent<BrickMatchArgs>
{

}

[System.Serializable]
public class BrickMatchArgs 
{
    public int Row;
    public List<Brick> Bricks;
    public BrickType Type;
}

