using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerator : MonoBehaviour
{
    public GameObject cloud;
    public int generatCount;
    public Vector3 minRange;
    public Vector3 maxRange;
    private int cou;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        cou = 0;
        player = GameObject.FindWithTag("Player");
        for (int i = 0; i < generatCount; i++)
        {
            Vector3 vec = new Vector3(
                Random.Range(minRange.x, maxRange.x),
                Random.Range(minRange.y, maxRange.y),
                Random.Range(minRange.z, maxRange.z));
            Instantiate(cloud, vec, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}