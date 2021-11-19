using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashSequence : MonoBehaviour
{
    public static int SceneNumber;

    // Start is called before the first frame update
    void Start()
    {
        if(SceneNumber == 0) {
            StartCoroutine(ToSplashTwo());
        }
        if(SceneNumber == 1) {
            StartCoroutine(ToMainMenu());
        }
    }


    IEnumerator ToSplashTwo() {
        yield return new WaitForSeconds(3);
        SceneNumber = 1;
        SceneManager.LoadScene("SplashScreen");
    }

    IEnumerator ToMainMenu() {
        yield return new WaitForSeconds(3);
        SceneNumber = 2;
        SceneManager.LoadScene("Main Menu");
    }
}
