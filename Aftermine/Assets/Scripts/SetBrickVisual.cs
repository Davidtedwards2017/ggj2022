using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBrickVisual : MonoBehaviour
{
    public SpriteRenderer Renderer;

    public void SetVisual(BrickType type)
    {
        Renderer.sprite = type.sprite;
    }
}
