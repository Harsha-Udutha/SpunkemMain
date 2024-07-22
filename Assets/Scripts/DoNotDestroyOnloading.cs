using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroyOnloading : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
