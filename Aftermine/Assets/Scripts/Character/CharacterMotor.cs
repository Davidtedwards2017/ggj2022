using gamedev.utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using System;

public class CharacterMotor : MonoBehaviour
{
    public Transform root;
    public CharacterController controller;

    public Action OnCompletedMovement;

    private GlobalPropertiesSO.Character characterProperties => controller.globalProperties.character;

    public float JumpHeight => characterProperties.MovementJumpHeight;
    public float MoveDuration => characterProperties.MovementDuration;
    public float FloorOffset => characterProperties.FloorPositionOffset;
    public float BrickOffset => characterProperties.BrickPositionOffset;

    [ReadOnly]
    public int column;

    private Side side => controller.side;

    public void PerformMove(int column)
    {
        this.column = column;

        var direction = controller.globalProperties.GetBrickMovementDirection(side);

        var brick = TraceForBrick(direction);
        if (brick != null)
        {
            MoveOnAboveBrick(brick);
        }
        else
        {
            MoveToFloor();
        }
    }


    private void MoveOnAboveBrick(Brick brick)
    {
        var grid = controller.gridObject.Grid;

        var x = grid.GetXPosition(column);

        var offset = controller.globalProperties.BrickSize.x + BrickOffset;
        if (side == Side.Lower) offset *= -1;

        var y = brick.transform.position.y + offset;

        var destination = new Vector2(x, y);

        root.transform.DOJump(destination, JumpHeight, 1, MoveDuration)
            .OnComplete(CompletedMovement);
    }

    private void MoveToFloor()
    {
        var grid = controller.gridObject.Grid;

        var x = grid.GetXPosition(column);

        var offset = FloorOffset;
        if (side == Side.Lower) offset *= -1;
        var y = grid.transform.position.y + offset;

        var destination = new Vector2(x, y);
        root.transform.DOJump(destination, JumpHeight, 1, MoveDuration)
            .OnComplete(CompletedMovement);
    }

    private void CompletedMovement()
    {
        OnCompletedMovement?.Invoke();
    }

    private Brick TraceForBrick(Vector2 direction)
    {
        var grid = controller.gridObject.Grid;
        var distance = (grid.Height / 2);

        var x = grid.GetXPosition(column);
        var y = grid.transform.position.y + ((side == Side.Upper) ? distance : - distance);

        var origin = new Vector2(x, y);

        RaycastHit2D[] hits = Physics2D.RaycastAll(origin, direction, distance);

        foreach (var hit in hits)
        {
            if (hit.collider == null) continue;

            var brick = hit.collider.GetComponentInParent<Brick>();
            if (brick != null && brick.AtRest())
            {
                return brick;   
            }
        }

        return null;
    }

}
