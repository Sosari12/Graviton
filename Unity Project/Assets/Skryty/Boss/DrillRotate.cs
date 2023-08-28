using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillRotate : MonoBehaviour
{
    public GameObject father;
    public float speed;
    public float z;
    public bool hitGround;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(father.GetComponent<Projectile>().collided == true)
        {
            hitGround = true;
        }


        if (hitGround)
        {
            if (speed > 0) speed -= Time.deltaTime * 50f;
            else speed = 0;
        }

        if(speed >= 0)
        {
            z += Time.deltaTime * speed;
            transform.localRotation = Quaternion.Euler(0, 0, z);
        }



    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            hitGround = true;
        }
    }
    */
}
