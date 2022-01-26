using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PunchEffect : MonoBehaviour
{
    public PunchEffectSettings settings;

    private Quaternion cachedRot; 

    private void Start()
    {
        cachedRot = transform.localRotation;
    }

    public void Peform()
    {
        transform.DOPunchRotation(settings.punch, settings.duration, settings.vibro, settings.elasticity)
            .SetEase(settings.ease)
            .OnComplete(() => Reset());
    }

    private void Reset()
    {
        transform.localRotation = cachedRot;
    }
}
