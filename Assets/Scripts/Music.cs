using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioSource music;
    // Start is called before the first frame update
    void Start()
    {
        music.volume = (float)PlayerPrefs.GetInt("volume", 100) / 100f;
    }
}
