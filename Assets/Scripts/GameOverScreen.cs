using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public int finalscore;
    [SerializeField] private Text _scoreText;

    // Start is called before the first frame update
    void Start()
    {
        finalscore = PlayerPrefs.GetInt("finalscore");
        _scoreText.text = "Final Score: " + finalscore;
    }

}
