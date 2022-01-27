using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GGJ/Brick Type")]
public class BrickType : ScriptableObject
{
    public List<Sprite> sprites;
    public int ClearValue = 1; 

    public bool CanSpawn(DifficultySetting difficulty)
    {
        return difficulty.BrickTypes.Contains(this);
    }
}