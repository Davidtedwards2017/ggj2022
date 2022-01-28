using gamedev.utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineClearSequence : MonoBehaviour
{
    [ReadOnly]
    public List<Brick> bricks;

    public float Duration = 0.5f;

    public void Init(List<Brick> bricks)
    {
        this.bricks = new List<Brick>(bricks);
        StartCoroutine(Sequence());
    }

    private IEnumerator Sequence()
    {
        foreach (var brick in bricks)
        {
            brick.RequestClearStart();
        }

        yield return new WaitForSeconds(Duration);

        foreach (var brick in bricks)
        {
            brick.RequestClearEnd();
        }

        Destroy(gameObject);
    }
    

}
