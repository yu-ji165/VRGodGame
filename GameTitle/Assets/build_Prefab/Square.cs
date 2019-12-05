using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : CreateParent
{
    public GameObject item;
    public int itemCount;
    private int discount;
    private int oldCreateCount;
    protected override bool IfUpdata()
    {
        return oldCreateCount != createCount;
    }
    protected override void Create()
    {
        ListReset();
        discount = 2 * 2 * 7 * 6 - createCount;
        if (discount < 0)
        {
            discount = 2;
        }
        for (int z2 = 1; z2 >= -1; z2 -= 2)//1と-1をだす
        {
            for (int y = 0; y < 2; y++)
            {
                for (int x = 0; x < 7; x++)
                {
                    for (int z = 0; z < 6; z++)
                    {
                        Vector3 a = transform.TransformDirection(Vector3.left) * (-105f + x * 35f);
                        Vector3 b = transform.TransformDirection(Vector3.up) * (y * 60);
                        Vector3 c = transform.TransformDirection(Vector3.forward) * (z2 * (45 + z * 25));
                        Vector3 abc = a + b + c;
                        objList.Add(Instantiate(obj,transform.position + abc, transform.rotation));
                    }
                }
            }
        }
        for (int i = 0; i < discount; i++)
        {
            int destroy = Random.Range(0, objList.Count - 1);
            if (i < itemCount)
                Instantiate(item,
                    new Vector3(objList[destroy].transform.position.x, objList[destroy].transform.position.y + 30, objList[destroy].transform.position.z),
                    objList[destroy].transform.rotation);
            Destroy(objList[destroy]);
            objList.RemoveAt(destroy);
        }
        oldCreateCount = createCount;
    }
}