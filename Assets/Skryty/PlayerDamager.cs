using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EZCameraShake;

public class PlayerDamager : MonoBehaviour
{
    [Header("Stats")]
    public float Health;
    private float MaxHealth;
    public float regenRate;
    private float maxRegenRate;
    private float shieldEnergy;

    [Header("UI")]
    public TMP_Text healthText;
    public TMP_Text shieldText;
    public Animator HitScreenAnim;

    [Header("Objects")]
    public PlayerShield plShield;

    [Header("Timers")]
    private bool HitRecently;
    public float regenCD;
    private float maxRegenCD;

    [Header("Inv")]
    public float invTime;
    private float invTimeMax;
    private bool invTriggered;

    [Header("Effects")]
    public GameObject hitSFX;

    // Start is called before the first frame update
    void Start()
    {
        maxRegenRate = regenRate;
        MaxHealth = Health;
        maxRegenCD = regenCD;
        invTimeMax = invTime;
    }

    // Update is called once per frame
    void Update()
    {
        shieldEnergy = plShield.shieldEnergy;
        healthText.text = Health + "%";
        shieldText.text = shieldEnergy + "%";
        if (HitRecently) ResetRegenCD();
        else if(Health < MaxHealth) Health += regenRate;


        if (invTriggered)
        {
            if (invTime <= 0)
            {
                invTriggered = false;
                invTime = invTimeMax;
            }
            else invTime -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
           float dmg = other.GetComponent<Projectile>().Damage;
           GetHit(dmg);
           Destroy(other.gameObject);
        }

        if (other.CompareTag("Spikes"))
        {
            float dmg = other.GetComponent<Spikes>().Damage;
            GetHit(dmg);
        }


    }


    public void GetHit(float dmg)
    {
        HitRecently = true;
        regenCD = maxRegenCD;
        CameraShaker.Instance.ShakeOnce(4f, 3f, 0f, .5f);

        if (!invTriggered)
        {
            //hitsfx
            Instantiate(hitSFX, transform.position, Quaternion.identity);
            if (Health > dmg)
            {
                invTriggered = true;
                HitScreenAnim.SetTrigger("Hit");
                Health -= dmg;
            }
            else
            {
                //else Death
                invTriggered = true;
                HitScreenAnim.SetTrigger("Hit");
                Health -= Health;
            }
        }
    }

    void ResetRegenCD()
    {
        if(regenCD < 0)
        {
            regenCD = maxRegenCD;
            HitRecently = false;
        }
        else
        {
            regenCD -= Time.deltaTime;
        }
    }

}
