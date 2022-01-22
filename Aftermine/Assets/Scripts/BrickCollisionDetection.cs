using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BrickCollisionDetection : MonoBehaviour
{
    public Brick Brick;
    public BrickCollisionEvent OnCollision;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollision?.Invoke(Brick, collision);
    }

    private void Trace()
    {
        //Physics2D.BoxCast(transform.position,  )
    }
}
