using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;


public class TextLevelTitle : MonoBehaviour
{

    #region Component's Public Attributes [Allowed only classes that inherit MonoBehaviour or primitive types]

    #endregion

    #region Publics Properties [Aren't visible in Editor]
    public bool IsScoreboard { get; set; }
    #endregion

    #region Auxiliary Attributes or Properties  [Aren't visible in Editor]   
    private bool componentAdded;
    private LevelDifficulty lastLevelDifficulty;
    private bool lastIsScoreboard;
    #endregion

    #region Messages Methods of MonoBehaviour
    void Start()
    {
        lastLevelDifficulty = LevelManager.CurrentLevelDifficulty;
        lastIsScoreboard = IsScoreboard;
    }

    void Update()
    {
        if (IsScoreboard)
        {
            GetComponent<Text>().color = Color.black;
        }
        else
        {
            GetComponent<Text>().color = LevelManager.GetCurrentColorByCurrentLevelDifficulty();
        }

        if (lastLevelDifficulty != LevelManager.CurrentLevelDifficulty || lastIsScoreboard != IsScoreboard)
        {
            lastLevelDifficulty = LevelManager.CurrentLevelDifficulty;
            lastIsScoreboard = IsScoreboard;
            Destroy(gameObject.GetComponent<ColorFadeEffect>());
            componentAdded = false;
        }

        if (!componentAdded)
        {
            gameObject.AddComponent<ColorFadeEffect>();
            componentAdded = true;
        }
    }
    #endregion

    #region Other Methods

    #endregion

}