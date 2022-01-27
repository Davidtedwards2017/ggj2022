using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickTypeEffectGetter : MonoBehaviour
{
    public GameObjectEvent OnRequestClearEffectPrefab;
    
    public void RequestClearEffectPrefab(Brick brick)
    {
        RequestClearEffectPrefab(brick.type);
    }

    public void RequestClearEffectPrefab(BrickType type)
    {
        OnRequestClearEffectPrefab?.Invoke(type.ClearEffectPrefab);
    }
}
