using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    [Header("Timers")]
    public float idleTime;
    private float idleTimeMax;

    [Header("Shooting")]
    public GameObject projectile;
    public Transform gunPoint;
    private Vector3 destination;
    public LayerMask IgnoreMe;
    public float projectileSpeed;

    [Header("Anims")]
    public Animator anim;
    public GameObject Eye;
    private bool attacking;
    public bool open;
    public bool dead;
    public GameObject father;

    [Header("States")]
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public LayerMask whatIsGround, whatIsPlayer;
    public bool rayOnPlayer;
    public bool rayOnGround;
    private Transform Player;



    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        idleTimeMax = idleTime;
        Player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(father.GetComponent<Boss_OkoDamager>().zniszczone == true && !dead)
        {
            dead = true;
            anim.SetBool("Dead", true);
            anim.SetTrigger("Die");
        }

        if (!dead)
        {
            if (!attacking)
            {
                CountToOpen();
            }


            gunPoint.transform.LookAt(Player);
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
            rayOnPlayer = Physics.Raycast(Eye.transform.position, Eye.transform.forward, attackRange, whatIsPlayer);
            rayOnGround = Physics.Raycast(Eye.transform.position, Eye.transform.forward, attackRange, whatIsGround);

            Debug.DrawRay(Eye.transform.position, Eye.transform.forward, Color.red);

            if (playerInSightRange && playerInAttackRange && open)
            {
                attacking = true;
                anim.SetBool("Attacking", true);
                anim.SetTrigger("Shoot");
            }

            if(!playerInSightRange && !playerInAttackRange)
            {
                attacking = false;
            }
        }
        else
        {
            if (Eye.activeSelf)
            {
                Eye.SetActive(false);
            }
        }

    }

    void CountToOpen()
    {
        if(idleTime <= 0)
        {
            idleTime = idleTimeMax;
            if (open)
            {
                open = false;
                anim.SetBool("Open", false);
            }
            else if (!open)
            {
                open = true;
                anim.SetBool("Open", true);
            } 

        }
        else
        {
            idleTime -= Time.deltaTime;
        }
    }

    public void ShootProjectile()
    {
        Ray ray = new Ray(gunPoint.transform.position, gunPoint.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, ~IgnoreMe))
        {
            destination = hit.point;
        }
        else
        {
            destination = ray.GetPoint(1000f);
        }

            InstantiateProject();

    }

    public void InstantiateProject()
    {
        //Instantiate(projectile, gunPoint.transform.position, Quaternion.identity);
        var projectileObj = Instantiate(projectile, gunPoint.transform.position, Quaternion.identity) as GameObject;
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - Eye.transform.position).normalized * projectileSpeed;
    }


}
