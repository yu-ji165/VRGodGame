using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEClip : MonoBehaviour
{
    public enum SE_NAME
    {
        shot = 0,
        hit,
        accelerator,
        maxsize
    }
    public AudioClip[]
        seClip = new AudioClip[(int)SE_NAME.maxsize];
    public GameObject
        seZone;
    public float
        distance = 5.0f;

    private static GameObject callObject { get; set; }
    private static SE_NAME seName { get; set; }
    private static Vector3? moveVector = null;

    private void SePlay()
    {
        GameObject audioZone;
        if (seName == SE_NAME.accelerator)
        {
            audioZone = Instantiate(seZone, callObject.transform.position + moveVector.Value * distance, Quaternion.identity);
        }
        else
        {
            audioZone = Instantiate(seZone, callObject.transform);
            audioZone.transform.parent = callObject.transform;
        }
        audioZone.GetComponent<AudioSource>().PlayOneShot(seClip[(int)seName]);
    }

    public static void CallSe(GameObject otherObject, SE_NAME callName, Vector3? moveVec)
    {
        SEClip audio;
        audio = GameObject.Find("AudioCon").GetComponent<SEClip>();

        callObject = otherObject;
        seName = callName;
        moveVector = moveVec;

        audio.SePlay();
    }
}