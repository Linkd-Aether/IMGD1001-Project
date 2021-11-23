using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public int finalscore;
    public int highscore;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _highscoreText;

    // Start is called before the first frame update
    void Start()
    {
        finalscore = PlayerPrefs.GetInt("finalscore");
        highscore = PlayerPrefs.GetInt("highscore");
        _highscoreText.text = "High Score: " + highscore;
        _scoreText.text = "Final Score: " + finalscore;
    }

}
