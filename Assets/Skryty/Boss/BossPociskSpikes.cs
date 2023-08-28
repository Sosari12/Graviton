using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPociskSpikes : MonoBehaviour
{
    public GameObject spikes;
    public GameObject sfx;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            //spawn sfx + vfx
            //spawn spikes

            GameObject spawned = Instantiate(spikes);
            spawned.transform.localRotation = Quaternion.Euler(new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f)));
            Destroy(gameObject);

        }
    }

}
