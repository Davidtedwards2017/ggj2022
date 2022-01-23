using gamedev.utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BrickGroundCheck : MonoBehaviour
{
    [ReadOnly]
    public bool Grounded;

    public Collider2D Collider;
    public Brick Brick;

    private GlobalPropertiesSO Properties => Brick.globalProperties;

    public void Init(Brick brick)
    {
        this.Brick = brick;
        
    }

    public void LateUpdate()
    {
        var direction = Properties.GetBrickMovementDirection(Brick.Side).normalized;
        Grounded = Trace(direction);
    }

    private bool Trace(Vector2 direction)
    {
        var origin = Brick.transform.position +
            (new Vector3(direction.x, direction.y, 0) * (Properties.BrickSize.y / 2));

        RaycastHit2D[] hits = Physics2D.RaycastAll(
            origin,
            direction,
            0.2f);                

        foreach (var hit in hits)
        {
            if (hit.collider == null || hit.collider == Collider) continue;

            return true;
        }

        return false;
    }
}
