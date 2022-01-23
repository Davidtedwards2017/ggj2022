using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListExtensions
{
    public static T PickRandom<T>(this List<T> collection)
    {
        var index = Random.Range(0, collection.Count);
        return collection[index];
    }

}
