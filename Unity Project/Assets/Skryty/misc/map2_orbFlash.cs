using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class map2_orbFlash : MonoBehaviour
{
    public Animator sphereAnim;
    public Animator lightAnim;
    public Transform target;
    private bool reachedTarget;
    public bool returnToGate;
    //public GameObject Stager;
    //public GameObject soundBoom;
    private bool done;
    public GameObject doorToDisable;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<LilOrb>().lastFlight) returnToGate = true;

        Vector3 distanceToTarget = transform.position - target.position;
        

        if (distanceToTarget.magnitude < 1f) reachedTarget = true;


        if (reachedTarget && !returnToGate && !done)
        {
            doorToDisable.SetActive(false);
            sphereAnim.SetBool("Flash", true);
            lightAnim.SetBool("Flash", true);
            //Stager.SetActive(true);
            //Instantiate(soundBoom, transform.position, Quaternion.identity);
            done = true;
        }

        if (returnToGate)
        {
            
            sphereAnim.SetBool("Flash", false);
            lightAnim.SetBool("Flash", false);
        }
    }
}
