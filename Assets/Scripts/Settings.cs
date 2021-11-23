using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public int difficulty;
    public int volume;
    public Slider myslider;

    // Start is called before the first frame update
    void Start()
    {
        
        volume = PlayerPrefs.GetInt("volume", 100);
        myslider.value = volume;
        difficulty = PlayerPrefs.GetInt("difficulty", 0);
        //set to the correct value
    }

    // Update is called once per frame
    void Update()
    {
        //grab the new volume value from slider
        //PlayerPrefs.SetInt("volume", volume);

        //grab the new difficulty value from slider
        PlayerPrefs.SetInt("difficulty", difficulty);
    }
}
