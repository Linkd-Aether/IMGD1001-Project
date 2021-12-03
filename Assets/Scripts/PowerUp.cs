using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public string powerName;
    public int score;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            collision.GetComponent<Pacman>().powerUp(powerName);
            FindObjectOfType<GameManager>().PowerUpEaten(this);
            GetComponentInParent<PowerUpRandomizer>().respawn();
        }
    }
}
