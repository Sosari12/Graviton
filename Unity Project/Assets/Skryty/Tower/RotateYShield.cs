using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateYShield : MonoBehaviour
{
    public float speed;
    public int randomNumber;
    private bool changed;
    public Boss_OkoDamager father;

    // Start is called before the first frame update
    void Start()
    {

        randomNumber = Random.Range(0, 5);
        if (randomNumber == 0) speed  *= 0.9f;
        if (randomNumber == 1) speed *= 0.8f;
        if (randomNumber == 2) speed *= 0.7f;
        if (randomNumber == 3) speed *= 0.6f;
        if (randomNumber == 4) speed *= 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!father.zniszczone)
        {
            transform.Rotate(0f, speed, 0f);
        }
        else
        {
            //if(speed)speed -= Time.deltaTime;
            speed = 0;
        }


    }
}
