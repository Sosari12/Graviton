using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    //gdy patrzysz si� na obiekt i jeste� wystarczaj�co blisko naci�nij E i co� si� stanie
    public bool seeSwitch;
    public LayerMask switchLayer;
    //JAKA� IKONA E NP. ABY DA� SNA�, �E MO�NA TO KLIKN��

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
