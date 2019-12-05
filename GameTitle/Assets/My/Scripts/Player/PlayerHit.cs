using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    public enum Finish { clear, gameOver, no, sleep }
    public Finish Finishd
    {
        get; set;
    }
    // Start is called before the first frame update
    void Start()
    {
        Finishd = Finish.no;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Enemy"))
        {
            Finishd = Finish.gameOver;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Cube")
        {
            other.gameObject.tag = "HitCube";
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "HitCube")
        {
            other.gameObject.tag = "Cube";
        }
    }
}