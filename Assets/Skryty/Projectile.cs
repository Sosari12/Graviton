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
        transform.LookAt(GameObject.Find("Player").transform);
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "Player" && !collided)
        {
            //collided = true;
            if(!dontDestroy) Destroy(gameObject, TimeToDestroy);
        }


    }
    */

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Enemy") && !belongsToEnemy)
        {
            DisableVFXParent();
            other.GetComponent<EnemyDamager>().TakeDamage(Damage);
            Instantiate(onHitVFX, other.transform.position, Quaternion.identity);
            if (hitSFX != null) hitSFX.Play();
            Destroy(gameObject);
        }

        if (other.CompareTag("Ground"))
        {
            DisableVFXParent();
            collided = true;
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            if(VFXPlace == null) Instantiate(groundHitVFX, transform.position, Quaternion.identity);
            else Instantiate(groundHitVFX, VFXPlace.position, Quaternion.identity);

            if (bossSpikes)
            {
                GameObject spawned = Instantiate(spikes, transform.position, Quaternion.identity);
                spawned.transform.localRotation = Quaternion.Euler(new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f)));
            }

            if(!dontDestroy)Destroy(gameObject);
        }


        if (other.CompareTag("BossEye") && !belongsToEnemy)
        {
            DisableVFXParent();
            if (hitSFX != null) hitSFX.Play();

            other.GetComponent<Boss_OkoDamager>().OdejmijHP(Damage);
            Instantiate(onHitVFX, other.transform.position, Quaternion.identity);

            //sfx
            //bfx
            Destroy(gameObject);
        }

        if(other.CompareTag("BossShield") && !belongsToEnemy)
        {
            DisableVFXParent();
            Instantiate(onHitVFX, transform.position, Quaternion.identity);
            //sfx

            Destroy(gameObject);
        }


       
        

        if (other.CompareTag("Player") && belongsToEnemy)
        {
            //DisableVFXParent();
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

        }


        if (other.gameObject.CompareTag("Shield") && belongsToEnemy && other.gameObject.GetComponent<PlayerShield>().shieldActive)
        {
            DisableVFXParent();
            //zadaj dmg
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            Destroy(gameObject);
        }

    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Shield") && belongsToEnemy && collision.gameObject.GetComponent<PlayerShield>().shieldActive)
        {
            DisableVFXParent();
            //zadaj dmg
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            Destroy(gameObject);
        }
    }
    */

    public void DisableVFXParent()
    {
        if (unParentTrail)
        {
            if (trail.transform.parent != null && trail != null) trail.transform.parent = null;
        }
    }


}
