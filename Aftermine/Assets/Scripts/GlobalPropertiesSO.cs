using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GGJ/Global Properties")]
public class GlobalPropertiesSO : ScriptableObject
{
    public Vector2 BrickSize = Vector2.one;
    public float GridSnapDistance = 0.7f;
    public float BrickFallingSpeed = 1;
    public float TimeBetweenBrickSpawns = 1.0f;
    public Prefabs prefabs;

    public Vector2 GetBrickMovementDirection(Side side)
    {
        return side == Side.Upper ? Vector2.down : Vector2.up;
    }

    [System.Serializable]
    public class Prefabs 
    {
        public LineClearSequence LineClearSequence;

    }

}
