using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossOkoAnimScript : MonoBehaviour
{
    int randomNumber;
    Animator anim;
    public GameObject RoarSFX;


    //shooting
    public Transform[] shootingPosition;
    public Transform[] homingPosition;
    public GameObject spikes;
    public GameObject homeAttack;
    public Transform player;
    public Vector3 destination;
    public float projectileSpeed;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        destination = player.transform.position;
    }

    public void RandomizeAttackFaze1()
    {
        randomNumber = Random.Range(0, 6);
        if (randomNumber == 0) anim.SetTrigger("Attack1");
        if (randomNumber == 1) anim.SetTrigger("Attack2");
        if (randomNumber == 2) anim.SetTrigger("Attack3");
        if (randomNumber == 3) anim.SetTrigger("Attack4");
        if (randomNumber == 4) anim.SetTrigger("Attack5");
        //mozna pozniej dodac aby ewentualnie losowalo z wiekszej puli i poza granica atakow robi idle
    }

    public void RandomizeAttackFaze2()
    {
        randomNumber = Random.Range(0, 9);
        if (randomNumber == 0) anim.SetTrigger("Attack1");
        if (randomNumber == 1) anim.SetTrigger("Attack2");
        if (randomNumber == 2) anim.SetTrigger("Attack3");
        if (randomNumber == 3) anim.SetTrigger("Attack4");
        if (randomNumber == 4) anim.SetTrigger("Attack5");
        if (randomNumber == 5) anim.SetTrigger("Attack6");
        if (randomNumber == 6) anim.SetTrigger("Attack7");
        if (randomNumber == 7) anim.SetTrigger("Attack8");
    }

    public void spawnSpikeL(int ile)
    {
        var projectileObj = Instantiate(spikes, shootingPosition[ile].position, Quaternion.identity) as GameObject;
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - shootingPosition[ile].position).normalized * projectileSpeed;
        //projectileObj.GetComponent<Projectile>().Damage = Damage;

        //var vfxShoot = Instantiate(shootParticle, GunPoint.position, Quaternion.identity) as GameObject;
    }

    public void spawnHoming(int ile)
    {
        Instantiate(homeAttack, homingPosition[ile].position, Quaternion.identity);
    }

    public void zmienFaze()
    {
        anim.SetBool("AlreadyIn", true);
    }

    public void RoarSpawn()
    {
        Instantiate(RoarSFX, transform.position, Quaternion.identity);
    }

    public void EndGame()
    {
        GameObject.Find("MainControl").GetComponent<MainController>().Return = true;
    }

}
