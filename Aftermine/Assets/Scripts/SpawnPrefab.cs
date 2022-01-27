using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrefab : MonoBehaviour
{
    public void Spawn(GameObject prefab)
    {
        if (prefab == null) return;
        Instantiate(prefab, transform.position, Quaternion.identity);
    }
}
