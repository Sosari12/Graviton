using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EZCameraShake;

public class Shooting : MonoBehaviour
{
    public Camera cam;

    public LayerMask IgnoreMe;

    [Header("Data")]
    private Vector3 destination;
    public GameObject projectile;
    public Transform GunPoint;
    public float projectileSpeed;
    public float timeToFire;
    private float maxTimeToFire;
    public float fireRate;
    public int Damage;
    bool readyToFire = true;

    [Header("Effects")]
    //public GameObject shootParticle;
    public ParticleSystem shootParticle;
    public ParticleSystem electricityReload;
    public ParticleSystem ReloadSmoke01;
    public ParticleSystem ReloadSmoke02;

    [Header("Animations")]
    public PlayerHandScript handAnimScr;
    private Animator handAnimator;
    int randomShoot = 0;
    //public GameObject CameraShaker;

    [Header("AmmoCount")]
    public TMP_Text ammoCountText;
    public float ammoProcent;

    [Header("Shield")]
    bool shieldActive;
    public PlayerShield plShield;



    // Start is called before the first frame update
    void Start()
    {
        maxTimeToFire = timeToFire;
        handAnimator = handAnimScr.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ammoCountText.text = ammoProcent + "%";
        int randomShoot = 0;
        if (!readyToFire) resetShoot();
        if (handAnimator.GetBool("Reloading"))
        {
            if (ammoProcent < 100) ammoProcent += 5;
        }


        if (Input.GetButton("Fire1") && readyToFire && !handAnimator.GetBool("Reloading") && !handAnimator.GetBool("Shooting") && ammoProcent > 0 && !plShield.shieldActive)// && Time.time >= timeToFire
        {
            
            //timeToFire += Time.time + 1 / fireRate;
            handAnimator.SetBool("Shooting", true);
            randomShoot = Random.Range(0, 3);
            if(randomShoot == 0)handAnimator.SetTrigger("Shoot1");
            if(randomShoot == 1)handAnimator.SetTrigger("Shoot2");
            if (randomShoot == 2) handAnimator.SetTrigger("Shoot3");
            //ShootProjectile();
        }
        else if (Input.GetButtonDown("Fire1") && readyToFire && !handAnimator.GetBool("Reloading") && !handAnimator.GetBool("Shooting") && ammoProcent <= 0 && !plShield.shieldActive)
        {
            Reload();
        }


        if (Input.GetKeyDown(KeyCode.R) && !handAnimator.GetBool("Reloading") && ammoProcent < 100)
        {
            Reload();

        }


        if (Input.GetMouseButton(1) && !plShield.shieldOnCD)
        {
            //wlaczanie boola anim
            //anim scrypt ktory wlacza i wylacza shieldactive

            //plShield.shieldActive = true;
            handAnimator.SetBool("ShieldHold", true);

        }
        if (Input.GetMouseButtonUp(1) || plShield.shieldOnCD)
        {
            //tu bedzie wylaczanie boola anim
            //anim scrypt ktory wlacza i wylacza shieldactive

            //plShield.shieldActive = false;
            handAnimator.SetBool("ShieldHold", false);

        }

    }

    void resetShoot()
    {
        if(timeToFire <= 0)
        {
            timeToFire = maxTimeToFire;
            readyToFire = true;
        }
        else
        {
            timeToFire -= Time.deltaTime;
        }
    }

    public void ShootProjectile()
    {
        CameraShaker.Instance.ShakeOnce(2f, 1f, 0f, .5f);
        readyToFire = false;
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, ~IgnoreMe))
        {
            destination = hit.point;
        }
        else
        {
            destination = ray.GetPoint(1000f);
        }

        InstantiateProject();
    }

    void InstantiateProject()
    {
        var projectileObj = Instantiate(projectile, GunPoint.position, Quaternion.identity) as GameObject;
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - GunPoint.position).normalized * projectileSpeed;
        projectileObj.GetComponent<Projectile>().Damage = Damage;
        //var vfxShoot = Instantiate(shootParticle, GunPoint.position, Quaternion.identity) as GameObject;
        shootParticle.Play();
    }


    void Reload()
    {
        handAnimator.SetTrigger("Reload");
        electricityReload.Play();
        ReloadSmoke01.Play();
        ReloadSmoke02.Play();
    }
}
