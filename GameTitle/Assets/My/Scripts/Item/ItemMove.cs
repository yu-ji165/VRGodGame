using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMove : MonoBehaviour
{
    public Vector2 randomCount;
    public Vector2 randomSpeed;
    public Vector2 randomMoveTime;
    public GameObject littleItemOriginal;
    private GameObject player;
    private Vector3 size;
    public bool Hiting
    {
        get; set;
    }
    public bool Move
    {
        get; set;
    }
    public float DefDistance
    {
        get; set;
    }
    public float Speed
    {
        get; set;
    }
    // Start is called before the first frame update
    void Start()
    {
        Hiting = false;
        Move = false;
        player = GameObject.FindWithTag("Player");
        size = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (Move)
        {
            GetComponent<SphereCollider>().enabled = false;
            Vector3 move = player.transform.position - transform.position;
            move = move.normalized;
            transform.position += move * (Speed);
            //transform.localScale = size * (distance / DefDistance);
        }
        if (distance < 3)
        {
            int random = (int)Random.Range(randomCount.x, randomCount.y);
            for (int i = 0; i < random; i++)
            {
                GameObject littleItem = Instantiate(littleItemOriginal, transform.position, Quaternion.identity);
                float f = Random.Range(0f, 2f * Mathf.PI);
                float t = Mathf.Acos(Random.Range(-1f, 1f));
                littleItem.GetComponent<LittleItemMove>().MoveVec = new Vector3(Mathf.Sin(t) * Mathf.Cos(f), Mathf.Sin(t) * Mathf.Sin(f), Mathf.Cos(t));
                littleItem.transform.LookAt(player.transform);
                if (i == random - 1)
                {
                    littleItem.GetComponent<LittleItemMove>().Speed = randomSpeed.y;
                    littleItem.GetComponent<LittleItemMove>().MoveTime = (int)randomMoveTime.y;
                    littleItem.GetComponent<LittleItemMove>().Last = true;
                }
                else
                {
                    littleItem.GetComponent<LittleItemMove>().Speed = Random.Range(randomSpeed.x, randomSpeed.y);
                    littleItem.GetComponent<LittleItemMove>().MoveTime = (int)Random.Range(randomMoveTime.x, randomMoveTime.y);
                }
            }
            Destroy(gameObject);
        }
    }
}