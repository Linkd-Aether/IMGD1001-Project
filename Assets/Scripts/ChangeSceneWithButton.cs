using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
using UnityEngine.SceneManagement;

public class ChangeSceneWithButton: MonoBehaviour {  
    public void LoadScene(string sceneName) {
        if(sceneName != "Quit") {
            SceneManager.LoadScene(sceneName);  
        }
        else {
            Application.Quit();
        }
        
    }

    public void LoadNextLevel() {
        int sceneIndex = SceneUtility.GetBuildIndexByScenePath("Lvl" + (InterLevelStats.level % GameManager.numLevels + 1));
        if(sceneIndex >= 0) {
            SceneManager.LoadSceneAsync(sceneIndex);
        } else {
            LoadFirstLevel();
        }
    }

    public void LoadFirstLevel() {
        //InterLevelStats.level = 0;
        LoadScene("BasicMap");
    }
      

}   