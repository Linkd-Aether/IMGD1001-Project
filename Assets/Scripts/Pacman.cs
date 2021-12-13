using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Pacman : MonoBehaviour
{
    public Movement movement { get; private set; }
    private const float FAST_RATIO = 1.5f;
    private UIManager _uiManager;
    private float levelSpeedMult;

    public PortalBullet portalBullet;
    public GameObject orangePortal;
    private int numPortals;

    private void Awake()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        this.movement = GetComponent<Movement>();

        levelSpeedMult = 1 + (InterLevelStats.speedStat * 0.02f);
        this.movement.speedMultiplier *= levelSpeedMult;
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
        
        if(numPortals > 0 && this.movement.checkOccupied == true && (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.RightControl))){
            shootPortal();
        }

        float angle = Mathf.Atan2(this.movement.direction.y, this.movement.direction.x);

        this.transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    public void ResetState()
    {
        this.movement.ResetState();
        this.gameObject.SetActive(true);
        Physics2D.IgnoreLayerCollision(6, 9, false);
        resetText();
    }

    public void powerUp(string type){
        if(InterLevelStats.usedPowerUps.Contains(type)) InterLevelStats.usedPowerUps.Add(type);
        if(InterLevelStats.usedPowerUps.Count == 4) PlayerPrefs.SetInt("hat2", 1);

        if(type.Equals("Phase")){
            Physics2D.IgnoreLayerCollision(6, 9);
            this.movement.checkOccupied = false;
            _uiManager.updatePowerUp(type);
            Invoke(nameof(unPhase), 10);
        }
        if(type.Equals("Extra Life")){
            GameManager manager = FindObjectOfType<GameManager>();
            manager.SetLives(InterLevelStats.lives + 1);
            _uiManager.updatePowerUp(type);
            Invoke(nameof(resetText), 2);
        }
        if(type.Equals("Fast")){
            this.movement.speedMultiplier *= FAST_RATIO;
            _uiManager.updatePowerUp(type);
            Invoke(nameof(unFast), 10);
        }
        if(type.Equals("Portal")){
            numPortals = 2;
            _uiManager.updatePowerUp(type);
        }
    }
    
    private void unPhase(){
        
        Transform toTP = GameManager.GetClosestObject(this.transform, FindObjectOfType<GameManager>().nodes);
        
        this.transform.position = new Vector3(toTP.position.x, toTP.position.y, this.transform.position.z);

        Physics2D.IgnoreLayerCollision(6, 9, false);
        this.movement.checkOccupied = true;
        resetText();
    }

    private void unFast(){
        this.movement.speedMultiplier /= FAST_RATIO;
        resetText();
    }

    private void resetText(){
        _uiManager.updatePowerUp("None");
    }
    
    private void shootPortal(){
        PortalBullet portalProj = Instantiate(portalBullet, this.gameObject.transform.position, this.gameObject.transform.rotation);
        portalProj.GetComponent<Rigidbody2D>().AddForce(this.movement.direction * 5, ForceMode2D.Impulse);
        if(numPortals == 2){
            portalProj.GetComponent<SpriteRenderer>().color = Color.yellow;
            portalProj.Inst("Orange", orangePortal);
        }
        if(numPortals == 1){
            portalProj.GetComponent<SpriteRenderer>().color = Color.blue;
            portalProj.Inst("Blue", orangePortal);
        }

        numPortals--;
        if(numPortals == 0)
            resetText();
    }
}
