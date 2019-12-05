using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetPoint : MonoBehaviour
{
    struct UDLR
    {
        public int pm;
        public float count;
        public float move;
    }
    public float period;//秒
    public Vector2 distance;
    public Vector2 one_ud_lr;
    private GameObject center;
    private GameObject enemy;
    private float ang;
    private float oneCount;
    private UDLR ud;
    private UDLR lr;
    // Start is called before the first frame update
    void Start()
    {
        center = transform.parent.gameObject;
        enemy = GameObject.FindWithTag("BossEnemy");
        ang = 0;
        oneCount = 360 / (period * 120);
        //lr = Vector3.left;
        ud.pm = 1;
        ud.count = 0;
        ud.move = 0;
        lr.pm = 1;
        lr.count = 0;
        lr.move = 0;
    }
    void UDLRUpdata(ref UDLR udlr, Vector2 ul)
    {
        udlr.count += (one_ud_lr.x * ul.x + one_ud_lr.y * ul.y) * udlr.pm;
        udlr.move += udlr.count;
        if (udlr.move > (distance.x * ul.x + distance.y * ul.y))
            udlr.pm = -1;
        else if (udlr.move < -(distance.x * ul.x + distance.y * ul.y))
            udlr.pm = 1;
    }

    // Update is called once per frame
    void Update()
    {
        UDLRUpdata(ref ud, Vector2.up);
        UDLRUpdata(ref lr, new Vector2(1, 0));
        transform.position =
            center.transform.position +
            center.transform.TransformDirection(Vector3.up) * ud.move +
            center.transform.TransformDirection(Vector3.left) * ud.move;

        //ang += oneCount;
        //transform.LookAt(enemy.transform.position);
        //transform.position = center.transform.position + center.transform.TransformDirection(Vector3.up) * Mathf.Sin(Mathf.Deg2Rad * ang) * distance * (int)(ang / 360 + 1);
        //if (ang > 720)
        //{
        //    ang -= 720;
        //}
        //if (ang >= 360)
        //{
        //    lr.x *= -1;
        //    ang -= 360;
        //}
        //transform.position = center.transform.position + center.transform.TransformDirection(lr) * distance;
    }
}