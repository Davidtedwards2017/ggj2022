using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(menuName = "GGJ/Punch Effect Settings")]
public class PunchEffectSettings : ScriptableObject
{
    public Vector3 punch;
    public float duration = 1;
    
    [Range(1, 50)]
    public int vibro = 10;

    [Range(0, 1)]
    public float elasticity = 1;

    public Ease ease;
}
