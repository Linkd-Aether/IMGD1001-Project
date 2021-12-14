using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    public int points = 10;

    protected virtual void Eat()
    {
        try{
            FindObjectOfType<GameManager>().PelletEaten(this);
        }
        catch(NullReferenceException e){
            e.GetHashCode(); // Hack to suppress warnings :|
            FindObjectOfType<GameManagerOriginal>().PelletEaten(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            Eat();
        }
    }
}
