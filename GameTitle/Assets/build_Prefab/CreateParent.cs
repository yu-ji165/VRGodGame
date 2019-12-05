using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateParent : MonoBehaviour
{
    public GameObject obj;
    public int createCount;
    protected List<GameObject> objList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        Create();
    }

    // Update is called once per frame
    void Update()
    {
        if (IfUpdata())
            Create();
    }
    protected virtual void Create()
    {

    }
    protected virtual bool IfUpdata()
    {
        return false;
    }
    protected virtual void ListReset()
    {
        for (int i = 0; i < objList.Count; i++)
            Destroy(objList[i]);
        objList = new List<GameObject>();
    }
}
