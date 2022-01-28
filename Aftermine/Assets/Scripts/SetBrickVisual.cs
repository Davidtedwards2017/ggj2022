using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBrickVisual : MonoBehaviour
{
    public SpriteRenderer Renderer;

    public void SetVisual(BrickType type)
    {
        if (type == null)
        {
            return;
        }

        Renderer.sprite = type.sprites.PickRandom();
    }
}
