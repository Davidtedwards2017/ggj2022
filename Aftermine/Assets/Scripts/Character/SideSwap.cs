using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideSwap : MonoBehaviour
{
    public CharacterController controller;
    public SideEvent OnSideUpdated;

    public void SwapSide()
    {
        OnSideUpdated?.Invoke(controller.side.Flip());
    }
}
