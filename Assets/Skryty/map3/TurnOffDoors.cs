using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffDoors : MonoBehaviour
{
    public GameObject doors;
    public GameObject Map;

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
            doors.SetActive(false);
            if (Map != null) Map.SetActive(false);
        }
    }
}
