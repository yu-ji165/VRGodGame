using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed;
    private Vector3 defPos;
    private int cou;
    private int dec;
    private GameObject player;
    public bool Back
    {
        get; set;
    }
    // Start is called before the first frame update
    void Start()
    {
        defPos = transform.position;
        cou = 0;
        dec = 0;
        player = GameObject.FindWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
    }
    public void MyMove(Vector3 defpos, ref LineRenderer ren)
    {
        if (!GetComponent<Hit>().Hiting && !Back)
        {
            // レイの設定
            Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Abs(Vector3.Distance(defpos + transform.forward * speed * (cou + 1), transform.position))))
            {
                //当たった時の処理
                if (hit.collider.tag == "Cube" || hit.collider.tag == "Item")
                {
                    transform.position = hit.point;
                }
                else
                {
                    cou++;
                    transform.position = defpos + transform.forward * speed * cou;
                }
            }
            else
            {
                cou++;
                transform.position = defpos + transform.forward * speed * cou;
            }
        }
        else if (Back)
        {
            dec += 2;
            Vector3 distsnce = defpos - transform.position;
            Vector3 move = distsnce.normalized;
            if (100f < Mathf.Abs(Vector3.Distance(Vector3.zero, distsnce)))
            {
                transform.position = defpos + -Vector3.Normalize(distsnce) * 100f;
            }
            transform.position += move * speed * 2;
            if (Mathf.Abs(Vector3.Distance(Vector3.zero, distsnce)) <= Mathf.Abs(Vector3.Distance(Vector3.zero, move * speed)))
            {
                ren.enabled = false;
                Destroy(gameObject);
            }
        }
        //transform.position += player.GetComponent<PlayerMove>().MoveVec * player.GetComponent<PlayerMove>().Speed;
    }
}