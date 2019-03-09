using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;


public class TextCurrentLevel : MonoBehaviour
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
        switch (LevelManager.CurrentLevelLanguage)
        {
            case Language.English:
                GetComponent<Text>().text = "Level: " + (int)LevelManager.CurrentLevelDifficulty;
                break;

            case Language.BrazilianPortuguese:
                GetComponent<Text>().text = "Fase: " + (int)LevelManager.CurrentLevelDifficulty;
                break;
        }

        Color color = LevelManager.GetCurrentColorByCurrentLevelDifficulty();
        color.a = 152 / 255f;
        GetComponent<Text>().color = color;
    }
    #endregion

    #region Other Methods

    #endregion

}