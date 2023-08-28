using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleLookAt : MonoBehaviour
{
    public Transform target;
    public bool lookAtPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(lookAtPlayer && target == null)target = GameObject.Find("Player").transform;
        transform.LookAt(target, Vector3.forward);
    }
}
