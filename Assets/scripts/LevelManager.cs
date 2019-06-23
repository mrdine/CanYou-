using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    List<string> scenes = new List<string>(new string[]{
        "00",
        "01",
        "02"
    });
    int currentLevel = 0;

    public static LevelManager instance;

    void Awake(){
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(instance);
        }else{
            Destroy(this);
        }
    }

    public static void GoToFirstLevel(){
        instance.goToFirstLevel();
    }

    public static void GameOver(){
        instance.gameOver();
    }

    public static void Win(){
        instance.win();
    }

    public static void NextLevel(){
        instance.nextLevel();
    }

    public void goToFirstLevel(){
        Debug.Log("Going to first level");
        SceneHelper.GoToScene(scenes[0]);
    }

    public void gameOver(){
        SceneHelper.GoToScene("you_died");
        currentLevel = 0;
    }

    public void win(){
        SceneHelper.GoToScene("you_won");
        currentLevel = 0;
    }

    public void nextLevel(){
        currentLevel++;
        if (scenes.Count > currentLevel) {
            SceneHelper.GoToScene(scenes[currentLevel]);
        } else {
            Win();
        }
    }
}
