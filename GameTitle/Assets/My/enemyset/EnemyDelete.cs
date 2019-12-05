using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDelete : MonoBehaviour
{
    public float
        distance;

    private GameObject
       player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (distance < Vector3.Distance(player.transform.position, transform.position))
            Destroy(gameObject);
    }
}
