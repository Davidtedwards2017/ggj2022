using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(menuName = "GGJ/Punch Effect Settings")]
public class PunchEffectSettings : ScriptableObject
{
    public Vector3 punch_min;
    public Vector3 punch_max;
    public float duration_min = 1;
    public float duration_max = 1;

    [Range(1, 50)]
    public int vibro = 10;

    [Range(0, 1)]
    public float elasticity = 1;

    public Ease ease;
}
