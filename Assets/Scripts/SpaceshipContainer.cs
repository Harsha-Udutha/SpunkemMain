using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipContainer : MonoBehaviour
{
    int max;
    // Start is called before the first frame update
    void Start()
    {
        max = transform.childCount;

    }

    public void UpdateRotation(int index)
    {
        for(int i=0; i<max;i++)
        {
            Transform c = transform.GetChild(i);
            c.GetComponent<ConstantRotation>().ResetRotation();
            c.GetComponent<ConstantRotation>().enabled = false;
        }
        Transform child = transform.GetChild(index);
        child.GetComponent<ConstantRotation>().enabled = true;
    }
}
