using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GGJ/Global Properties")]
public class GlobalPropertiesSO : ScriptableObject
{
    public Vector2 BrickSize = Vector2.one;
    public float GridSnapDistance = 0.7f;

    public List<BrickType> BrickTypes;

    public Character character;
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

    [System.Serializable]
    public class Character
    {
        public float MovementDuration = 0.15f;
        public float MovementJumpHeight = 1.5f;

        public float FloorPositionOffset = 0.2f;
        public float BrickPositionOffset = 0.1f;

        public float FallSpeed = 1f;
    }

}
