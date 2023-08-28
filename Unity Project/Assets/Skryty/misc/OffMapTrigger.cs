using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffMapTrigger : MonoBehaviour
{
    public DeathReset father;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(!father.triggerTP)father.triggerTP = true;
        }
    }
}
