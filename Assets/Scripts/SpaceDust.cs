using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceDust : MonoBehaviour
{
    private Transform player;
    [SerializeField] float radious = 100;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player!=null)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if(distance>radious)
            {
                transform.position = Vector3.Lerp(transform.position, player.position, 10);
            }
        }
    }
}
