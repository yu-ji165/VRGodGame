using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public bool hit;
    public Color color;
    public Material defcol;
    private int cou;
    public Vector3 GrabbablePos
    {
        get; set;
    }
    // Start is called before the first frame update
    void Start()
    {
        hit = false;
        cou = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (cou < 3)
        {
            cou++;
        }
        if (hit)
        {
            meshRenderer.material.color = color;
            cou = 0;
            hit = false;
        }
        else if (cou >= 3)
        {
            //meshRenderer.material = defcol;
            cou++;
        }
    }
}