using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilOrb : MonoBehaviour
{
    public float speed;
    public Transform target;
    public Transform altTarget;

    //szukanie
    public string coSzukac;
    public string coSzukacDrugi;
    public bool CzySzukac;

    public float homeTime;
    private float maxHomeTime;
    private Rigidbody rb;
    Vector3 moveUp;
    bool touchedGround;
    public bool lastFlight;

    [Header("Efekty")]
    private AudioSource source;
    private float val;

    // Start is called before the first frame update
    void Start()
    {
        if(this.GetComponent<AudioSource>())source = this.GetComponent<AudioSource>();
        if(CzySzukac) target = GameObject.Find(coSzukac).transform;
        maxHomeTime = homeTime;
        rb = GetComponent<Rigidbody>();
        moveUp.y = .7f;
    }

    // Update is called once per frame
    void Update()
    {
        if (lastFlight && altTarget != null)
        {
            target = altTarget;
        }
        if(target == null)
        {
            target = GameObject.Find(coSzukacDrugi).transform;
            lastFlight = true;
        }
        Vector3 distanceToTarget = transform.position - target.position;
        if (distanceToTarget.magnitude < 1f && lastFlight) Destroy(gameObject);

        if (homeTime < 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed);
        }
        else
        {
            rb.AddRelativeForce(moveUp, ForceMode.Force);
            homeTime -= Time.deltaTime;
            if(source != null)if(source.volume < .5f)source.volume += Time.deltaTime * .1f;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            touchedGround = true;
        }
    }
}
