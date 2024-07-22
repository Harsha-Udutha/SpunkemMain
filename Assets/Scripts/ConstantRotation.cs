using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    private Quaternion rotation;

    void Start()
    {
        rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, -50 * Time.deltaTime);
    }

    public void ResetRotation()
    {
        transform.rotation = Quaternion.Euler(-80, 0, 0);
    }
}
