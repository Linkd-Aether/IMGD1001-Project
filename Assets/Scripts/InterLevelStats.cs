using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterLevelStats : MonoBehaviour
{
    public static int score { get; set; }
    public static int lives { get; set; }
    public static int level { get; set; }
    public static int xp { get; set; }
    public static int playerlvl { get; set; }
    public static int difficulty { get; set; }
    public static int skillpoints {get; set; }
    public static int speedStat {get; set; }
    public static int enemyStat {get; set; }
    public static int xpStat {get; set; }
    public static int powerupStat {get; set; }

    void Awake(){
        DontDestroyOnLoad(this.gameObject);
    }

    void Reset(){
        score = lives = level = xp = playerlvl = difficulty = skillpoints = speedStat = enemyStat = xpStat = powerupStat = 0;
    }

}
