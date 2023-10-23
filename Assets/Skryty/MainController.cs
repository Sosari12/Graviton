using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{
    public bool Return;
    private float timeReturn = 2f;
    public Animator Fader;


    // Start is called before the first frame update
    void Awake()
    {
        Application.targetFrameRate = 30;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Application.Quit();
            Return = true;
        }
        

        if(Return)ReturnToMenu();
    }

    public void ReturnToMenu()
    {
        if (Fader.GetBool("Dead") != true) Fader.SetBool("Dead", true);
        if (timeReturn <= 0) SceneManager.LoadScene(0);
        else timeReturn -= Time.deltaTime;
    }
}
