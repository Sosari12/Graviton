using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class PlayerShield : MonoBehaviour
{
    [Header("Values")]
    public float shieldEnergy;
    private float maxShieldEnergy;
    public float shieldCD;
    private float maxShieldCD;
    public float regenRate;
    private float baseRegenRate;
    public float shortCD;
    private float maxShortCD;

    public bool shieldActive;
    public bool shieldOnCD;
    public bool shortCDActive;

    public GameObject shieldObject;
    public GameObject onhitVFX;

    [Header("Animator")]
    public Animator handAnim;
    public Shooting father;


    // Start is called before the first frame update
    void Start()
    {
        maxShieldEnergy = shieldEnergy;
        maxShieldCD = shieldCD;
        baseRegenRate = regenRate;
        maxShortCD = shortCD;
    }

    // Update is called once per frame
    void Update()
    {
        if (shieldEnergy == 100) regenRate = baseRegenRate;
        if (shieldOnCD) ResetCD();



        if (shieldActive)
        {
            shortCDActive = true;
            //if (!shieldObject.activeSelf) shieldObject.SetActive(true);
            shortCD = maxShortCD;
        }
        else
        {
            //if (shieldObject.activeSelf) shieldObject.SetActive(false);
            if (shortCDActive) ResetShortCD();
        }

        
        if (shieldEnergy < maxShieldEnergy && !shieldActive && !shieldOnCD && !shortCDActive) shieldEnergy += regenRate;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet") && shieldActive)
        {

            CameraShaker.Instance.ShakeOnce(3f, 2f, 0f, .5f);
            if (Random.Range(0,2) == 0) handAnim.SetTrigger("Hit1");
            else handAnim.SetTrigger("Hit2");

            if(father.ammoProcent < 100)father.ammoProcent += 10;

            float dmg = other.GetComponent<Projectile>().Damage;
            Destroy(other.gameObject);
            //VFX
            //SFX
            //ANIMATOR
            //ZMNIEJSZENIE ENERGII
            Instantiate(onhitVFX, other.transform.position, Quaternion.identity);
            if(shieldEnergy > dmg)shieldEnergy -= dmg;
            else
            {
                //handAnim.SetBool("ShieldDestroy", true);
                handAnim.SetTrigger("ShieldDstr");
                handAnim.SetBool("ShieldUp", false);
                shieldEnergy = 0;
                shieldOnCD = true;
                regenRate = regenRate /2f;
            }
        }
    }

    void ResetCD()
    {
        if (shieldCD < 0)
        {
            shieldCD = maxShieldCD;
            shieldOnCD = false;
        }
        else
        {
            shieldCD -= Time.deltaTime;
            shieldEnergy += regenRate;
        }
    }


    void ResetShortCD()
    {
        if (shortCD < 0)
        {
            shortCD = maxShortCD;
            shortCDActive = false;
        }
        else
        {
            shortCD -= Time.deltaTime;
        }
    }


}
