using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    List<string> scenes = new List<string>(new string[]{
        "00",
        "01",
        "02"
    });

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
    }

    public void win(){
        SceneHelper.GoToScene("you_won");
    }

    public void nextLevel(){
        int currentLevel = scenes.IndexOf(SceneManager.GetActiveScene().name);
        if (scenes.Count - 1 > currentLevel) {
            SceneHelper.GoToScene(scenes[currentLevel + 1]);
        } else {
            Win();
        }
    }
}
