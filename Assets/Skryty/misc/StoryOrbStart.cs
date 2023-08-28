using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryOrbStart : MonoBehaviour
{
    public GameObject orb;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            orb.SetActive(true);
            Destroy(gameObject);
        }
    }
}
