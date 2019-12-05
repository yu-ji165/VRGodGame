using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pull : MonoBehaviour
{
    //public float speed;
    public Vector3 target;
    public OVRInput.Controller lr;
    private GameObject player;
    private Vector3 move;
    public bool HitPull
    {
        get; set;
    }
    public Vector3 PullVec
    {
        get; set;
    }
    // Start is called before the first frame update
    void Start()
    {
        HitPull = false;
        PullVec = Vector3.zero;
        player = GameObject.FindWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        //if (moveFlig && LRController.NowWier == GetComponent<Controller>().lr)
        //    player.transform.position += move.normalized * speed;
    }
    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == GetComponent<Controller>().lr.ToString())
        {
            HitPull = true;
            PullVec = Vector3.Normalize(other.gameObject.transform.position - transform.position);
            Destroy(other.gameObject);
        }
    }
}