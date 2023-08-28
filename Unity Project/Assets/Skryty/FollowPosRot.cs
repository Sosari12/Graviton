using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPosRot : MonoBehaviour
{
    public Transform target;
    //public string coSzukac;
    //public bool uzycSzukania;
    public float followRatio = 7;
    public float rotateRatio = 7;

    // Start is called before the first frame update
    void Start()
    {
        //if (coSzukac != null && uzycSzukania) target = GameObject.Find(coSzukac).transform;
        if(followRatio > 0)followRatio = Random.Range(followRatio - 2, followRatio + 2);
        if(rotateRatio > 0)rotateRatio = Random.Range(rotateRatio - 2, rotateRatio + 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (rotateRatio > 0) transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, rotateRatio * Time.deltaTime);
        if (followRatio > 0) transform.position = Vector3.Lerp(transform.position, target.position, followRatio * Time.deltaTime);
    }
}
