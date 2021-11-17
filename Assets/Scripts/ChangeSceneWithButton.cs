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
      

}   