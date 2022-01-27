using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PunchEffect : MonoBehaviour
{
    public enum PunchType 
    {
        Rotation,
        Position
    }

    public PunchType type;

    public PunchEffectSettings settings;

    private Quaternion cachedRot;
    private Vector3 cachedPos;

    private Tweener tween;

    protected void Start()
    {
        cachedRot = transform.localRotation;
        cachedPos = transform.localPosition;
    }

    public void Peform()
    {
        DoPunch();
    }

    private void DoPunch()
    {
        //if (tween != null && tween.active)
        //{
        //    tween.Kill(true);
        //}

        var x = Random.Range(settings.punch_min.x, settings.punch_max.x);
        var y = Random.Range(settings.punch_min.y, settings.punch_max.y);
        var z = Random.Range(settings.punch_min.z, settings.punch_max.z);

        Vector3 punch = new Vector3(x, y, z);

        var duration = Random.Range(settings.duration_min, settings.duration_max);
                
        switch (type)
        {
            case PunchType.Rotation:
                RotPunch(punch, duration);
                break;
            case PunchType.Position:
                MovePunch(punch, duration);
                break;
        }
    }

    private void RotPunch(Vector3 punch, float duration)
    {
        tween = transform.DOPunchRotation(punch, duration, settings.vibro, settings.elasticity)
        .SetEase(settings.ease)
        .OnComplete(() => Reset());
    }

    private void MovePunch(Vector3 punch, float duration)
    {
        tween = transform.DOPunchPosition(punch, duration, settings.vibro, settings.elasticity)
        .SetEase(settings.ease)
        .OnComplete(() => Reset());
    }

    protected void Reset()
    {
        transform.localRotation = cachedRot;
        transform.localPosition = cachedPos;
    }
}
