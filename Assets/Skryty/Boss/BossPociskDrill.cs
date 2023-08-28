using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPociskDrill : MonoBehaviour
{
    private Transform Player;
    public float followTime;
    public float shootSpeed;
    public bool shot;
    public GameObject model;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(followTime <= 0 && !shot)
        {
            shootForward();
            shot = true;
            model.GetComponent<SimpleLookAt>().enabled = false;

        }
        else
        {
            followTime -= Time.deltaTime;
        }
    }

    void shootForward()
    {
        GetComponent<EnemyHomingAttack>().StopHoming();
        GetComponent<EnemyHomingAttack>().enabled = false;
        //pierd sfx
        gameObject.GetComponent<Rigidbody>().velocity = (Player.position - transform.position).normalized * shootSpeed;
    }
}
