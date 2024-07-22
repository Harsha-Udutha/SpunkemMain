using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthRegenaration : MonoBehaviour
{
    PlayerCombatController pcc;
    RocketLauncher rl;

    // Start is called before the first frame update
    void Start()
    {
        pcc = GetComponent<PlayerCombatController>();
        rl = GetComponent<RocketLauncher>();
        StartCoroutine(HealthGenRoutine());
    }

    IEnumerator HealthGenRoutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(120, 180));
            pcc.HealthGen();
            rl.AmmoGen();
        }
    }
}
