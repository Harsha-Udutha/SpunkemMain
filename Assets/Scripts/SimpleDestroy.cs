using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDestroy : MonoBehaviour
{
    public float destroyIn = 2;
    private void Update()
    {
        Destroy(this.gameObject, destroyIn);
    }
}
