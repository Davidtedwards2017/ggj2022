using gamedev.utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BrickSlider : MonoBehaviour
{
    public Brick Brick;
    public GlobalPropertiesSO globalProperties;
    public Collider2D Collider;

    [ReadOnly]
    public Vector2 Direction;
    [ReadOnly]
    public Vector2 Velocity;
    [ReadOnly]
    public float Distance;
    [ReadOnly]
    public float Speed;

    public BrickEvent OnCollisionWithFallingBrick;
    public BrickEvent OnRequestStopSlide;

    public BrickEvent OnStartSlide;
    public BrickEvent OnStopSlide;

    [ReadOnly]
    public SlideGroup slideGroup;

    public void StartSliding(SlideGroup slideGroup)
    {
        OnStartSlide?.Invoke(Brick);
        this.Direction = slideGroup.Direction;
        this.slideGroup = slideGroup;
        this.Distance = slideGroup.Distance;
        Speed = slideGroup.Distance / slideGroup.Duration;
    }

    public void UpdateMovement()
    {
        Velocity = Direction.normalized * Speed * Time.deltaTime;
        Brick.transform.Translate(Velocity);
        var distanceTraveled = Velocity.magnitude;
        Distance -= distanceTraveled;
        Trace();

        if (Distance <= 0)
        {
            RequestStopSlide();
        }
    }

    private void Trace()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(
            Brick.transform.position,
            globalProperties.BrickSize,
            0,
            Direction,
            0.01f);

        var siblingBricks = new List<Brick>(slideGroup.Bricks);

        foreach (var hit in hits)
        {
            if (hit.collider == null) continue;

            var brick = hit.collider.GetComponentInParent<Brick>();
            if (brick != null)
            {
                if (siblingBricks.Contains(brick)) continue;

                DetectedCollisionWithFallingBrick(brick);
            }
        }
    }

    private void DetectedCollisionWithFallingBrick(Brick brick)
    {
        Debug.LogError("Detected Collision With Falling Brick");
        OnCollisionWithFallingBrick?.Invoke(brick);
    }

    public void RequestStopSlide()
    {
        OnRequestStopSlide?.Invoke(Brick);
    }

    public void StopSlide()
    {
        Velocity = Vector2.zero;
        OnStopSlide?.Invoke(Brick);
    }

}
