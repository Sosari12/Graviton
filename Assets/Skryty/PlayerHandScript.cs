using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandScript : MonoBehaviour
{
    private Shooting player;
    private Animator anim;
    public PlayerShield plShield;

    [Header("Audio")]
    public AudioSource reloadSource;
   // public GameObject shieldUpSFX;
    //public GameObject shieldDownSFX;
    public AudioSource shieldSFXSource;
    public AudioClip shieldUpClip;
    public AudioClip shieldDownClip;

    //random idle
    private int randomIdle;
    private int prevIdle;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Shooting").GetComponent<Shooting>();
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShootProjectile()
    {
        player.ShootProjectile();
        player.ammoProcent -= 10;
    }


    public void BoolShootingTrue()
    {
        anim.SetBool("Shooting", true);
    }

    public void BoolShootingFalse()
    {
        anim.SetBool("Shooting", false);
    }

    public void BoolReloading()
    {
        if (anim.GetBool("Reloading"))
        {
            anim.SetBool("Reloading", false);
        }
        else
        {
            anim.SetBool("Reloading", true);
        }
    }

    public void BoolShieldTrue()
    {
        anim.SetBool("ShieldUp", true);
    }

    public void BoolShieldFalse()
    {
        anim.SetBool("ShieldUp", false);
    }

    public void ShieldDestroyTrue()
    {
        anim.SetBool("ShieldDestroy", true);
    }

    public void ShieldDestroyFalse()
    {
        anim.SetBool("ShieldDestroy", false);
    }

    public void ShieldActiveTrue()
    {
        plShield.shieldActive = true;
    }

    public void ShieldActiveFalse()
    {
        plShield.shieldActive = false;
    }

    public void ShieldOnCD()
    {
        player.plShield.shieldOnCD = true;
    }

    public void RandomIdle()
    {
        randomIdle = Random.Range(0, 3);
        if (randomIdle == 1) anim.SetTrigger("Idle2");
        if (randomIdle == 2) anim.SetTrigger("Idle3");
    }


    /// --- sounds ---
    /// 

    public void ReloadSound()
    {
        reloadSource.Play();
    }

    public void ShieldUpSound()
    {
        //Instantiate(shieldUpSFX, transform.position, Quaternion.identity);
        shieldSFXSource.clip = shieldUpClip;
        shieldSFXSource.Play();
    }

    public void ShieldDownSound()
    {
        //Instantiate(shieldDownSFX, transform.position, Quaternion.identity);
        shieldSFXSource.clip = shieldDownClip;
        shieldSFXSource.Play();

    }

}
