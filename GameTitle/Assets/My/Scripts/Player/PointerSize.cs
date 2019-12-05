using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerSize : MonoBehaviour
{
    public float maxDistance;
    private Vector3 defSize;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void SizeUpDate(Vector3 playerPos)
    {
        defSize = transform.localScale;
        float distance = Vector3.Distance(playerPos, transform.position);
        float changeSize;
        if (distance < maxDistance)
        {
            changeSize = (distance / maxDistance);
        }
        else
        {
            changeSize = 1;
        }
        transform.localScale = defSize * changeSize;
    }
}