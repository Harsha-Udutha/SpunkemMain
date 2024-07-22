using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyShipController : MonoBehaviour
{
    //variables
    public float forwardSpeed = 20f;
    public float rotSpeed = 30f;
    public float minOffsetDistance = 10f;
    private bool doneRotating = false;
    int n;
    public float explosionRadius = 10, explosionForce = 750;
    private Rigidbody rb;

    //Referance Variables
    private Transform player;

    private void Start()
    {
        n = Random.Range(0, 3);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        StartCoroutine(StablizeShipRoutine());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player != null)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.position - transform.position), rotSpeed * Time.deltaTime);
            float distance = Vector3.Distance(player.position, transform.position);
            if (distance >= minOffsetDistance)
            {
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hitInfo, 20))
                {
                    if(hitInfo.transform.CompareTag("Astroid"))
                    {
                        switch (n)
                        {
                            case 0: transform.position += transform.up * forwardSpeed * Time.deltaTime* 1.5f; break;
                            case 1: transform.position += -transform.up * forwardSpeed * Time.deltaTime*1.5f; break;
                            case 2: transform.position += transform.right * forwardSpeed * Time.deltaTime*1.5f; break;
                            case 3: transform.position += -transform.right * forwardSpeed * Time.deltaTime*1.5f; break;
                        }
                        //transform.position += -transform.up * forwardSpeed * Time.deltaTime;
                    }
                }
                else
                {
                    //translating enemy to player
                    transform.position += transform.forward * forwardSpeed * Time.deltaTime;
                }
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10, Color.red);
            }
        }
        else
        {
            if(!doneRotating)
            {
                doneRotating = true;
                transform.rotation = Quaternion.Lerp(transform.rotation, Random.rotation,1);
            }
            transform.position += transform.forward * forwardSpeed * Time.deltaTime;
        }
    }

    public void BlastImpact()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider obj in colliders)
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            EnemyShipCombat esc = obj.GetComponent<EnemyShipCombat>();
            PlayerCombatController pcc = obj.GetComponent<PlayerCombatController>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
            if(esc!=null)
            {
                esc.EnemyDamage(Random.Range(5,50));
            }
            if(pcc!= null)
            {
                pcc.PlayerDamage(Random.Range(5,10));
            }
        }
    }

    IEnumerator StablizeShipRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3, 5));
            rb.isKinematic = true;
            yield return new WaitForSeconds(0.3f);
            rb.isKinematic = false;
        }
    }

}
