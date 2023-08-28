using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointSetter : MonoBehaviour
{
    public DeadScript father;
    public int myNumber;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            father.SetNewCheckPoint(myNumber);
        }
    }
}
