using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] private Text _scoreText;

    [SerializeField] private Text _livesText;

    [SerializeField] private Text _levelText;

    [SerializeField] private Text _xpText;

    [SerializeField] private Text _playerLevelText;

    [SerializeField] private Text _powerupText;

    // Start is called before the first frame update
    void Start()
    {
        // _scoreText.text = "Score: " + 0;
        // _livesText.text = "Lives: " + 3;
        // _levelText.text = "Level " + 1;
        // _xpText.text = "Experience: " + 0;
        // _playerLevelText.text = "Player Level: " + 1;
    }

    public void updateScore(int playerScore) {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void updateLives(int playerLives) {
        _livesText.text = "Lives: " + playerLives.ToString();
    }

    public void updateLevel(int level) {
        _levelText.text = "Level " + level.ToString();
    }

    public void updateXP(int xp) {
       _xpText.text = "Expierence: " + xp.ToString();
    }
    
    public void updatePlayerLevel(int plevel) {
       _playerLevelText.text = "Player Level: " + plevel.ToString();
    }

    public void updatePowerUp(string str) {
       _powerupText.text = "" + str;
    }
}
