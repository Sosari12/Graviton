using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [Header("Choose")]
    public bool unParentAtStart;
    public bool destroyOverTime;
    public float timer;
    [Header("IfNoParent")]
    public bool DestroyIf_NoParent;
    public bool StopParticleIf_NoParent;
    public bool StopAudioIf_NoParent;

    // Start is called before the first frame update
    void Start()
    {
        if (unParentAtStart) transform.parent = null; 
        
    }

    // Update is called once per frame
    void Update()
    {
        if (destroyOverTime) Destroy(gameObject, timer);
        if (transform.parent == null)
        {
            if(DestroyIf_NoParent) Destroy(gameObject, timer);
            if(StopParticleIf_NoParent && transform.GetComponent<ParticleSystem>() != null) this.GetComponent<ParticleSystem>().Stop();
            if (StopAudioIf_NoParent && transform.GetComponent<AudioSource>() != null) this.GetComponent<AudioSource>().Stop();

        }

    }
}
