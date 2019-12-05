using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemymove : MonoBehaviour
{
    public float
        speed,
        jumpPower,
        jumpTime;

    private GameObject 
        player;
    private const float
        gravity = 9.8f;
    private float
        gravityTime = 0;
    private bool
        isJump = false;
    private Rigidbody
        enemyRB;
    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.LookAt(player.transform);

        if (isJump)
        {
            gravityTime += Time.deltaTime;
            enemyRB.useGravity = true;
            if (jumpTime < gravityTime)
            {
                isJump = false;
                enemyRB.velocity = Vector3.zero;
                enemyRB.useGravity = false;
                gravityTime = 0.0f;
            }
        }
        else
        {
            enemyRB.AddForce(transform.up * jumpPower, ForceMode.VelocityChange);
            enemyRB.AddForce(transform.forward * speed, ForceMode.VelocityChange);
            isJump = true;
        }
    }
}
