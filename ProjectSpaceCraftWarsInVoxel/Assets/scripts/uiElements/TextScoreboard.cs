using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;


public class TextScoreboard : MonoBehaviour
{

    #region Component's Public Attributes [Allowed only classes that inherit MonoBehaviour or primitive types]

    #endregion

    #region Publics Properties [Aren't visible in Editor]

    #endregion

    #region Auxiliary Attributes or Properties  [Aren't visible in Editor]

    #endregion

    #region Messages Methods of MonoBehaviour
    void Start()
    {

    }

    void Update()
    {
        GetComponent<Text>().text = "" + ScoreboardManager.CurrentPoints;
        Color color = LevelManager.GetCurrentColorByCurrentLevelDifficulty();
        color.a = 152 / 255f;
        GetComponent<Text>().color = color;
        transform.GetChild(0).GetComponent<Image>().color = color;
    }
    #endregion

    #region Other Methods

    #endregion

}