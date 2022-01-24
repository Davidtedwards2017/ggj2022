using gamedev.utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGroundCheck : MonoBehaviour
{

    public Transform root;
    [ReadOnly]
    public bool Grounded;

    //public Collider2D Collider;
    public CharacterController controller;

    private GlobalPropertiesSO Properties => controller.globalProperties;

    public void LateUpdate()
    {
        var direction = Properties.GetBrickMovementDirection(controller.side);
        Grounded = Trace(direction);
    }

    private bool Trace(Vector2 direction)
    {
        var origin = root.position +
            (new Vector3(direction.x, direction.y, 0) * (Properties.BrickSize.y / 2));

        RaycastHit2D[] hits = Physics2D.RaycastAll(
            origin,
            direction,
            0.2f);

        foreach (var hit in hits)
        {
            if (hit.collider == null) continue;

            //if (hit.collider == null || hit.collider == Collider) continue;

            return true;
        }

        return false;
    }
}
