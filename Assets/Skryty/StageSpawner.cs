using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSpawner : MonoBehaviour
{
    public Spawn_Enemy enemySpawner;
    public bool Active;
    public int enemyCount;
    public int enemiesToSpawn;
    public int defeatedEnemies;
    public bool done;
    public GameObject NextStage;

    //respawn times
    public float respawnRate;
    private float respawnRateMax;
    private bool notUpToSpawm;

    // Start is called before the first frame update
    void Start()
    {
        respawnRateMax = respawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (defeatedEnemies >= enemiesToSpawn && !done)
        {
            done = true;
            enemySpawner.activeStager = null;
            Active = false;
            enemySpawner.enemyCount = 0;
            if(NextStage != null)NextStage.SetActive(true);
        }

        if (notUpToSpawm)
        {
            resetSpawnTime();
        }
        else
        {
            if (Active && enemyCount < enemiesToSpawn)
            {
                enemyCount = enemySpawner.enemyCount;
                enemySpawner.activeStager = this.GetComponent<StageSpawner>();
                enemySpawner.SearchSpawnPoint();
                if(respawnRate > 0 && !enemySpawner.searching)notUpToSpawm = true;
            }
        }

        //cant go further if not defeated enough

    }



    void resetSpawnTime()
    {
        if(respawnRate <= 0)
        {
            respawnRate = respawnRateMax;
            notUpToSpawm = false;
        }
        else
        {
            respawnRate -= Time.deltaTime;
        }
    }

}
