using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float distance;
    public float outDistance;
    private GameObject player;
    private GameObject ETarget;
    private GameObject pCamera;
    private bool turning;
    private bool turningFinish;
    private Vector3 center;
    private Transform turningTrans;
    private Vector3 turningPos;
    private Quaternion turningRota;
    private int turningCou;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        ETarget = GameObject.FindWithTag("ETarget");
        pCamera = GameObject.FindWithTag("MainCamera");
        turning = false;
        turningFinish = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerHit>().Finishd == PlayerHit.Finish.no)
        {
            Vector3 target;

            if (Mathf.Abs(Vector3.Distance(player.transform.position, transform.position)) > distance)
            {
                target = ETarget.transform.position;
                transform.LookAt(target);
                transform.position += transform.TransformDirection(Vector3.forward) * speed;
            }
            else
            {
                target = player.transform.position;
                Vector3 targetDir = target - transform.position;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 0.7f * Time.deltaTime, 0f);
                transform.rotation = Quaternion.LookRotation(newDir);
                transform.position += transform.TransformDirection(Vector3.forward) * speed;
            }
            //if (Mathf.Abs(Vector3.Distance(player.transform.position, transform.position)) <= distance && !turning)
            //{
            //    turning = true;
            //    turningTrans = transform;
            //    center = turningTrans.TransformDirection(Vector3.left) * 3;
            //    turningCou = 0;
            //}
            //else if (Mathf.Abs(Vector3.Distance(player.transform.position, transform.position)) >= outDistance)
            //{
            //    turning = false;
            //    turningFinish = false;
            //}
            //if (!turning || turningFinish)
            //    transform.LookAt(player.transform);
        }
    }
}