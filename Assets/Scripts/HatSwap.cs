using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatSwap : MonoBehaviour
{

    public List<Sprite> hats;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = hats[PlayerPrefs.GetInt("wornHat", 5)];
    }
}
