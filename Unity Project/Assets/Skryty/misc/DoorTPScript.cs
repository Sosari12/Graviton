using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTPScript : MonoBehaviour
{
    public int aktywowanePilary;
    public GameObject TpDoor;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(aktywowanePilary >= 3)
        {
            TpDoor.SetActive(true);
        }
    }
}
