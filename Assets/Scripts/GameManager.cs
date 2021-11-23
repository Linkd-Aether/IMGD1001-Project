using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pellets;
    public UIManager _uiManager;
    public PauseMenu pauseMenu;
    public AudioClip eat1;
    public AudioClip eat2;
    public AudioClip death;
    public AudioClip ghostEat;
    public AudioClip powerPelletEat;
    public AudioClip powerPelletMode;
    
    public AudioSource waka;
    public AudioSource audiostuff;
    public AudioSource powersound;
    
    public bool playedEat1 = false;
    public bool playingPowerup = false;
    public int ghostMultiplier { get; private set; } = 1;
    public int score { get; private set; }
    public int lives { get; private set; }
    public int level { get; private set; } = 0;
    //0.0 - 1.0
    public float volume;
    //0 - 10
    public int difficulty;

    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        PlayerPrefs.SetInt("finalscore", 0);
        volume = (float)PlayerPrefs.GetInt("volume", 100) / 100;
        waka.volume = volume;
        audiostuff.volume = volume;
        powersound.volume = volume;
        if (PlayerPrefs.GetInt("difficulty", 0) == 0) {
            difficulty = 1;
        }
        else if(PlayerPrefs.GetInt("difficulty", 0) == 1) {
            difficulty = 5;
        }
        else {
            difficulty = 8;
        }

        NewGame();
    }

    private void Update()
    {
        if (this.lives <= 0 && Input.anyKeyDown)
        {
            NewGame();
        }
        if(Input.GetKeyDown(KeyCode.Escape)){        
            pauseMenu.Pause();            
        }
    }
    public void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void NewRound()
    {
        this.level++;
        _uiManager.updateLevel(this.level);
        foreach(Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }
        

        ResetState();
        SetDifficulty();
    }

    private void SetDifficulty() {
        for (int i = 0; i < ghosts.Length; i++)
        {
            this.ghosts[i].movement.speedMultiplier =  (1 + 0.1f * ((float)difficulty/10) * 2.5f);
        }
    }

    private void ResetState()
    {
        ResetGhostMultiplier();

        for (int i = 0; i < ghosts.Length; i++)
        {
            this.ghosts[i].ResetState();
        }

        this.pacman.ResetState();
    }

    private void GameOver()
    {
        for (int i = 0; i < ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(false);
        }

        this.pacman.gameObject.SetActive(false);

        //save score
        PlayerPrefs.SetInt("finalscore", score);
        if(score > PlayerPrefs.GetInt("highscore")) {
            PlayerPrefs.SetInt("highscore", score);
        }
        
        //change to gameover scene
        SceneManager.LoadScene("GameOver");
    }

    private void SetScore(int score)
    {
        this.score = score;
        _uiManager.updateScore(score);
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
        _uiManager.updateLives(lives);
    }

    public void GhostEaten(Ghost ghost)
    {
        audiostuff.PlayOneShot(ghostEat);
        int points = ghost.points * this.ghostMultiplier;
        SetScore(this.score + points);
        this.ghostMultiplier++;
    }

    public void PacmanEaten()
    {
        waka.Pause();
        audiostuff.PlayOneShot(death);
        this.pacman.gameObject.SetActive(false);
        SetLives(this.lives - 1);
        if(this.lives > 0)
        {
            Invoke(nameof(ResetState), 3.0f);
            
        }
        else
        {
            GameOver();
        }
    }

    public void PlayEatSound(){
        if(playedEat1) {
            //play eat2
            audiostuff.PlayOneShot(eat2, 0.5f);
            playedEat1 = false;
        }
        else{
            //play eat 1
            audiostuff.PlayOneShot(eat1, 0.5f);
            playedEat1 = true;
        }
    }

    public void PelletEaten(Pellet pellet)
    {
        PlayEatSound();
        pellet.gameObject.SetActive(false);
        SetScore(this.score + pellet.points);

        if (!HasRemainingPellets())
        {
            this.pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3.0f);
        }
    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        audiostuff.PlayOneShot(powerPelletEat);
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].frightened.Enable(pellet.duration);
        }
        PelletEaten(pellet);
        if(!playingPowerup) {
            powersound.mute = false;
            powersound.Play();
            waka.Pause();
            playingPowerup = true;
        }

        
        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);
    }

    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in this.pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;

    }

    private void ResetGhostMultiplier()
    {
        playingPowerup = false;

        waka.UnPause();
        powersound.Pause();
        powersound.mute = true;
        this.ghostMultiplier = 1;
    }

}
