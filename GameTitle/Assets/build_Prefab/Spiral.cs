using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiral : MonoBehaviour
{
    public GameObject obj;
    public int objMax;
    public float distance;
    public float height;
    public float angleOne;
    // Start is called before the first frame update
    void Start()
    {
        float heightOne = height / objMax;
        for (int i = 0; i < objMax; i++)
        {
            GameObject a;
            a = Instantiate(obj,
                new Vector3(
                distance * Mathf.Cos(i * angleOne * Mathf.Deg2Rad),
                500 + i * heightOne,
                2000 + distance * Mathf.Sin(i * angleOne * Mathf.Deg2Rad)),
                Quaternion.identity);
            a.transform.localRotation = Quaternion.LookRotation(
                a.transform.position - new Vector3(
                distance * Mathf.Cos((i + 1) * angleOne * Mathf.Deg2Rad),
                500 + (i + 1) * heightOne,
                2000 + distance * Mathf.Sin((i + 1) * angleOne * Mathf.Deg2Rad)),
                Vector3.forward);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
