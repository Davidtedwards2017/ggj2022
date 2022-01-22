using gamedev.utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class BrickMover : MonoBehaviour
{
    public Brick Brick;
    public Collider2D Collider;
    public GlobalPropertiesSO globalProperties;

    public UnityEvent OnRequestStopMovement;
    public BrickEvent OnStopMovement;

    [ReadOnly]
    public Vector2 Direction;
    [ReadOnly]
    public Vector2 Velocity;

    public void Init(Brick brick)
    {
        Brick = brick;
        Direction = globalProperties.GetBrickMovementDirection(brick.Side);
    }

    public void UpdateMovement()
    {
        Velocity = Direction.normalized * globalProperties.BrickFallingSpeed * Time.deltaTime;
        Brick.transform.Translate(Velocity);
        Trace();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(Brick.transform.position, Velocity);
    }

    public void StopMovement()
    {
        Velocity = Vector2.zero;
        OnStopMovement?.Invoke(Brick);
    }

    private void Trace()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(
            Brick.transform.position, 
            globalProperties.BrickSize, 
            0, 
            Direction, 
            0.01f);

        var ignoreColliders = Brick.group.Bricks.Select(b => b.mover.Collider).ToList();

        foreach (var hit in hits)
        {
            if (hit.collider != null && !ignoreColliders.Contains(hit.collider))
            {
                OnRequestStopMovement?.Invoke();
                return;
            }
        }
    }
}
