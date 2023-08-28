using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform cameraPosition;
    public PlayerMovement father;
    public bool copyPos;
    public bool copyRot;
    //float pos

    // Update is called once per frame
    void Update()
    {


        if(copyPos)transform.position = cameraPosition.position;
        if(copyRot)transform.rotation = cameraPosition.transform.rotation;
        //rotX = cameraPosition.transform.rotation.x;
        //rotY = cameraPosition.transform.rotation.y;
        //rotZ = cameraPosition.transform.rotation.z;
        //transform.rotation = Quaternion.Euler(rotX, rotY, rotZ);
        //if(lockY == true)transform.localRotation = Quaternion.Euler(new Vector3(cameraPosition.eulerAngles.x, 0f, cameraPosition.eulerAngles.z));
    }
}
