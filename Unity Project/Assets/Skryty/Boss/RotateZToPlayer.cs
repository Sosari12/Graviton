using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateZToPlayer : MonoBehaviour
{
    Rigidbody rb;
    public Transform player;
    public Transform father;
    public float rotateRatio;
    public Vector3 newRotation;
    public float tiltAroundZ;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //float Rotation;
        //tiltAroundZ = player.eulerAngles.z;

        /*
        if (player.localEulerAngles.z <= 180f)
        {
            tiltAroundZ = player.localEulerAngles.z;
        }
        else
        {
            tiltAroundZ = player.localEulerAngles.z - 360f;
        }


        Quaternion target = Quaternion.Euler(0f, 0f, tiltAroundZ);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * rotateRatio);

        //transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, rotateRatio * Time.deltaTime);
        */


        newRotation = new Vector3(father.eulerAngles.x, father.eulerAngles.y, player.transform.eulerAngles.z);
        gameObject.transform.eulerAngles = newRotation;

    }
}
