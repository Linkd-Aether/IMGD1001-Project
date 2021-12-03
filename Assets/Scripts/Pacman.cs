using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Pacman : MonoBehaviour
{
    public Movement movement { get; private set; }
    private const float FAST_RATIO = 1.5f;
    private void Awake()
    {
        this.movement = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)){
            this.movement.SetDirection(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)){
            this.movement.SetDirection(Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)){
            this.movement.SetDirection(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)){
            this.movement.SetDirection(Vector2.right);
        }

        float angle = Mathf.Atan2(this.movement.direction.y, this.movement.direction.x);

        this.transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    public void ResetState()
    {
        this.movement.ResetState();
        this.gameObject.SetActive(true);
        Physics2D.IgnoreLayerCollision(6, 9, false);
    }

    public void powerUp(string type){
        if(type.Equals("Phase")){
            Physics2D.IgnoreLayerCollision(6, 9);
            this.movement.checkOccupied = false;
            Invoke(nameof(unGhost), 10);
        }
        if(type.Equals("Extra Life")){
            GameManager manager = FindObjectOfType<GameManager>();
            manager.SetLives(InterLevelStats.lives + 1);
        }
        if(type.Equals("Fast")){
            this.movement.speedMultiplier *= FAST_RATIO;
            Invoke(nameof(unFast), 10);
        }
    }
    
    private void unGhost(){
        
        Transform toTP = GameManager.GetClosestObject(this.transform, FindObjectOfType<GameManager>().nodes);
        
        this.transform.position = new Vector3(toTP.position.x, toTP.position.y, this.transform.position.z);

        Physics2D.IgnoreLayerCollision(6, 9, false);
        this.movement.checkOccupied = true;
    }

    private void unFast(){
        this.movement.speedMultiplier /= FAST_RATIO;
    }
}
