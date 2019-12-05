using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : CreateParent
{
    public float defSplitDistance;
    public LineRenderer line;
    private int oldCreateCount;
    private List<GameObject> children;
    private List<Vector3> vertexPos;
    private List<Vector3> linePos;
    protected override bool IfUpdata()
    {
        return oldCreateCount != createCount;
    }
    protected override void Create()
    {
        children = new List<GameObject>();
        vertexPos = new List<Vector3>();
        linePos = new List<Vector3>();
        foreach (Transform child in transform)
        {
            //children.Add(child.gameObject);
            vertexPos.Add(child.gameObject.transform.position);
        }
        for (int i = 0; i < vertexPos.Count - 1; i++)
        {
            float distance = Vector3.Distance(vertexPos[i + 1], vertexPos[i]);
            if (defSplitDistance <= 0)
                defSplitDistance = 1f;
            int split = (int)(distance / defSplitDistance);
            for (int s = 0; s < split; s++)
            {
                float t = s / (float)split;
                linePos.Add(CatmullRom(vertexPos, i, t));
            }
            linePos.Add(vertexPos[i + 1]);
        }
        float sumDistance = 0;
        for (int i = 0; i < linePos.Count - 1; i++)
        {
            sumDistance += Vector3.Distance(linePos[i], linePos[i + 1]);
        }
        float createDistance = sumDistance / (float)createCount;
        float createSumDistance = 0;
        for (int i = 0; i < linePos.Count - 1; i++)
        {
            createSumDistance += Vector3.Distance(linePos[i], linePos[i + 1]);
            if (createSumDistance > createDistance)
            {
                objList.Add(Instantiate(obj, linePos[i], Quaternion.identity));
                objList[objList.Count - 1].transform.LookAt(linePos[i + 1]);
                createSumDistance -= createDistance;
            }
        }
        line.positionCount = linePos.Count;
        line.SetPositions(linePos.ToArray());
        oldCreateCount = createCount;
    }
    private Vector3 CatmullRom(List<Vector3> list, int count, float t)
    {
        float t2 = t * t;
        float t3 = t2 * t;
        float c0 = -0.5f * t3 + t2 - 0.5f * t;
        float c1 = 1.5f * t3 - 2.5f * t2 + 1.0f;
        float c2 = -1.5f * t3 + 2.0f * t2 + 0.5f * t;
        float c3 = 0.5f * t3 - 0.5f * t2;

        if (count == 0)
            return list[count] * c0 + list[count] * c1 + list[count + 1] * c2 + list[count + 2] * c3;
        else if (count + 2 == list.Count)
            return list[count - 1] * c0 + list[count] * c1 + list[count + 1] * c2 + list[count + 1] * c3;
        else
            return list[count - 1] * c0 + list[count] * c1 + list[count + 1] * c2 + list[count + 2] * c3;
    }
}