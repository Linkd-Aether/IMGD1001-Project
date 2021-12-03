using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pellets;
    public Transform nodes;
    public Transform powerUps;
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
    public int level { get; private set; }
    //0.0 - 1.0
    public float volume;
    //0 - 10
    public int difficulty;

    public int xp;
    public int playerlvl;

    private int GHOSTXP = 15;
    private int PELLETXP = 1;
    private int POWERPELLETXP = 4;

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
        volume = (float)PlayerPrefs.GetInt("volume", 100) / 100;
        waka.volume = volume - 0.09f;
        audiostuff.volume = volume;
        powersound.volume = volume;
    }

    private void NextLevel() {
        if(this.level == 1) {
            this.level++;
            _uiManager.updateLevel(this.level);
            SceneManager.LoadScene("Lvl2");
        }
        else {
            NewRound();
        }
        
    }

    public void NewGame()
    {
        if(this.level > 1) {
            NewRound();
        }
        else {
            SetScore(0);
            SetXP(0);
            SetPlayerLevel(1);
            SetLives(3);
            NewRound();
        }

    }

    private void NewRound()
    {
        this.level++;
        _uiManager.updateLevel(this.level);
        foreach(Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }
        
        foreach(Transform powerUp in this.powerUps)
        {
            powerUp.gameObject.SetActive(true);
        }

        ResetState();
        
    }

    private void SetDifficulty() {
        for (int i = 0; i < ghosts.Length; i++)
        {
            this.ghosts[i].movement.speedMultiplier =  (1 + 0.1f * ((float)difficulty/10) * 2.0f);
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
        SetDifficulty();
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


    private void SetXP(int xp)
    {
       this.xp = xp;
       

        if(playerlvlup()) {
            this.xp = this.xp - 100;
            this.playerlvl++;
            _uiManager.updatePlayerLevel(this.playerlvl);
        }

        _uiManager.updateXP(this.xp);
    }

    private bool playerlvlup() {
        if(this.xp >= 100) {
            return true;
        }
        else {
            return false;
        }
    }

    private void SetPlayerLevel(int lvl)
    {
       this.playerlvl = lvl;
       _uiManager.updatePlayerLevel(lvl);
    }


    private void SetScore(int score)
    {
        this.score = score;
        _uiManager.updateScore(score);
    }

    public void SetLives(int lives)
    {
        this.lives = lives;
        _uiManager.updateLives(lives);
    }

    public void GhostEaten(Ghost ghost)
    {
        audiostuff.PlayOneShot(ghostEat);
        int points = ghost.points * this.ghostMultiplier;
        SetScore(this.score + points);
        SetXP(this.xp + GHOSTXP);
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
        SetXP(this.xp + PELLETXP);

        if (!HasRemainingPellets())
        {
            this.pacman.gameObject.SetActive(false);
            Invoke(nameof(NextLevel), 3.0f);
        }
    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        audiostuff.PlayOneShot(powerPelletEat);
        SetXP(this.xp + POWERPELLETXP);
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

    public void PowerUpEaten(PowerUp eaten){
        PlayEatSound();
        eaten.gameObject.SetActive(false);
        SetScore(this.score + eaten.score);
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

    public static Transform GetClosestObject (Transform origin, Transform objects)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = origin.position;
        foreach(Transform potentialTarget in objects)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if(dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
     
        return bestTarget;
    }

}
