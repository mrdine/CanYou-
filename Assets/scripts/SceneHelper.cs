using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneHelper : MonoBehaviour {
    public static SceneHelper instance;
    public static void GoToScene(string sceneName){
        instance.goToScene(sceneName);
    }

    void Awake(){
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(instance);
        }else{
            Destroy(this);
        }
    }

    IEnumerator loadScene(string sceneName){
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    void goToScene(string sceneName){
        StartCoroutine(loadScene(sceneName));
    }
}