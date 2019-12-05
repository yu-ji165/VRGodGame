using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneControl : MonoBehaviour
{
    enum ENEMY_MODE
    {
        idle = 0,
        patrol,
        chase,
        max
    }
    public float moveAIRange;
    public Material
        normalMaterial,
        dangerMaterial;

    private Drone droneMove;
    private GameObject player;
    private Renderer enemyRender;
    private ENEMY_MODE enemyMode = ENEMY_MODE.max;
    // Start is called before the first frame update
    void Start()
    {
        droneMove = GetComponent<Drone>();
        enemyRender = transform.GetChild(0).GetComponent<Renderer>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーとの距離でAIの動きを切り分ける
        //プレイヤーとの距離が離れ過ぎていたら動きを止める
        if (moveAIRange < Vector3.Distance(player.transform.position, transform.position))
        {
            enemyMode = ENEMY_MODE.patrol;
            CallMode(enemyMode);
            return;
        }

        //Rayを飛ばしてプレイヤーかどうかを判断する
        Ray enemyRay =
            new Ray(transform.position, (player.transform.position - transform.position).normalized);
        Debug.DrawRay(enemyRay.origin, enemyRay.direction * Vector3.Distance(player.transform.position, transform.position), Color.red);

        RaycastHit hitData;
        if (Physics.Raycast(enemyRay, out hitData, Vector3.Distance(player.transform.position, transform.position)))
        {
            switch (hitData.collider.tag)
            {
                case "Player":
                    transform.LookAt(player.transform.position);
                    enemyMode = ENEMY_MODE.chase;
                    break;
                case "Cube":
                    enemyMode = ENEMY_MODE.patrol;
                    break;
                case "Enemy":
                    break;
                default:
                    break;
            }
        }
        CallMode(enemyMode);
    }
    private void CallMode(ENEMY_MODE mode)
    {
        switch (mode)
        {
            case ENEMY_MODE.idle:
                enemyRender.material = normalMaterial;
                droneMove.isRol = false;
                droneMove.Idle();
                break;
            case ENEMY_MODE.chase:
                enemyRender.material = dangerMaterial;
                droneMove.isRol = false;
                droneMove.Chase();
                break;
            case ENEMY_MODE.patrol:
                enemyRender.material = normalMaterial;
                droneMove.Patrol();
                break;
            default:
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Route" && enemyMode == ENEMY_MODE.patrol)
        {
            transform.rotation = other.transform.rotation;
            droneMove.Stop();
            droneMove.oldRouteObj = other.gameObject;
        }
    }
}
