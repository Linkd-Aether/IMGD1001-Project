using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] GameObject pauseMenu;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){        
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;         
        }

    }

    public void Pause(){
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.audiostuff.Pause();
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResetGame(){
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gm.NewGame();
    }

    public void Resume(){
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.audiostuff.Play();
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void MainMenu(){
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    public void Quit(){
        Application.Quit();
    }


}
