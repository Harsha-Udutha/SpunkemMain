using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private Transform player;
    public GameObject[] enemyPrefabs;
    private float _spwanRate = 3.0f;
    private float _canSpawn = -1f;
    public float EnemyOffset = 50f;
    public int maxSpawns = 30;
    public float minSpawnTime = 2, maxSpawnTime = 15;
    public int maxBound = 1;

    //Enemy settings.
    public float eSpeed = 30, lSpeed = 50;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        }
        catch(System.Exception e)
        {
            print(e.ToString());
        }

    }

    // Update is called once per frame
    void Update()
    {
        int childCount = transform.childCount;

        if(maxSpawnTime<3)
        {
            maxSpawnTime = 3;
        }

        if (Time.time > _canSpawn && childCount < maxSpawns && player != null)
        {
            try
            {
                Vector3 spawnPoint = player.GetComponent<PlayerShipController>().GetEnemySpawnPoint(EnemyOffset);
                _spwanRate = Random.Range(minSpawnTime, maxSpawnTime);
                _canSpawn = Time.time + _spwanRate;
                if (maxBound > enemyPrefabs.Length)
                {
                    maxBound -= 1;
                }
                int index = Random.Range(0, maxBound);// later change according to difficulty
                GameObject enemyShip = Instantiate(enemyPrefabs[index], spawnPoint, Quaternion.identity) as GameObject;
                if (enemyShip.transform.name == "EnemyShip(Clone)" || enemyShip.transform.name == "EnemyShip2(Clone)")
                {
                    enemyShip.transform.parent = this.transform;
                    enemyShip.GetComponent<EnemyShipController>().forwardSpeed = eSpeed;
                    enemyShip.GetComponent<EnemyShipCombat>().laserSpeed = lSpeed;
                }
                else
                {
                    return;
                }
            }
            catch(System.Exception e)
            {
                print(e.ToString());
            }

        }
    }

}
