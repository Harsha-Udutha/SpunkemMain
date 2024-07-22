using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipCombat : MonoBehaviour
{
    public float health,maxHealth=100;
    private float _canFire = -1;
    private float _fireRate = 3;

    public GameObject LaserPrefab;
    public Transform LaserSpawnPos;
    public float laserSpeed = 50f;
    private Transform player;
    public float damageToPlayer = 1;
    public int ScoreToPlayer = 10;
    public GameObject blastPrefab;
    private EnemyShipController esc;

    // Start is called before the first frame update
    void Start()
    {
        if(this.transform.name== "EnemyShip2(Clone)")
        {
            maxHealth = 200;
        }
        health = maxHealth;
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        esc = GetComponent<EnemyShipController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Fire();
        }
        else
        {
            return;
        }

        if (health < 0)
        {
            EnemyDeath();
        }
    }

    void Fire()
    {
        if(Time.time>_canFire)
        {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;
            LaserPrefab.GetComponent<Laser>().Damage = damageToPlayer;
            GameObject enemyLaser = Instantiate(LaserPrefab, LaserSpawnPos.position, Quaternion.identity) as GameObject;
            enemyLaser.GetComponent<Laser>().Speed = laserSpeed;
        }
    }

    public void EnemyDamage(float Damage)
    {
        health -= Damage;
    }
    private void EnemyDeath()
    {
        player.GetComponent<PlayerCombatController>().PlayerScore(ScoreToPlayer);
        GameObject BlastPrefab = Instantiate(blastPrefab, transform.position, Quaternion.identity);
        esc.BlastImpact();
        Destroy(this.gameObject);
    }
}
