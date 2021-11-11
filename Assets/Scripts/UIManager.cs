using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] private Text _scoreText;

    [SerializeField] private Text _livesText;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _livesText.text = "Lives: " + 3;
    }

    public void updateScore(int playerScore) {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void updateLives(int playerLives) {
        _livesText.text = "Lives: " + playerLives.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
