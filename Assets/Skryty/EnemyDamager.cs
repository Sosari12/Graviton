using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : MonoBehaviour
{
    public float Health;
    private Spawn_Enemy spawner;
    private bool dead;
    public GameObject ObjSpawner;
    //SFX
    //VFX
    public GameObject destroyVFX;
    public StageSpawner stager;
    public Animator anim;


    private void Awake()
    {
        if (ObjSpawner == null) ObjSpawner = GameObject.Find("Spawner");
        spawner = ObjSpawner.GetComponent<Spawn_Enemy>();
        GetComponent<EnemyBehaviour>().spawner = spawner;
    }

    void Start()
    {

    }

    void Update()
    {
       
    }



    public void TakeDamage(int damage)
    {
        anim.SetTrigger("Hit");
        Health -= damage;
        //SFX
        //VFX
        if (Health <= 0 && !dead)
        {
            Death();
            dead = true;
        }

    }


    public void Death()
    {
        anim.SetBool("Dead", true);
        anim.SetTrigger("Die");
        spawner.enemiesOverAll--;
        if (stager != null) stager.defeatedEnemies++;

        ///Instantiate(destroyVFX, transform.position, Quaternion.identity);

        Destroy(gameObject, 1f);

    }

}
