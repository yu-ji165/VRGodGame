using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public float 
        safeRange;
    public GameObject[]
        enemyObject;
    private Vector3
        spawnPoint;
    private float 
        enemyCollider;

    // Start is called before the first frame update
    void Start()
    {
        enemyCollider = GetComponent<SphereCollider>().radius;
    }
    private void OnTriggerEnter(Collider enterCollider)
    {
        if (enterCollider.tag == "Player")
        {
            do
            {
                spawnPoint = new Vector3(
                transform.position.x + Random.Range(-enemyCollider, enemyCollider),
                transform.position.y + Random.Range(-enemyCollider, enemyCollider),
                transform.position.z + Random.Range(-enemyCollider, enemyCollider));
            } while (safeRange > Vector3.Distance(spawnPoint, enterCollider.transform.position));
            Instantiate(enemyObject[0],spawnPoint,Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
