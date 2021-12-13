using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passage : MonoBehaviour
{
    public Transform connection;

    public int teleportCount;

    private void Start(){
        teleportCount = 0;
    }

    private void Update(){
        if(teleportCount > 0) teleportCount--;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(teleportCount <= 0){
            Vector3 position = collision.transform.position;
            position.x = this.connection.position.x;
            position.y = this.connection.position.y;
            collision.transform.position = position;
        
            if(this.gameObject.tag == "Portal"){
                teleportCount = 60;
                this.connection.GetComponent<Passage>().teleportCount = 60;
            }
        }
    }
}
