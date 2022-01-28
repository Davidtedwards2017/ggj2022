using gamedev.utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterFalling : MonoBehaviour
{
    public Transform root;
    public CharacterController controller;
    public GlobalPropertiesSO globalProperties => controller.globalProperties;

    public UnityEvent OnRequestStopMovement;
    public BrickEvent OnStopMovement;

    [ReadOnly]
    public Vector2 Velocity;

    public void UpdateMovement()
    {
        var direction = globalProperties.GetBrickMovementDirection(controller.side);
        Velocity = direction.normalized * globalProperties.character.FallSpeed * Time.deltaTime;
        if (Trace(direction))
        {
            root.transform.Translate(Velocity);
        }
    }

    private bool Trace(Vector2 direction)
    {
        var origin = root.position +
            (new Vector3(direction.x, direction.y, 0) * (globalProperties.BrickSize.y / 2));

        RaycastHit2D[] hits = Physics2D.RaycastAll(
            origin,
            direction,
            Velocity.magnitude);

        foreach (var hit in hits)
        {
            if (hit.collider == null) continue;
            Brick brick = hit.collider.GetComponentInParent<Brick>();
            if (brick != null)
            {
                OnRequestStopMovement?.Invoke();
                return false;
            }

            if (hit.collider.tag == "Floor")
            {
                OnRequestStopMovement?.Invoke();
                return false;
            }
        }

        return true;
    }
}
