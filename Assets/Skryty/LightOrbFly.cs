using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOrbFly : MonoBehaviour
{

    [SerializeField] private GameObject target;
    [SerializeField] private float degreesPerSecond = 45;
    public bool forwardOrUp;
    public OrbReturn doorTrigger;

    // Update is called once per frame
    void Update()
    {
        if (!doorTrigger.orbyWracac)
        {
            if (!forwardOrUp) transform.RotateAround(target.transform.position, Vector3.forward, degreesPerSecond * Time.deltaTime);
            if (forwardOrUp) transform.RotateAround(target.transform.position, Vector3.up, degreesPerSecond * Time.deltaTime);
        }
        else
        {
            this.GetComponent<LilOrb>().enabled = true;
            GetComponent<LilOrb>().lastFlight = true;
        }

    }
}
