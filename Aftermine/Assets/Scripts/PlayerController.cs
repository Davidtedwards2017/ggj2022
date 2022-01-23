using gamedev.utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public ColumnSelector columnSelector;
    public ColumnSlider columnSlider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void MovementInput(InputAction.CallbackContext value)
    {
        if (!value.performed) return;

        var inputValue = value.ReadValue<Vector2>();
        var x = inputValue.x;

        if (x > 0) columnSelector.RequestMoveRight();
        else if (x < 0) columnSelector.RequestMoveLeft();
    }

    public void SlidingInput(InputAction.CallbackContext value)
    {
        if (!value.performed) return;

        var inputValue = value.ReadValue<Vector2>();
        var y = inputValue.y;

        if (y > 0) columnSlider.RequestSlideUp();
        else if (y < 0) columnSlider.RequestSlideDown();
    }
}
