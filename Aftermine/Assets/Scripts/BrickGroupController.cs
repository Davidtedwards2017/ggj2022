using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickGroupController : MonoBehaviour
{
    public List<BrickGroup> BrickGroups;

    private void Awake()
    {
        BrickGroups.Clear();
    }

    public void AddBrickGroup(BrickGroup group)
    {
        BrickGroups.Add(group);
    }

    public void RemoveBrickGroup(BrickGroup group)
    {
        BrickGroups.Remove(group);
    }

    public void Refresh()
    {

    }
}
