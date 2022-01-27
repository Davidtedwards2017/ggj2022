using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float Duration = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destroy", Duration);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }


}
