using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCenterMove : MonoBehaviour
{
    public float speed;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(Vector3.Distance(player.transform.position, transform.position)) > 10)
        {
            //transform.LookAt(player.transform.position);
            //transform.position += transform.TransformDirection(Vector3.forward) * speed;
            Vector3 target = player.transform.position;
            Vector3 targetDir = target - transform.position;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 0.7f * Time.deltaTime, 0f);
            transform.rotation = Quaternion.LookRotation(newDir);
            transform.position += transform.TransformDirection(Vector3.forward) * speed;
        }
        //Vector3 targetDir = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z) - transform.position;
        //Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 0.7f * Time.deltaTime, 0f);
        //transform.rotation = Quaternion.LookRotation(newDir);
        //transform.position += transform.TransformDirection(Vector3.forward) * speed;
    }
}
