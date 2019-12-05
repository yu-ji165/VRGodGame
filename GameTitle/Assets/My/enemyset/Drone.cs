using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public Vector3
        moveSpeed;
    public float
        deceleration = 0.99f,
        waitTimer,
        moveTimer,
        rolMoveTimer,
        killerRange;
    public bool
        isRol = false;
    public GameObject
        oldRouteObj;

    private Rigidbody
        droneRB;
    private GameObject
        player;
    private bool
        isDroneMove = false;
    private float
        waitTimeStock,
        moveTimeStock,
        moveSwitch/*Right:1 Left:-1*/,
        rolTimer;
    private Vector3
        targetX,
        targetZ;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        droneRB = GetComponent<Rigidbody>();
        moveSwitch = 1;
        waitTimeStock = waitTimer;
        moveTimeStock = moveTimer;
    }
    public void Idle()
    {
        if (!droneRB.IsSleeping())
            droneRB.velocity *= deceleration;
    }
    public void Chase()
    {
        //待ち時間
        if (!isDroneMove)
        {
            if (waitTimer > 0)
            {
                waitTimer -= Time.deltaTime;
                droneRB.velocity *= deceleration;
                return;
            }
            else
            {
                waitTimer = waitTimeStock;
                isDroneMove = true;
                targetX = transform.right * moveSwitch;
                targetZ = transform.forward;
                droneRB.velocity = Vector3.zero;
            }
        }
        //動き出すよ
        else
        {
            moveTimer -= Time.deltaTime;
            if (moveTimer > 0)
            {
                //プレイヤーに向かって突っ込む
                if (Vector3.Distance(player.transform.position, transform.position) < killerRange)
                    droneRB.AddForce(transform.forward * moveSpeed.z, ForceMode.Acceleration);
                //プレイヤーの周りを左右に迂回しながら近づく
                else
                {
                    droneRB.AddForce(targetX * moveSpeed.x, ForceMode.Acceleration);
                    droneRB.AddForce(targetZ * moveSpeed.z, ForceMode.Acceleration);
                }
                if (player.transform.position.y > transform.position.y)
                    droneRB.AddForce(Vector3.up * moveSpeed.y, ForceMode.Acceleration);
                else if (player.transform.position.y < transform.position.y)
                    droneRB.AddForce(Vector3.down * moveSpeed.y, ForceMode.Acceleration);
            }
            else
            {
                moveTimer = moveTimeStock;
                isDroneMove = false;

                //反転
                moveSwitch *= Mathf.Pow(-1, Random.Range(1, 3));
            }
        }
    }
    public void Patrol()
    {
        if (isRol)
        {
            if (droneRB.velocity.magnitude <= moveSpeed.z / 2)
            {
                droneRB.AddForce(transform.forward * moveSpeed.z / 5, ForceMode.VelocityChange);
            }
            rolTimer += Time.deltaTime;
            if (rolTimer > rolMoveTimer)
            {
                rolTimer = 0;
                isRol = false;
            }
        }
        else
        {
            Stop();
            GameObject nearRouteObj;
            nearRouteObj = SserchRoute();
            transform.LookAt(nearRouteObj.transform.position);
            isRol = true;
        }
    }
    public void Stop()
    {
        droneRB.velocity = Vector3.zero;
        rolTimer = 0;
    }
    private GameObject SserchRoute()
    {
        GameObject routeObj = null;
        float minDis = 0.0f;
        float stockDis = 0.0f;
        foreach (GameObject obs in GameObject.FindGameObjectsWithTag("Route"))
        {
            if (obs == oldRouteObj)
                continue;
            //自身と取得したオブジェクトの距離を取得
            stockDis = Vector3.Distance(obs.transform.position, transform.position);

            //オブジェクトの距離が近いか、距離0であればオブジェクト名を取得
            //一時変数に距離を格納
            if (minDis == 0 || minDis > stockDis)
            {
                minDis = stockDis;
                //nearObjName = obs.name;
                routeObj = obs;
            }
        }
        return routeObj;
    }
}
