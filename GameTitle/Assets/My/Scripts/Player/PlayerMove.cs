using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float deceleration;
    private int cou;
    public Vector3 MoveVec
    {
        get; set;
    }
    public float Speed
    {
        get; set;
    }
    public Vector3 LMove
    {
        get; set;
    }
    public Vector3 RMove
    {
        get; set;
    }
    public bool LSimultaneous
    {
        get; set;
    }
    public bool RSimultaneous
    {
        get; set;
    }
    // Start is called before the first frame update
    void Start()
    {
        MoveVec = Vector3.zero;
        Speed = 0;
        LMove = Vector3.zero;
        RMove = Vector3.zero;
        LSimultaneous = false;
        RSimultaneous = false;
        cou = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (LSimultaneous && RSimultaneous)
        {
            MoveVec = LMove + RMove;
            MoveVec *= 0.75f;
            LSimultaneous = false;
            RSimultaneous = false;
            cou = 0;
            SEClip.CallSe(gameObject, SEClip.SE_NAME.accelerator, GetComponent<PlayerMove>().MoveVec);
        }
        if (LSimultaneous || RSimultaneous)
        {
            cou++;
            if (cou >= 20)
            {
                LSimultaneous = false;
                RSimultaneous = false;
                cou = 0;
            }
        }
        transform.position += MoveVec * Speed;
        Speed /= deceleration;
        if (Speed < 0)
        {
            Speed = 0;
        }
    }
}