using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Movement movement { get; private set; }
    public GhostHome home { get; private set; }
    public GhostScatter scatter { get; private set; }
    public GhostChase chase { get; private set; }
    public GhostFrightened frightened { get; private set; }
    public GhostBehavior initialBehavior;
    public Transform target;
    public int points = 200;

    private float levelMult;

    private void Awake()
    {
        this.movement = GetComponent<Movement>();
        this.home = GetComponent<GhostHome>();
        this.scatter = GetComponent<GhostScatter>();
        this.chase = GetComponent<GhostChase>();
        this.frightened = GetComponent<GhostFrightened>();
    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        this.gameObject.SetActive(true);
        this.movement.ResetState();

        this.frightened.Disable();
        this.chase.Disable();
        this.scatter.Enable();

        if (this.home != this.initialBehavior)
        {
            this.home.Disable();
        }

        if (this.initialBehavior != null)
        {
            this.initialBehavior.Enable();
        }
    }

    public void SetPosition(Vector3 position)
    {
        // Keep the z-position the same since it determines draw depth
        position.z = this.transform.position.z;
        this.transform.position = position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman") && !this.frightened.eaten)
        {
            if (this.frightened.enabled)
            {
                FindObjectOfType<GameManager>().GhostEaten(this);
            }
            else
            {
                FindObjectOfType<GameManager>().PacmanEaten();
            }
        }
    }

    public void modifyDifficulty(){
        // Higher difficulty -> higher multiplier
        float difficultyMult = Mathf.Max((1 + ((float)InterLevelStats.difficulty - InterLevelStats.enemyStat/2) * 0.02f), 0);
        this.movement.speedMultiplier *= difficultyMult;
        this.chase.duration *= difficultyMult;
        
        this.frightened.duration *= 1/difficultyMult;
        this.scatter.duration *= 1/difficultyMult;
        this.home.duration *= 1/difficultyMult;
    }
}
