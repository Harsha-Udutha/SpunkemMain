using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    public float Speed = 50f;
    private Transform player;
    public float Damage;
    public GameObject hitMarkerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        if (player != null)
        {
            transform.LookAt(player);
        }
        else
        {
            print("Player is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Speed * Time.deltaTime;
        Destroy(gameObject, 10f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            player.GetComponent<PlayerCombatController>().PlayerDamage(Damage);
            GameObject hitMarker = Instantiate(hitMarkerPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if(other.transform.CompareTag("Astroid"))
        {
            GameObject hitMarker = Instantiate(hitMarkerPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
