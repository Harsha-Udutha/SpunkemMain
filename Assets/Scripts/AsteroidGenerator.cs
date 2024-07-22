using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGenerator : MonoBehaviour
{
    public float spawnRange;
    public float amountToSpawn;
    private Vector3 spawnPoint;
    public GameObject[] asteroid;
    public float startSafeRange;
    private List<GameObject> objectsToPlace = new List<GameObject>();
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        Spwan();
        //asteroid.SetActive(false);
    }

    public void Spwan()
    {
        for (int i = 0; i < amountToSpawn; i++)
        {
            //PickSpawnPoint();
            spawnPoint = player.position;

            //pick new spawn point if too close to player start
            while (Vector3.Distance(spawnPoint, Vector3.zero) < startSafeRange)
            {
                PickSpawnPoint();
            }
            int index = Random.Range(0, asteroid.Length);
            objectsToPlace.Add(Instantiate(asteroid[index], spawnPoint, Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f)))as GameObject);
            objectsToPlace[i].transform.parent = this.transform;
        }
    }

    public void PickSpawnPoint()
    {
        spawnPoint = new Vector3(
            Random.Range(-1f,1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f));

        if(spawnPoint.magnitude > 1)
        {
            spawnPoint.Normalize();
        }

        spawnPoint *= spawnRange;
    }
}

