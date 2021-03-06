using gamedev.utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public ColumnSelector columnSelector;
    public CharacterController controller;


    public bool EnableInput = true;


    public void Init()
    {
        controller.Init();
    }

    public void SetInputEnabled(bool enabled)
    {
        EnableInput = enabled;
    }


    public void MovementInput(InputAction.CallbackContext value)
    {
        if (!EnableInput) return;

        if (!value.performed) return;

        var inputValue = value.ReadValue<Vector2>();
        var x = inputValue.x;

        if (x > 0) columnSelector.RequestMoveRight();
        else if (x < 0) columnSelector.RequestMoveLeft();
    }

    public void PushInput(InputAction.CallbackContext value)
    {
        if (!EnableInput) return;

        if (!value.performed) return;

        var inputValue = value.ReadValueAsButton();
        if (inputValue)
        {
            controller.RequestPerformPush();
        }
    }

    public void ChangeSideInput(InputAction.CallbackContext value)
    {
        return;

        if (!EnableInput) return;
        if (!value.performed) return;

        var inputValue = value.ReadValue<Vector2>();
        var y = inputValue.y;

        if (y > 0) controller.ChangeSide(Side.Upper);
        else if (y < 0) controller.ChangeSide(Side.Lower);

        //if (y > 0) columnSlider.RequestSlideUp();
        //else if (y < 0) columnSlider.RequestSlideDown();
    }
}
