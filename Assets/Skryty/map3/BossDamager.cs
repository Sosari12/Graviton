using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamager : MonoBehaviour
{
    /*
     Du¿o hp np. 2000
     rozbicie pilarow lub oczu zadaje np 125
     jak straci 500 hp to nastepna faza
     */

    public float BossHP;
    public float[] HpFaz;
    public float okoHP;
    public float ileMaZadac;
    public int faza;
    private bool Dead;
    private Animator anim;
    public Animator lightAnim;
    public Animator bossAnimator;

    //tymczasowe zamienic to pozniej na animSkrypt dla r¹k aby oczy siê poprostu otwiera³y a nie jak teraz acrive
    public GameObject[] oczy;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (faza == 1) bossAnimator.SetBool("Faze1", true);
        else bossAnimator.SetBool("Faze1", false);

        if (faza == 2) bossAnimator.SetBool("Faze2", true);
        else bossAnimator.SetBool("Faze2", false);

        if (!Dead && BossHP <= 0)
        {
            CheckIfDead();
        }

        //cheats
        BossCheat();
    }

    public void OdejmijHP(float ile)
    {
        BossHP -= ile;
        anim.SetTrigger("Hit");
        lightAnim.SetTrigger("Hit");

        //to samo dla koloru swiatla
        if (faza == 1 || faza == 2 && BossHP == HpFaz[faza] - ileMaZadac)
        {
            bossAnimator.SetTrigger("Hurt");
        }

        
    }

    public void ZmienFaze()
    {
        if (BossHP == HpFaz[faza] - ileMaZadac)
        {
            if (faza == 0) bossAnimator.SetTrigger("Enter");
            if (faza == 1) bossAnimator.SetTrigger("Enter");
            faza++;
            if (faza == 1)
            {
                oczy[0].SetActive(true);
                oczy[1].SetActive(true);
            }
            if(faza == 2)
            {
                oczy[2].SetActive(true);
                oczy[3].SetActive(true);
                oczy[4].SetActive(true);
                oczy[5].SetActive(true);
            }
        }
    }

    public void BossCheat()
    {
        if (Input.GetKeyDown(KeyCode.F9))
        {
            if(faza == 0 || faza == 1)
            {
                bossAnimator.SetTrigger("Enter");
                faza++;
            }
            //OdejmijHP(0);
            if (faza == 2) OdejmijHP(BossHP);
        }
    }

    private void CheckIfDead()
    {
        Dead = true;
        anim.SetBool("Dead", true);
        anim.SetTrigger("Die");
        lightAnim.SetBool("Dead", true);
        lightAnim.SetTrigger("Die");
        bossAnimator.SetBool("Dead", true);
        bossAnimator.SetTrigger("Die");
    }
}
