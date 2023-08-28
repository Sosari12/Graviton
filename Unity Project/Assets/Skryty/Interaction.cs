using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    //gdy patrzysz siê na obiekt i jesteœ wystarczaj¹co blisko naciœnij E i coœ siê stanie
    public bool seeSwitch;
    public LayerMask switchLayer;
    //JAKAŒ IKONA E NP. ABY DAÆ SNAÆ, ¯E MO¯NA TO KLIKN¥Æ

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        seeSwitch = Physics.Raycast(transform.position, transform.forward, 1f, switchLayer);
        Debug.DrawRay(transform.position, transform.forward, Color.green, switchLayer);


        if (Input.GetKeyDown(KeyCode.E) && seeSwitch)
        {
            //cos zrob

        }
    }
}
