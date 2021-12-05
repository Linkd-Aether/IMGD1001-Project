using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterLevelStats : MonoBehaviour
{
    // Current stage being played
    public static int level { get; set; }

    // Total XP gained over the course of the playthrough. Saved as a high score
    public static int score { get; set; }

    // Remaining lives. Game over at 0 lives
    public static int lives { get; set; }

    // Current XP. Resets at 1000, and grants 1 level
    public static int xp { get; set; }
    
    // Current character level. 1 level is gained for every 1000 XP. Each level-up grants 1 skill point
    public static int playerlvl { get; set; }

    // How difficult the game is, chosen by the player. Increases/decreases enemy speed  
    public static int difficulty { get; set; }

    // Current number of allocatable skill points, spendable on increasing stats
    public static int skillpoints {get; set; }

    // Increases movement speed
    public static int speedStat {get; set; }

    // Decreases enemy speed and increases time spent in Home, Frightened, and Scatter behaviors
    public static int enemyStat {get; set; }

    // Increases XP gained
    public static int xpStat {get; set; }

    // Increases the length and respawn time of power-ups
    public static int powerupStat {get; set; }

    void Awake(){
        DontDestroyOnLoad(this.gameObject);
    }

    void Reset(){
        score = lives = level = xp = playerlvl = difficulty = skillpoints = speedStat = enemyStat = xpStat = powerupStat = 0;
    }

}
