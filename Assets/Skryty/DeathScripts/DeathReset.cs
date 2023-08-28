using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathReset : MonoBehaviour
{
    public Transform player;
    public Transform teleportTo;
    public GameObject particles;
    public bool triggerTP;
    public Animator BlackScreen;
    //black screen trigger
    public float timeToTP;
    private float maxTimeToTP;

    // Start is called before the first frame update
    void Start()
    {
        maxTimeToTP = timeToTP;
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerTP)
        {
            TeleportPlayer();
        }
    }


    public void TeleportPlayer()
    {
        if(timeToTP <= 0)
        {
            
            player.rotation = teleportTo.rotation;
            player.position = teleportTo.position;
            BlackScreen.SetBool("Dead", false);
            SpawnParticles();
            timeToTP = maxTimeToTP;
            triggerTP = false;
        }
        else
        {
            if(!BlackScreen.GetBool("Dead")) BlackScreen.SetBool("Dead", true);
            timeToTP -= Time.deltaTime;
        }
    }

    public void SpawnParticles()
    {
        Instantiate(particles, teleportTo.position, Quaternion.identity);
    }
}
