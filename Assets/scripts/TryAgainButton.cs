using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TryAgainButton : MonoBehaviour
{
    public Button tryAgain;
    void Start()
    {
       tryAgain.onClick.AddListener(() => LevelManager.GoToFirstLevel());
    }
}
