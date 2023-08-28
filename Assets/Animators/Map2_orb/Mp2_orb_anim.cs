using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mp2_orb_anim : MonoBehaviour
{
    public GameObject soundBoom;
    public GameObject Stager;

    public void SpawnSoundBoom()
    {
        Instantiate(soundBoom, transform.position, Quaternion.identity);
        Stager.SetActive(true);
    }

}
