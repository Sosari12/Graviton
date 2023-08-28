using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool collided;
    public int Damage;
    public GameObject onHitVFX;
    public GameObject groundHitVFX;
    public Transform VFXPlace;
    public bool belongsToEnemy;
    public bool bossSpikes;
    public GameObject spikes;
    public float TimeToDestroy = 2f;
    public bool dontDestroy;
    private AudioSource hitSFX;

    public bool unParentTrail;
    public GameObject trail;

    private void Start()
    {
        hitSFX = GameObject.Find("HitSFX").GetComponent<AudioSource>();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "Player" && !collided)
        {
            //collided = true;
            if(!dontDestroy) Destroy(gameObject, TimeToDestroy);
        }


    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !belongsToEnemy)
        {
            other.GetComponent<EnemyDamager>().TakeDamage(Damage);
            Instantiate(onHitVFX, other.transform.position, Quaternion.identity);
            Destroy(gameObject);
            if (hitSFX != null) hitSFX.Play();
        }

        if (other.CompareTag("Ground"))
        {
            collided = true;
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            if(VFXPlace == null) Instantiate(groundHitVFX, transform.position, Quaternion.identity);
            else Instantiate(groundHitVFX, VFXPlace.position, Quaternion.identity);

            if (bossSpikes)
            {
                GameObject spawned = Instantiate(spikes, transform.position, Quaternion.identity);
                spawned.transform.localRotation = Quaternion.Euler(new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f)));
            }

            if (unParentTrail)
            {
                trail.GetComponent<Destroyer>().enabled = true;
                trail.transform.parent = null;
            }
            if(!dontDestroy)Destroy(gameObject);
        }


        if (other.CompareTag("BossEye") && !belongsToEnemy)
        {
            other.GetComponent<Boss_OkoDamager>().OdejmijHP(Damage);
            Instantiate(onHitVFX, other.transform.position, Quaternion.identity);
            Destroy(gameObject);
            //sfx
            //bfx
            if (hitSFX != null) hitSFX.Play();
        }

        if(other.CompareTag("BossShield") && !belongsToEnemy)
        {
            Destroy(gameObject);
            Instantiate(onHitVFX, transform.position, Quaternion.identity);
            //sfx
        }


        /*
        if(other.CompareTag("Shield") && belongsToEnemy)
        {
            //zadaj dmg
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            Destroy(gameObject);
        }
        */

        if (other.CompareTag("Player") && belongsToEnemy)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

        }

    }


}
