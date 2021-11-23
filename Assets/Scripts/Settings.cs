using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public int difficulty;
    public int volume;

    // Start is called before the first frame update
    void Start()
    {
        
        volume = PlayerPrefs.GetInt("volume", 100);
        //set the slider to the correct value


        difficulty = PlayerPrefs.GetInt("difficulty", 0);
        //set to the correct value
    }

    // Update is called once per frame
    void Update()
    {
        //grab the new volume value from slider
        PlayerPrefs.SetInt("volume", volume);

        //grab the new difficulty value from slider
        PlayerPrefs.SetInt("difficulty", difficulty);
    }
}
