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
    private UIManager _uiManager;
    public ShakeBehavior shake;
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
    //0.0 - 1.0
    public float volume;
    //0 - 10

    private float levelXPMult;

    private const int XP_PER_LEVEL_BASE = 1000;

    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        PlayerPrefs.SetInt("finalscore", 0);
        volume = (float)PlayerPrefs.GetInt("volume", 100) / 100;
        waka.volume = volume;
        audiostuff.volume = volume;
        powersound.volume = volume;
        if (PlayerPrefs.GetInt("difficulty", 0) == 0) {
            InterLevelStats.difficulty = 1;
        }
        else if(PlayerPrefs.GetInt("difficulty", 0) == 1) {
            InterLevelStats.difficulty = 5;
        }
        else {
            InterLevelStats.difficulty = 8;
        }

        levelXPMult = 1 + (InterLevelStats.xpStat * 0.01f);
        
        NewGame();
        
    }

    private void Update()
    {
        if (InterLevelStats.lives <= 0 && Input.anyKeyDown)
        {
            NewGame();
        }
        if(Input.GetKeyDown(KeyCode.Escape)){        
            pauseMenu.Pause();            
        }
        if(Application.isEditor && Input.GetKeyDown(KeyCode.P)) {
            NextLevel();
        }
        if(Application.isEditor && Input.GetKeyDown(KeyCode.L)) {
            InterLevelStats.xp = InterLevelStats.xp + (int)Mathf.Ceil(XP_PER_LEVEL_BASE * (1 + (Mathf.Pow((InterLevelStats.playerlvl), 2) - 1 * 0.01f)));
        }
        volume = (float)PlayerPrefs.GetInt("volume", 100) / 100;
        waka.volume = volume - 0.09f;
        audiostuff.volume = volume;
        powersound.volume = volume;
    }

    private void NextLevel() {
        SceneManager.LoadScene("LevelUp");
        
    }

    public void NewGame()
    {
        InterLevelStats.level++;
        if(InterLevelStats.level > 1) {
            InterLevelStats.difficulty++;
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
        _uiManager.updateLevel(InterLevelStats.level);
        _uiManager.updatePlayerLevel(InterLevelStats.playerlvl);
        _uiManager.updateXP(InterLevelStats.xp);
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
            this.ghosts[i].modifyDifficulty();
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
        PlayerPrefs.SetInt("finalscore", InterLevelStats.score);
        if(InterLevelStats.score > PlayerPrefs.GetInt("highscore")) {
            PlayerPrefs.SetInt("highscore", InterLevelStats.score);
        }
        
        //change to gameover scene
        SceneManager.LoadScene("GameOver");
    }

    private void AddPoints(int points){
        points = (int) (points * levelXPMult);
        SetXP(InterLevelStats.xp + points);
        SetScore(InterLevelStats.score + points);
    }

    private void SetXP(int xp)
    {
       InterLevelStats.xp = xp;
       

        if(playerlvlup()) {
            Debug.Log("XP Before: " + InterLevelStats.xp);
            Debug.Log("XP Removed: " + (int)Mathf.Ceil(XP_PER_LEVEL_BASE * (1 + (Mathf.Pow((InterLevelStats.playerlvl - 1), 2) * 0.01f))));
            InterLevelStats.xp = InterLevelStats.xp - (int)Mathf.Ceil(XP_PER_LEVEL_BASE * (1 + (Mathf.Pow((InterLevelStats.playerlvl - 1), 2) * 0.01f)));
            Debug.Log("New XP Total: " + InterLevelStats.xp);
            InterLevelStats.playerlvl++;
            InterLevelStats.skillpoints++;
            _uiManager.updatePlayerLevel(InterLevelStats.playerlvl);
        }

        _uiManager.updateXP(InterLevelStats.xp);
    }

    private bool playerlvlup() {
        if(InterLevelStats.xp >= (int)Mathf.Ceil(XP_PER_LEVEL_BASE * (1 + (Mathf.Pow((InterLevelStats.playerlvl - 1), 2) * 0.01f)))) {
            return true;
        }
        else {
            return false;
        }
    }

    private void SetPlayerLevel(int lvl)
    {
       InterLevelStats.playerlvl = lvl;
       _uiManager.updatePlayerLevel(lvl);
    }


    private void SetScore(int score)
    {
        InterLevelStats.score = score;
        _uiManager.updateScore(score);
    }

    public void SetLives(int lives)
    {
        InterLevelStats.lives = lives;
        _uiManager.updateLives(lives);
    }

    public void GhostEaten(Ghost ghost)
    {
        audiostuff.PlayOneShot(ghostEat);
        int points = ghost.points * this.ghostMultiplier;
        AddPoints(points);
        this.ghostMultiplier++;
    }

    public void PacmanEaten()
    {
        waka.Pause();
        audiostuff.PlayOneShot(death);
        this.pacman.gameObject.SetActive(false);
        SetLives(InterLevelStats.lives - 1);
        this.pacman.CancelInvoke("unPhase");
        if(InterLevelStats.lives > 0)
        {
            shake.TriggerShake();
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
        AddPoints(pellet.points);

        if (!HasRemainingPellets())
        {
            this.pacman.gameObject.SetActive(false);
            Invoke(nameof(NextLevel), 3.0f);
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

    public void PowerUpEaten(PowerUp eaten){
        PlayEatSound();
        eaten.gameObject.SetActive(false);
        SetScore(InterLevelStats.score + eaten.score);
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
            if(dSqrToTarget < closestDistanceSqr && potentialTarget.gameObject.name != "Inside" && potentialTarget.gameObject.name != "Outside")
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
     
        return bestTarget;
    }

}
