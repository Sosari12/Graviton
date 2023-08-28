using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadScript : MonoBehaviour
{
    //jeszcze inny skrypt z colliderem ktory wysyla ktory check point ma byc
    //skrypt dla obiektu ktory wykrywa czy gracz umarl i jak tak to ma wczytac zapisany checkpoint i go tam przeteleportowac

    public Transform playerTransform;
    public PlayerDamager player;
    public Transform[] currentCheckpoint;
    public int checkPointNumber;
    public string SceneName;


    private void Awake()
    {
        checkPointNumber = PlayerPrefs.GetInt("Checkpoint", 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        //wczytaj zapisane dane i jak jakieœ s¹ to teleport gracza na checkpoint

        playerTransform.position = currentCheckpoint[checkPointNumber].position;
        playerTransform.rotation = currentCheckpoint[checkPointNumber].rotation;
    }

    // Update is called once per frame
    void Update()
    {
        checkPointNumber = PlayerPrefs.GetInt("Checkpoint", 0);
        if (player.Health <= 0)
        {
            //resetSceny;
            SceneManager.LoadScene(SceneName);
        }
        //jezeli gracz martwy to reset


        //Do Testow
        if (Input.GetKeyDown(KeyCode.F2))
        {
            playerTransform.position = currentCheckpoint[0].position;
            playerTransform.rotation = currentCheckpoint[0].rotation;
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            playerTransform.position = currentCheckpoint[1].position;
            playerTransform.rotation = currentCheckpoint[1].rotation;
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            playerTransform.position = currentCheckpoint[2].position;
            playerTransform.rotation = currentCheckpoint[2].rotation;
        }

        if(Input.GetKeyDown(KeyCode.F5)) SceneManager.LoadScene(SceneName);
    }

   public void SetNewCheckPoint(int zap)
    {
        PlayerPrefs.SetInt("CheckPoint", zap);
    }
}
