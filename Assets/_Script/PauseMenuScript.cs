using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour {

    public GameObject PauseUI;

    private bool _paused = false;

    void Start()
    {
        PauseUI.SetActive(false);
    }

    void Update()
    {
        //暂停功能，如果按下ESC键或者暂停键则停止。
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _paused = !_paused;
        }


        if (_paused)
        {
            Time.timeScale = 0;
            PauseUI.SetActive(true);
        }
        //if (!_paused)
        else
        {
            Time.timeScale = 1;
            PauseUI.SetActive(false);
        }
    }
    public void Resume()
    {
        _paused = false;
        Debug.Log("resume");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Restart : " + SceneManager.GetActiveScene().name);
           
    }
    public void MainMenu()//回到主菜单
    {
        SceneManager.LoadScene(0);
        Debug.Log("go to main menu");
    }
    public void Quit()
    {
        Application.Quit();

    }

}

	

