using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public Spawn_Enemy spawner;

    //AI
    [Header("AI")]
    public NavMeshAgent agent;
    public Transform Player;

    [Header("Patroling")]
    public Vector3 walkpoint;
    bool walkPointSet;
    public float walkPointRange;

    public bool seenPlayer;

    [Header("Attacking")]
    public float timeBetweenAttack;
    bool alreadyAttacked;
    public GameObject Eye;
    public GameObject enemyProjectile;
    public GameObject followProjectile;
    public float projectileSpeed;
    public LayerMask IgnoreMe;
    private Vector3 destination;
    public LayerMask playerMask;
    public bool rayOnPlayer;
    public bool rayOnGround;
    public bool visionBlocked;

    [Header("States")]
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;


    [Header("Teleport")]
    //Teleport
    public float MaxInActiveTime;
    public float inactiveTime;
    public bool teleporting;
    public Vector3 tpPoint;
    private float tpRange;
    public LayerMask whatIsGround, whatIsPlayer;
    //SFX
    //VFX
    public GameObject TeleportVFX;
    bool createdTPEffect;
    private float tpTime;
    public float maxTpTime;

    [Header("SpiderAnim")]
    public Transform[] IKPlaces;
    public Transform[] IKs;
    public Animator anim;

    private void Awake()
    {
        spawner = GameObject.Find("Spawner").GetComponent<Spawn_Enemy>();
        Player = GameObject.Find("Player").transform;
    }


    // Start is called before the first frame update
    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        inactiveTime = MaxInActiveTime;
        tpRange = spawner.spawnPointRange /2f;
        tpTime = maxTpTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInSightRange && !visionBlocked)
        {
            seenPlayer = true;
            inactiveTime = MaxInActiveTime;
        }
        if(!playerInSightRange || visionBlocked)
        {
            inactiveTime -= Time.deltaTime;
        }

        if (inactiveTime <= 0)
        {
            seenPlayer = true;
            if(!teleporting)CheckActiveTime();
        }

        //if (seenPlayer && !teleporting && !playerInSightRange) CheckActiveTime(); // po zobaczeniu gracza szukanie teleport point
        //if (visionBlocked && !teleporting && playerInSightRange) CheckActiveTime(); //tp gdy gracz jest poza wizj¹
        if (teleporting) Teleport(tpPoint);

        //Check for Playr in sight
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        /// - from forward to up
        rayOnPlayer = Physics.Raycast(Eye.transform.position, Eye.transform.forward, attackRange, whatIsPlayer);
        rayOnGround = Physics.Raycast(Eye.transform.position, Eye.transform.forward, attackRange, whatIsGround);
        if (!rayOnPlayer && rayOnGround) visionBlocked = true;
        if (rayOnPlayer && !rayOnGround) visionBlocked = false;

        Debug.DrawRay(Eye.transform.position, Eye.transform.forward, Color.red);

        if (!playerInSightRange && !playerInAttackRange && !teleporting) Patrol();
        if (seenPlayer && playerInSightRange && !playerInAttackRange && !teleporting) Chase();
        if (seenPlayer && playerInSightRange && playerInAttackRange && !teleporting) Attack();

        

        
    }


    /// <summary>
    /// -----------------------------PATROL CHASE ATTACK
    /// </summary>



    void Patrol()
    {
        if (!walkPointSet) searchWalkPoint();
        if (walkPointSet) agent.SetDestination(walkpoint);

        Vector3 distanceToWalkPoint = transform.position - walkpoint;

        if (distanceToWalkPoint.magnitude < 1f) walkPointSet = false;
    }

    void searchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomY = Random.Range(-walkPointRange, walkPointRange);
        walkpoint = new Vector3(transform.position.x + randomX, transform.position.y + randomY, transform.position.z + randomZ);

        if(Physics.Raycast(walkpoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    void Attack()
    {
        Eye.transform.LookAt(Player);
        

        if (rayOnPlayer && !rayOnGround)
        {

            
            //walking while shooting
            if (walkPointSet) agent.SetDestination(walkpoint);
            else
            {
                agent.SetDestination(transform.position);
                FindPlaceBackwards();
            }
            Vector3 distanceToWalkPoint = transform.position - walkpoint;
            if (distanceToWalkPoint.magnitude < 1f) walkPointSet = false;
            

            //standing while shooting
            /*
            walkpoint = transform.position;
            agent.SetDestination(walkpoint);
            walkPointSet = true;
            */

            //shooting

            if (!alreadyAttacked)
            {

                ShootProjectile();
                ///ATTACK
                ///SFX
                ///VFX

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttack);
                walkPointSet = false;
            }
        }
        else
        {
            agent.SetDestination(Player.transform.position);
        }


    }

    public void ShootProjectile()
    {
        Ray ray = new Ray(Eye.transform.position, Eye.transform.forward); // forward to up
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, ~IgnoreMe))
        {
            destination = hit.point;
        }
        else
        {
            destination = ray.GetPoint(1000f);
        }


        if(Random.Range(0,3) == 1)
        {
            anim.SetTrigger("Attack2");
            InstantiateFollower();
        }
        else
        {
            anim.SetTrigger("Attack1");
            InstantiateProject();
        }
    }


    void InstantiateProject()
    {
        var projectileObj = Instantiate(enemyProjectile, Eye.transform.position, Quaternion.identity) as GameObject;
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - Eye.transform.position).normalized * projectileSpeed;
        //projectileObj.GetComponent<Projectile>().Damage = Damage;
    }

    void InstantiateFollower()
    {
        var projectileObj = Instantiate(followProjectile, Eye.transform.position, transform.rotation) as GameObject;
        //projectileObj.GetComponent<Projectile>().Damage = Damage;
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }


    void Chase()
    {
        agent.SetDestination(Player.position);
        seenPlayer = true;
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    /// <summary>
    /// -------------------------- teleport
    /// </summary>

    private void FindTeleportPoint()
    {
        float randomZ = Random.Range(-tpRange, tpRange);
        float randomX = Random.Range(-tpRange, tpRange);
        float randomY = Random.Range(-tpRange, tpRange);

        tpPoint = new Vector3(spawner.transform.position.x + randomX, spawner.transform.position.y + randomY, spawner.transform.position.z + randomZ);
        if (tpRange > 10f) tpRange /= 2f;


        if (Physics.Raycast(tpPoint, -transform.up, 2f, whatIsGround))
        {
            //tpPoint = new Vector3(tpPoint.x, tpPoint.y + 2f, tpPoint.z);
            
            ///tpinanim
            anim.SetBool("TP", true);
            anim.SetTrigger("Teleport");

            teleporting = true;
            agent.enabled = false;
            walkPointSet = false;
            //gameObject.transform.position = tpPoint;
        }

    }






    private void Teleport(Vector3 tpp)
    {
        if (!createdTPEffect)
        {
            var startTPVFX = Instantiate(TeleportVFX, transform.position, Quaternion.identity) as GameObject;
            startTPVFX.SetActive(true);
            var exitTPVFX = Instantiate(TeleportVFX, tpp, Quaternion.identity) as GameObject;
            exitTPVFX.SetActive(true);
            createdTPEffect = true;
        }
        if(tpTime <= 0)
        {
            gameObject.transform.position = tpp;
            teleporting = false;

            inactiveTime = MaxInActiveTime;
            tpTime = maxTpTime;
            agent.enabled = true;
            createdTPEffect = false;

            //reset IK position
            IKs[0].position = IKPlaces[0].position;
            IKs[1].position = IKPlaces[1].position;
            IKs[2].position = IKPlaces[2].position;
            IKs[3].position = IKPlaces[3].position;

            ///tpoutanim
            anim.SetBool("TP", false);

        }
        else
        {
            tpTime -= Time.deltaTime;
        }
    }

    


    private void CheckActiveTime()
    {
        if(inactiveTime <= 0)
        {
            //teleporting = true;
            FindTeleportPoint(); 
        }
        else
        {
            inactiveTime -= Time.deltaTime;
        }
    }

    private void FindPlaceBackwards()
    {
        float randomZ = Random.Range(-walkPointRange /2f , walkPointRange /2f);
        float randomX = Random.Range(-walkPointRange / 2f, walkPointRange / 2f);
        float randomY = Random.Range(-walkPointRange / 2f, walkPointRange / 2f);
        walkpoint = new Vector3(transform.position.x + randomX, transform.position.y + randomY, transform.position.z + randomZ);

        if (Physics.Raycast(walkpoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

}
