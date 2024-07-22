using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid : MonoBehaviour
{
    private Transform player;
    public float AstroidOffset = 750f;
    public float MaxSpawnpoint = 1000f;
    public float replForce = 100f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        transform.rotation = Random.rotation;
    }

    // Update is called once per frame
    void Update()
    {

        if(player!=null)
        {
            if(Vector3.Distance(player.position,transform.position)>MaxSpawnpoint)
            {
                Vector3 spawnPoint = player.transform.GetComponent<PlayerShipController>().GetEnemySpawnPoint(AstroidOffset);
                this.transform.position = spawnPoint;
            }
        }
        else
        {
            return;
        }
    }
}
