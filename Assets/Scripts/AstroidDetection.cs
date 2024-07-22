using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidDetection : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Player"))
        {
            print("Detected player");
            //other.transform.GetComponent<PlayerCombatController>().PlayerDestroy();
        }
    }
}
