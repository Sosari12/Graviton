using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateReactor : MonoBehaviour
{
    public float speed;
    public float timer;
    private float timerMax;

    // Start is called before the first frame update
    void Start()
    {
        timerMax = timer;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer <= 0)
        {
            timer = timerMax;
            speed = -1f * speed;
        }
        else
        {
            timer -= Time.deltaTime;
        }
        transform.Rotate(speed * 0.8f, speed * 0.9f, speed * 0.5f);
    }
}
