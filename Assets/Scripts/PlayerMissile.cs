using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissile : MonoBehaviour
{
    public float Speed = 50f;
    public float Damage;
    public GameObject blastPrefab;
    private RocketLauncher _rl;
    public float explosionRadius=10,explosionForce=1000;

    // Start is called before the first frame update
    void Start()
    {
        _rl = GameObject.FindWithTag("Player").GetComponent<RocketLauncher>();
        if (_rl == null)
        {
            print("_rl is null");
        }
        transform.LookAt(_rl.GetEnemycurrentPos());

        Destroy(this.gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            if (other.transform.name == "EnemyShip(Clone)" || other.transform.name == "EnemyShip2(Clone)")
            {
                other.transform.GetComponent<EnemyShipCombat>().EnemyDamage(Damage);
            }
            else
            {
                other.transform.GetComponent<LaserBeam>().EnemyDamage(Damage);
            }

            Collider[] colliders = Physics.OverlapSphere(transform.position,explosionRadius);
            foreach (Collider obj in colliders)
            {
                Rigidbody rb=obj.GetComponent<Rigidbody>();
                if(rb!=null)
                {
                    rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                }

                EnemyShipCombat esc = obj.GetComponent<EnemyShipCombat>();
                if(esc!=null)
                {
                    esc.EnemyDamage(50);
                }
            }
            GameObject Blast = Instantiate(blastPrefab, transform.position, Quaternion.identity) as GameObject;
            Destroy(gameObject);
        }
        if (other.transform.CompareTag("Astroid"))
        {
            GameObject Blast = Instantiate(blastPrefab, transform.position, Quaternion.identity) as GameObject;
            Destroy(gameObject);
        }
    }
}
