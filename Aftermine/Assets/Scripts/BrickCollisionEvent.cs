using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class BrickCollisionEvent : UnityEvent<Brick, Collision2D>
{

}
