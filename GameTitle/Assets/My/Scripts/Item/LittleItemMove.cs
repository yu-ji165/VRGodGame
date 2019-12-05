using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleItemMove : MonoBehaviour
{
    private int cou;
    private GameObject player;
    public float Speed
    {
        get; set;
    }
    public Vector3 MoveVec
    {
        get; set;
    }
    public int MoveTime
    {
        get; set;
    }
    public bool Last
    {
        get; set;
    }
    // Start is called before the first frame update
    void Start()
    {
        //MoveTime = 60;
        cou = 0;
        //Last = false;
        player = GameObject.FindWithTag("PlayerHand");
    }

    // Update is called once per frame
    void Update()
    {
        if (cou < MoveTime)
        {
            transform.position += MoveVec * Speed;
            GetComponent<LineRenderer>().SetPosition(0, transform.position);
            GetComponent<LineRenderer>().SetPosition(1, transform.position);
        }
        else if (cou - MoveTime < MoveTime)
        {
            //GetComponent<MeshRenderer>().enabled = false;
            Vector3 distance = player.transform.position - transform.position;
            GetComponent<LineRenderer>().SetPosition(0, transform.position);
            GetComponent<LineRenderer>().SetPosition(1, transform.position + distance / (MoveTime) * (cou - MoveTime));
        }
        else if (cou - MoveTime - MoveTime < MoveTime)
        {
            Vector3 distance = player.transform.position - transform.position;
            GetComponent<LineRenderer>().SetPosition(0, transform.position + distance / (MoveTime) * (cou - MoveTime - MoveTime));
            GetComponent<LineRenderer>().SetPosition(1, player.transform.position);
        }
        else
        {
            if (Last)
            {
                player.GetComponent<Controller>().ItemCount++;
            }
            Destroy(gameObject);
        }
        cou++;
    }
}
