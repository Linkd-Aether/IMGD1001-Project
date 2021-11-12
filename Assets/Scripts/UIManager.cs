using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] private Text _scoreText;

    [SerializeField] private Text _livesText;

    [SerializeField] private Text _levelText;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _livesText.text = "Lives: " + 3;
        _levelText.text = "Level " + 1;
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

    // Update is called once per frame
    void Update()
    {

    }
}
