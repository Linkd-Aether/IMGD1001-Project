using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpRandomizer : MonoBehaviour
{
    public PowerUp[] possibilities;

    public int respawnTimer = 0;

    void Start() {
        int num = Random.Range(0, possibilities.Length);
        Instantiate(possibilities[num].gameObject, this.transform);
    }

    public void respawn(){
        if(respawnTimer > 0) Invoke(nameof(Start), respawnTimer);
    }

    
}
