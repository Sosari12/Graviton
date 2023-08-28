using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public float time = 1f;

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, time);
    }
}
