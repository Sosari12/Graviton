using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHomingAttack : MonoBehaviour
{
    public float speed;
    private Transform target;
    public string coSzukac;
    public float homeTime;
    private float maxHomeTime;
    private Rigidbody rb;
    Vector3 moveUp;
    bool touchedGround;
    bool stop;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find(coSzukac).transform;
        maxHomeTime = homeTime;
        rb = GetComponent<Rigidbody>();
        moveUp.y = .7f;
    }

    // Update is called once per frame
    void Update()
    {
        if(homeTime < 0)
        {
            if(!stop)if(!touchedGround)transform.position = Vector3.MoveTowards(transform.position, target.position, speed);
        }
        else
        {
            rb.AddRelativeForce(moveUp, ForceMode.Force);
            homeTime -= Time.deltaTime;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            touchedGround = true;
        }
    }

    public void StopHoming()
    {
        stop = true;
        transform.position = transform.position;
    }
}
