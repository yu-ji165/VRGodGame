using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public GameObject HitObject
    {
        get; set;
    }
    public Vector3 HitPos
    {
        get; set;
    }
    public bool ItemHiting
    {
        get; set;
    }
    public bool Hiting
    {
        get; set;
    }
    public GameObject HitItem
    {
        get; set;
    }
    // Start is called before the first frame update
    void Start()
    {
        Hiting = false;
        ItemHiting = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Cube" && !Hiting && !GetComponent<Move>().Back)
        {
            //other.gameObject.GetComponent<ColorChange>().hit = true;
            HitObject = other.gameObject;
            Hiting = true;
            foreach (ContactPoint point in other.contacts)
            {
                HitPos = point.point;
            }
            //meshRenderer.enabled = false;
            SEClip.CallSe(gameObject, SEClip.SE_NAME.hit, null);
        }
        if (other.gameObject.tag == "Item" && !other.gameObject.GetComponent<ItemMove>().Hiting && !Hiting)
        {
            //other.gameObject.GetComponent<ColorChange>().hit = true;
            HitObject = other.gameObject;
            Hiting = true;
            ItemHiting = true;
            HitItem = other.gameObject;
            other.gameObject.GetComponent<ItemMove>().Hiting = true;
            foreach (ContactPoint point in other.contacts)
            {
                HitPos = point.point;
            }
            //meshRenderer.enabled = false;
            SEClip.CallSe(gameObject, SEClip.SE_NAME.hit, null);
        }
    }
    void OnCollisionStay(Collision other)
    {
        //other.gameObject.GetComponent<ColorChange>().hit = true;
    }
    public void LineDelete()
    {
        Hiting = false;
        meshRenderer.enabled = true;
    }
}
