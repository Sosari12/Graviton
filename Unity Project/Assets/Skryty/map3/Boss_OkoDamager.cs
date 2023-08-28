using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_OkoDamager : MonoBehaviour
{
    //zbijasz hp oka i gdy hp spadnie do 0 to znisczone anim tez i nie mozna zadawac dmg
    //gdy nowa faza wchodzi to wtedy hp sie odnawia i mozna zniszczyc
    //nie mozna zadawac hp gdy zamkniete

    public float okoHP;
    private float okoHPMax;
    public bool zniszczone;
    public bool zamkniete;
    public bool letActive;
    public Animator okoAnim;
    public BossDamager father;
    public GameObject RedOrb;

    [Header("Spawner")]
    public bool wlSpawner;
    public GameObject spawner;


    // Start is called before the first frame update
    void Start()
    {
        okoAnim = GetComponent<Animator>();
        //if(father.okoHP > 0 && getHPFromFather) okoHP = father.okoHP;
        okoHPMax = okoHP;

    }

    // Update is called once per frame
    void Update()
    {
        if (!zniszczone) CheckHp();

        /*if(father.faza == 2)
        {
            zniszczone = false;
            okoHP = okoHPMax;
        }
        */
        //sprawdza czy hp spadlo do 0 i jak tak to juz nie sprawdza i zadaje dmg bossowi;
    }

    public void OdejmijHP(float ile)
    {
        if(!zniszczone && !zamkniete)okoHP -= ile;
        
    }


    public void CheckHp()
    {
        if(okoHP <= 0)
        {
            //father.OdejmijHP(okoHPMax);
            //father.ZmienFaze();
            zniszczone = true;
            GameObject orbObj = Instantiate(RedOrb, transform.position, Quaternion.identity);
            orbObj.GetComponent<RedOrb_ToDmgBoss>().setDmg(okoHPMax);
            if (wlSpawner) spawner.SetActive(true);
            if(!letActive) gameObject.SetActive(false);
        }
    }
}
