using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpRandomizer : MonoBehaviour
{
    public PowerUp[] possibilities;

    void Start() {
        int num = Random.Range(0, possibilities.Length);
        Instantiate(possibilities[num].gameObject, this.transform);
    }

    
}
