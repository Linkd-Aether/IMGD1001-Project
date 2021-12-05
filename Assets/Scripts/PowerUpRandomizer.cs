using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpRandomizer : MonoBehaviour
{
    public PowerUp[] possibilities;

    public int respawnTimer = 0;

    private float levelMult;

    void Start() {
        int num = Random.Range(0, possibilities.Length);
        Instantiate(possibilities[num].gameObject, this.transform);

        levelMult = 1 + (InterLevelStats.powerupStat * 0.01f);
    }

    public void respawn(){
        if(respawnTimer > 0) Invoke(nameof(Start), respawnTimer / levelMult);
    }

    
}
