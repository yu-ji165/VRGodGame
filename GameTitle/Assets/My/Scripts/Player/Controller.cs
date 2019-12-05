using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Controller : MonoBehaviour
{
    public LRController.LR_WIER lr;
    public OVRInput.Controller lrController;
    public float speed;
    public float maxSize;
    public OVRInput.Button controllerNumber;
    public OVRInput.Button recoveryButton;
    public OVRInput.RawButton buttonNumber;
    public LineRenderer ren;
    public GameObject pullJudgOriginal;
    public GameObject clawOriginal;
    public GameObject pointerOriginal;
    public Material[] materials;
    public int itemMaxCount;
    private GameObject claw;
    private GameObject pull;
    private GameObject player;
    private GameObject pointer;
    private GameObject text;
    private GameObject finishText;
    public int ItemCount
    {
        get; set;
    }

    void Start()
    {
        //線の幅
        ren.startWidth = 0.01f;
        ren.endWidth = 0.01f;
        //頂点の数
        ren.positionCount = 2;
        ren.enabled = false;
        player = GameObject.FindWithTag("Player");
        text = GameObject.FindWithTag("Text");
        finishText = GameObject.FindWithTag("FinishText");
        ItemCount = 0;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //if(OVRInput.GetDown(OVRInput.Button.Three))
        //{
        //    Debug.Log(ItemCount);
        //}

        if (ItemCount >= itemMaxCount)
        {
            finishText.GetComponent<TextMeshProUGUI>().text = "Clear";
            player.GetComponent<PlayerHit>().Finishd = PlayerHit.Finish.clear;
            if (claw != null)
            {
                claw.GetComponent<Move>().Back = true;
                claw.GetComponent<Move>().MyMove(transform.position, ref ren);
                ren.SetPosition(0, transform.position);
                ren.SetPosition(1, claw.transform.position);
                if (claw.GetComponent<Hit>().Hiting && !claw.GetComponent<Move>().Back)
                {
                    ren.material = materials[0];
                }
                else if (!claw.GetComponent<Move>().Back)
                {
                    ren.material = materials[1];
                }
                else
                {
                    ren.material = materials[2];
                }
            }
        }
        else if (player.GetComponent<PlayerHit>().Finishd == PlayerHit.Finish.gameOver)
        {
            finishText.GetComponent<TextMeshProUGUI>().text = "GameOver";
            if (claw != null)
            {
                claw.GetComponent<Move>().Back = true;
                claw.GetComponent<Move>().MyMove(transform.position, ref ren);
                ren.SetPosition(0, transform.position);
                ren.SetPosition(1, claw.transform.position);
                if (claw.GetComponent<Hit>().Hiting && !claw.GetComponent<Move>().Back)
                {
                    ren.material = materials[0];
                }
                else if (claw.GetComponent<Move>().Back)
                {
                    ren.material = materials[1];
                }
                else
                {
                    ren.material = materials[2];
                }
            }
        }
        else if (player.GetComponent<PlayerHit>().Finishd == PlayerHit.Finish.no)
        {
            claw = GameObject.FindWithTag(controllerNumber.ToString());
            Vector3 position = transform.position;
            if (OVRInput.GetDown(controllerNumber) && claw == null)
            {
                GameObject a;
                a = Instantiate(clawOriginal, position, Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, 0)) as GameObject;
                a.tag = controllerNumber.ToString();
                pull = Instantiate(pullJudgOriginal, position, Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, 0));
                pull.transform.LookAt(a.transform.position);
                pull.transform.parent = player.transform;
                pull.tag = lr.ToString();
                GetComponent<Pull>().target = a.transform.position;
                a.GetComponent<Move>().MyMove(transform.position, ref ren);
                SEClip.CallSe(gameObject, SEClip.SE_NAME.shot, null);
            }
            else if (claw != null)
            {
                // 頂点を設定
                if (!ren.enabled)
                    ren.enabled = true;
                claw.GetComponent<Move>().MyMove(transform.position, ref ren);
                ren.SetPosition(0, transform.position);
                ren.SetPosition(1, claw.transform.position);
                if (claw.GetComponent<Hit>().Hiting && !claw.GetComponent<Move>().Back)
                {
                    ren.material = materials[0];
                }
                else if (!claw.GetComponent<Move>().Back)
                {
                    ren.material = materials[1];
                }
                else
                {
                    ren.material = materials[2];
                }
                if (GetComponent<Pull>().HitPull)
                {
                    if (claw.GetComponent<Hit>().Hiting && claw.GetComponent<Hit>().ItemHiting)//アイテム回収
                    {
                        Vector3 a = OVRInput.GetLocalControllerVelocity(GetComponent<Controller>().lrController);
                        float moveSpeed = Vector3.Distance(Vector3.zero, a);
                        claw.GetComponent<Hit>().HitItem.GetComponent<ItemMove>().Move = true;
                        claw.GetComponent<Hit>().HitItem.GetComponent<ItemMove>().Speed = moveSpeed;
                        claw.GetComponent<Hit>().HitItem.GetComponent<ItemMove>().DefDistance = Mathf.Abs(Vector3.Distance(player.transform.position, claw.GetComponent<Hit>().HitPos));
                    }
                    else if (claw.GetComponent<Hit>().Hiting)//自分移動設定
                    {
                        Vector3 a = OVRInput.GetLocalControllerVelocity(GetComponent<Controller>().lrController);
                        float moveSpeed = Vector3.Distance(Vector3.zero, a);
                        Vector3 move;
                        //if (moveSpeed < 1)
                        //    moveSpeed = 1;
                        //if (OVRInput.Get(OVRInput.RawButton.B) || OVRInput.Get(OVRInput.RawButton.Y))
                        //    target = FindObjectOfType<NULL>().gameObject.transform.position;
                        move = claw.transform.position - transform.position;
                        move = move.normalized + GetComponent<Pull>().PullVec * 0.1f;
                        player.GetComponent<PlayerMove>().MoveVec = move;
                        player.GetComponent<PlayerMove>().Speed = moveSpeed * speed;
                        if (lr == LRController.LR_WIER.left)
                        {
                            player.GetComponent<PlayerMove>().LMove = move;
                            player.GetComponent<PlayerMove>().LSimultaneous = true;
                        }
                        if (lr == LRController.LR_WIER.right)
                        {
                            player.GetComponent<PlayerMove>().RMove = move;
                            player.GetComponent<PlayerMove>().RSimultaneous = true;
                        }
                        SEClip.CallSe(player.gameObject, SEClip.SE_NAME.accelerator, player.GetComponent<PlayerMove>().MoveVec);
                    }
                    claw.GetComponent<Move>().Back = true;
                    GetComponent<Pull>().HitPull = false;
                }
                else
                {

                    if (OVRInput.GetDown(recoveryButton))
                    {
                        claw.GetComponent<Move>().Back = true;
                        Destroy(pull.gameObject);
                    }
                }
            }
        }
        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
        RaycastHit[] hits = Physics.RaycastAll(ray, 1000f);
        Destroy(pointer);
        foreach (var obj in hits)
        {
            // 衝突したオブジェクトのタグ毎に色を変え、当てはまらない場合はオブジェクトを非アクティブにする
            if (obj.collider.tag == "Cube" || obj.collider.tag == "Item")
            {
                pointer = Instantiate(pointerOriginal, obj.point, Quaternion.identity);
                pointer.GetComponent<PointerSize>().SizeUpDate(player.transform.position);
                break;
            }
        }
        if (lr == LRController.LR_WIER.left)
            text.GetComponent<TextMeshProUGUI>().text = ItemCount + "/" + itemMaxCount;
    }
}