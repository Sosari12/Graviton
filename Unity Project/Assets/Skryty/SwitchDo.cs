using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchDo : MonoBehaviour
{
    public GameObject SmallOrb;
    public AudioSource selfSoundSource;
    [Header("Chose which")]
    public bool spawnOrb;
    public bool spawnStoryOrb;


    private bool doneSmthingOnce;
    public Transform spawnPoint;
    [Header("Checker")]
    public bool playerInRange;
    public DoorTPScript drzwi;
    public GameObject startLight;

    // Start is called before the first frame update
    void Start()
    {
        if(this.GetComponent<AudioSource>())selfSoundSource.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            DoSomething();
        }
    }


    public void DoSomething()
    {
        if (spawnOrb && !doneSmthingOnce)
        {
            Instantiate(SmallOrb, spawnPoint.position, transform.rotation);
            doneSmthingOnce = true;
            drzwi.aktywowanePilary += 1;
            startLight.SetActive(false);
            if(selfSoundSource != null) selfSoundSource.Stop();
        }
        if(playerInRange && spawnStoryOrb)
        {
            Instantiate(SmallOrb, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

}
