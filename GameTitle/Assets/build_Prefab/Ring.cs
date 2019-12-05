using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : CreateParent
{
    public Vector3 scale;
    public float distance;
    private int oldCreateCount;
    private float oldDistance;
    private Vector3 oldScale;
    protected override bool IfUpdata()
    {
        return oldCreateCount != createCount || oldDistance != distance || oldScale != scale;
    }
    protected override void Create()
    {
        ListReset();
        float oneAng = 360 / createCount;
        for (int i = 0; i < createCount; i++)
        {
            objList.Add(Instantiate(obj, transform.position + new Vector3(Mathf.Cos(i * oneAng * Mathf.Deg2Rad), 0, Mathf.Sin(i * oneAng * Mathf.Deg2Rad)) * distance, Quaternion.identity));
            objList[objList.Count - 1].transform.localScale = scale;
        }
        oldCreateCount = createCount;
        oldDistance = distance;
        oldScale = scale;
    }
}
