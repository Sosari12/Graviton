using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Enemy : MonoBehaviour
{
    public float spawnPointRange;
    public LayerMask whatIsGround;
    public Vector3 spawnPoint;
    public GameObject Enemy;
    public int enemyCount;
    public int enemiesOverAll;
    public bool searching;
    public StageSpawner activeStager;
    public bool PlayerSpawner;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (searching) SearchSpawnPoint();


        if (Input.GetKeyDown(KeyCode.F1) && !searching && PlayerSpawner)
        {
            searching = true;
        }
    }





    public void SearchSpawnPoint()
    {
        float randomZ = Random.Range(-spawnPointRange, spawnPointRange);
        float randomX = Random.Range(-spawnPointRange, spawnPointRange);
        float randomY = Random.Range(-spawnPointRange, spawnPointRange);

        spawnPoint = new Vector3(transform.position.x + randomX, transform.position.y + randomY, transform.position.z + randomZ);

        if (Physics.Raycast(spawnPoint, -transform.up, 2f, whatIsGround))
        {
            GameObject enemyObj = Instantiate(Enemy, spawnPoint, Quaternion.identity);
            if(activeStager != null)enemyObj.GetComponent<EnemyDamager>().stager = activeStager;
            enemyCount++;
            enemiesOverAll++;
            searching = false;
        }
    }



}
