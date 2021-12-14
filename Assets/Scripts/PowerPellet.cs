using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPellet : Pellet
{
    protected override void Eat()
    {
        try{
            FindObjectOfType<GameManager>().PowerPelletEaten(this);
        }
        catch(NullReferenceException e){
            e.GetHashCode(); // Hack to suppress warnings :|
            FindObjectOfType<GameManagerOriginal>().PowerPelletEaten(this);
        }
    }
    public float duration = 8.0f;
}
