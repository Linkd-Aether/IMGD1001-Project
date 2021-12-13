using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public int difficulty;
    public int hat;
    public int volume;
    public Slider myslider;
    public Button easy;
    public Button medium;
    public Button hard;
    public Button hat1;
    public Button hat2;
    public Button hat3;
    public Button hat4;
    public Button hat5;

    // Start is called before the first frame update
    void Start()
    {
        volume = PlayerPrefs.GetInt("volume", 100);
        myslider.value = volume;
        difficulty = PlayerPrefs.GetInt("difficulty", 0);

        darkenHats();

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

    public void setHats(int hat){
        if(hat == 0 && PlayerPrefs.GetInt("hat1", 0) == 1){
            PlayerPrefs.SetInt("wornHat", 0);
        }
        else if(hat == 1 && PlayerPrefs.GetInt("hat2", 0) == 1){
            PlayerPrefs.SetInt("wornHat", 1);
        }
        else if(hat == 2 && PlayerPrefs.GetInt("hat3", 0) == 1){
            PlayerPrefs.SetInt("wornHat", 2);
        }
        else if(hat == 3 && PlayerPrefs.GetInt("hat4", 0) == 1){
            PlayerPrefs.SetInt("wornHat", 3);
        }
        else if(hat == 4 && PlayerPrefs.GetInt("hat5", 0) == 1){
            PlayerPrefs.SetInt("wornHat", 4);
        }

        selectHatButton();
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

    private void selectHatButton(){
        hat = PlayerPrefs.GetInt("wornHat", 0);
        if(hat == 0){
            //equip hat 1
            ColorBlock cb = easy.colors;
            cb.normalColor = Color.cyan;
            cb.highlightedColor = Color.cyan;
            cb.selectedColor = Color.cyan;
            cb.pressedColor = Color.cyan;
            hat1.colors = cb;

            hat2.colors = ColorBlock.defaultColorBlock;
            hat3.colors = ColorBlock.defaultColorBlock;
            hat4.colors = ColorBlock.defaultColorBlock;
            hat5.colors = ColorBlock.defaultColorBlock;
        }
        else if(hat == 1){
            //equip hat 2
            ColorBlock cb = medium.colors;
            cb.normalColor = Color.cyan;
            cb.highlightedColor = Color.cyan;
            cb.selectedColor = Color.cyan;
            cb.pressedColor = Color.cyan;
            hat2.colors = cb;
            
            hat1.colors = ColorBlock.defaultColorBlock;
            hat3.colors = ColorBlock.defaultColorBlock;
            hat4.colors = ColorBlock.defaultColorBlock;
            hat5.colors = ColorBlock.defaultColorBlock;
        }
        else if(hat == 2){
            //equip hat 3
            ColorBlock cb = hard.colors;
            cb.normalColor = Color.cyan;
            cb.highlightedColor = Color.cyan;
            cb.selectedColor = Color.cyan;
            cb.pressedColor = Color.cyan;
            hat3.colors = cb;

            hat1.colors = ColorBlock.defaultColorBlock;
            hat2.colors = ColorBlock.defaultColorBlock;
            hat4.colors = ColorBlock.defaultColorBlock;
            hat5.colors = ColorBlock.defaultColorBlock;
        }
        else if(hat == 3){
            //equip hat 4
            ColorBlock cb = hard.colors;
            cb.normalColor = Color.cyan;
            cb.highlightedColor = Color.cyan;
            cb.selectedColor = Color.cyan;
            cb.pressedColor = Color.cyan;
            hat4.colors = cb;

            hat1.colors = ColorBlock.defaultColorBlock;
            hat2.colors = ColorBlock.defaultColorBlock;
            hat3.colors = ColorBlock.defaultColorBlock;
            hat5.colors = ColorBlock.defaultColorBlock;
        }
        else if(hat == 4){
            //equip hat 5
            ColorBlock cb = hard.colors;
            cb.normalColor = Color.cyan;
            cb.highlightedColor = Color.cyan;
            cb.selectedColor = Color.cyan;
            cb.pressedColor = Color.cyan;
            hat5.colors = cb;

            hat1.colors = ColorBlock.defaultColorBlock;
            hat2.colors = ColorBlock.defaultColorBlock;
            hat3.colors = ColorBlock.defaultColorBlock;
            hat4.colors = ColorBlock.defaultColorBlock;
        }
        darkenHats();
    }

    private void darkenHats(){
        if(PlayerPrefs.GetInt("hat1", 0) == 0){
            ColorBlock cb = hat1.colors;
            cb.normalColor = Color.gray;
            cb.highlightedColor = Color.gray;
            cb.selectedColor = Color.gray;
            cb.pressedColor = Color.gray;
            hat1.colors = cb;
        }
        if(PlayerPrefs.GetInt("hat2", 0) == 0){
            ColorBlock cb = hat2.colors;
            cb.normalColor = Color.gray;
            cb.highlightedColor = Color.gray;
            cb.selectedColor = Color.gray;
            cb.pressedColor = Color.gray;
            hat2.colors = cb;
        }
        if(PlayerPrefs.GetInt("hat3", 0) == 0){
            ColorBlock cb = hat3.colors;
            cb.normalColor = Color.gray;
            cb.highlightedColor = Color.gray;
            cb.selectedColor = Color.gray;
            cb.pressedColor = Color.gray;
            hat3.colors = cb;
        }
        if(PlayerPrefs.GetInt("hat4", 0) == 0){
            ColorBlock cb = hat4.colors;
            cb.normalColor = Color.gray;
            cb.highlightedColor = Color.gray;
            cb.selectedColor = Color.gray;
            cb.pressedColor = Color.gray;
            hat4.colors = cb;
        }
        if(PlayerPrefs.GetInt("hat5", 0) == 0){
            ColorBlock cb = hat5.colors;
            cb.normalColor = Color.gray;
            cb.highlightedColor = Color.gray;
            cb.selectedColor = Color.gray;
            cb.pressedColor = Color.gray;
            hat5.colors = cb;
        }
    }
}
