using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpManager : MonoBehaviour
{

    [SerializeField] private Text remainingPoints;
    [SerializeField] private Text speedStatCount;
    [SerializeField] private Text powerupStatCount;
    [SerializeField] private Text enemyStatCount;
    [SerializeField] private Text xpStatCount;

    private void Start() {
        updatePoints(InterLevelStats.skillpoints);
    }

    public void updatePoints(int points) {
        remainingPoints.text = "You have " + points.ToString() + " remaining.";
    }

    public void updateStatCounts() {
        speedStatCount.text = "" + InterLevelStats.speedStat.ToString();
        enemyStatCount.text = "" + InterLevelStats.enemyStat.ToString();
        xpStatCount.text = "" + InterLevelStats.xpStat.ToString();
        powerupStatCount.text = "" + InterLevelStats.powerupStat.ToString();
    }

    public void speedStatUp() {
        
        if(InterLevelStats.skillpoints > 0) {
            InterLevelStats.skillpoints--;
            InterLevelStats.speedStat++;
            updatePoints(InterLevelStats.skillpoints);
            updateStatCounts();
        }
        
    }

    public void powerStatUp() {
        if(InterLevelStats.skillpoints > 0) {
            InterLevelStats.skillpoints--;
            InterLevelStats.powerupStat++;
            updatePoints(InterLevelStats.skillpoints);
            updateStatCounts();
        }
    }

    public void enemyStatUp() {
        if(InterLevelStats.skillpoints > 0) {
            InterLevelStats.skillpoints--;
            InterLevelStats.enemyStat++;
            updatePoints(InterLevelStats.skillpoints);
            updateStatCounts();
        }
    }

    public void xpStatUp() {
        if(InterLevelStats.skillpoints > 0) {
            InterLevelStats.skillpoints--;
            InterLevelStats.xpStat++;
            updatePoints(InterLevelStats.skillpoints);
            updateStatCounts();
        }
    }

}
