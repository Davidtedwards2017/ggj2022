using gamedev.utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class BrickMover : MonoBehaviour
{
    public Brick Brick;
    public GlobalPropertiesSO globalProperties => Brick.globalProperties;
    public Side Side => Brick.Side;

    public UnityEvent OnRequestStopMovement;
    public BrickEvent OnStopMovement;

    [ReadOnly]
    public Vector2 Velocity;

    public void Init(Brick brick)
    {
        Brick = brick;
    }

    public void UpdateMovement()
    {
        var direction = globalProperties.GetBrickMovementDirection(Side);

        Velocity = direction.normalized * globalProperties.BrickFallingSpeed * Time.deltaTime;
        Brick.transform.Translate(Velocity);
        Trace(direction);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        var direction = globalProperties.GetBrickMovementDirection(Side);
        var origin = Brick.transform.position +
            (new Vector3(direction.x, direction.y, 0) * (globalProperties.BrickSize.y / 2));

        Gizmos.DrawRay(origin, direction * 0.2f);
    }

    public void StopMovement()
    {
        Velocity = Vector2.zero;
        OnStopMovement?.Invoke(Brick);
    }

    private void Trace(Vector2 direction)
    {
        var origin = Brick.transform.position + 
            (new Vector3(direction.x, direction.y, 0) * (globalProperties.BrickSize.y / 2));

        RaycastHit2D[] hits = Physics2D.RaycastAll(
            origin, 
            direction, 
            0.2f);

        foreach (var hit in hits)
        {
            if (hit.collider == null) continue;
            Brick brick = hit.collider.GetComponentInParent<Brick>();
            if (brick != null)
            {
                OnRequestStopMovement?.Invoke();
                return;
            }

            CharacterController character = hit.collider.GetComponentInParent<CharacterController>();
            if (character != null && character.side == Side)
            {
                character.RequestSquish();
            }

            
            if (hit.collider.tag == "Floor")
            {
                OnRequestStopMovement?.Invoke();
            }
        }
    }
}
