using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public float fadeTime;
    private float fadeTimeMax;
    private bool startFade;
    public Animator Fader;

    // Start is called before the first frame update
    void Start()
    {
        fadeTimeMax = fadeTime;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (startFade)
        {
            if(fadeTime <= 0)
            {
                fadeTime = fadeTimeMax;
                SceneManager.LoadScene(1);
            }
            else
            {
                fadeTime -= Time.deltaTime;
            }
        }
    }


    public void StartGame()
    {
        if (!startFade) Fader.SetTrigger("FadeOut");
        startFade = true;
    }


    public void ExitGame()
    {
        Application.Quit();
    }
}
