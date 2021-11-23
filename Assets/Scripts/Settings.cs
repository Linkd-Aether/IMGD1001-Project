using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public int difficulty;
    public int volume;
    public Slider myslider;
    public Button easy;
    public Button medium;
    public Button hard;

    // Start is called before the first frame update
    void Start()
    {
        volume = PlayerPrefs.GetInt("volume", 100);
        myslider.value = volume;
        difficulty = PlayerPrefs.GetInt("difficulty", 0);
        selectButton();
    }

    public void setDiff(string diff) {
        if(diff == "easy") {
            PlayerPrefs.SetInt("difficulty", 0);
        }
        else if(diff == "med") {
            PlayerPrefs.SetInt("difficulty", 1);
        }
        else if(diff == "hard") {
            PlayerPrefs.SetInt("difficulty", 2);
        }

        selectButton();
    }

    public void selectButton() {
        difficulty = PlayerPrefs.GetInt("difficulty", 0);
        if(difficulty == 0){
            //easy
            ColorBlock cb = easy.colors;
            cb.normalColor = Color.cyan;
            cb.highlightedColor = Color.cyan;
            cb.selectedColor = Color.cyan;
            cb.pressedColor = Color.cyan;
            easy.colors = cb;
            medium.colors = ColorBlock.defaultColorBlock;
            hard.colors = ColorBlock.defaultColorBlock;
        }
        else if(difficulty == 1){
            //med
            ColorBlock cb = medium.colors;
            cb.normalColor = Color.cyan;
            cb.highlightedColor = Color.cyan;
            cb.selectedColor = Color.cyan;
            cb.pressedColor = Color.cyan;
            medium.colors = cb;
            
            easy.colors = ColorBlock.defaultColorBlock;
            hard.colors = ColorBlock.defaultColorBlock;
        }
        else if(difficulty == 2){
            //hard
            ColorBlock cb = hard.colors;
            cb.normalColor = Color.cyan;
            cb.highlightedColor = Color.cyan;
            cb.selectedColor = Color.cyan;
            cb.pressedColor = Color.cyan;
            hard.colors = cb;

            medium.colors = ColorBlock.defaultColorBlock;
            easy.colors = ColorBlock.defaultColorBlock;
        }
    }

}
