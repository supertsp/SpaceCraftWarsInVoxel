using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;


public class TextCurrentPoints : MonoBehaviour
{

    #region Component's Public Attributes [Allowed only classes that inherit MonoBehaviour or primitive types]

    #endregion

    #region Publics Properties [Aren't visible in Editor]

    #endregion

    #region Auxiliary Attributes or Properties  [Aren't visible in Editor]   
    private bool componentAdded;
    private LevelDifficulty lastLevelDifficulty;
    #endregion

    #region Messages Methods of MonoBehaviour
    void Start()
    {
        lastLevelDifficulty = LevelManager.CurrentLevelDifficulty;
    }

    void Update()
    {
        transform.GetChild(0).gameObject.GetComponent<Text>().text = "" + ScoreboardManager.CurrentPoints;
        transform.GetChild(1).gameObject.GetComponent<Text>().text = "" + ScoreboardManager.CurrentPoints;

        transform.GetChild(0).gameObject.GetComponent<Text>().color = LevelManager.GetCurrentColorByCurrentLevelDifficulty();

        if (lastLevelDifficulty != LevelManager.CurrentLevelDifficulty)
        {
            lastLevelDifficulty = LevelManager.CurrentLevelDifficulty;

            Destroy(transform.GetChild(0).gameObject.GetComponent<ColorFadeEffect>());
            Destroy(transform.GetChild(1).gameObject.GetComponent<ColorFadeEffect>());

            componentAdded = false;
        }

        if (!componentAdded)
        {
            ColorFadeEffect temp1 = transform.GetChild(0).gameObject.AddComponent<ColorFadeEffect>();
            ColorFadeEffect temp2 = transform.GetChild(1).gameObject.AddComponent<ColorFadeEffect>();

            temp1.speedTransition = 2;
            temp2.speedTransition = 2;

            componentAdded = true;
        }
    }
    #endregion

    #region Other Methods

    #endregion

}