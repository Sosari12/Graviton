using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedOrb_ToDmgBoss : MonoBehaviour
{
    public float ileMamDmg;
    private Transform target;
    public string coSzukac;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find(coSzukac).transform;
    }

    // Update is called once per frame
    void Update()
    {
        //orb ma leciec do obiektu bedacego celem
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed);
    }


    public void setDmg(float ile)
    {
        ileMamDmg = ile;
    }

    //to juz boss bedzie mial trigger jak zetknie sie z orbem;


    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Boss"))
        {
            other.GetComponent<BossDamager>().OdejmijHP(ileMamDmg);
            other.GetComponent<BossDamager>().ZmienFaze();
            Destroy(gameObject);
        }

    }


}
