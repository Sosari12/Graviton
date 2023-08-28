using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public float speed;
    public float currentSize;
    public float maxSize;
    private Vector3 scaleChange;
    public float Damage;
    public bool scaleUp;

    // Start is called before the first frame update
    void Start()
    {
        scaleChange = new Vector3(speed, speed, speed);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentSize < maxSize && scaleUp)SpikesGrow();
    }


    void SpikesGrow()
    {
        transform.localScale += scaleChange * Time.deltaTime;
        currentSize += speed * Time.deltaTime;
    }

}
