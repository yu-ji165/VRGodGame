using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cm : MonoBehaviour
{
    public Transform verRot;
    public Transform horRot;

    // Use this for initialization
    void Start()
    {

        verRot = transform.parent;
        horRot = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float Z_Rotation = Input.GetAxis("Mouse X");
        float Y_Rotation = Input.GetAxis("Mouse Y");
        verRot.transform.Rotate(0, -Z_Rotation, 0);
        horRot.transform.Rotate(Y_Rotation, 0, 0);
    }
}
