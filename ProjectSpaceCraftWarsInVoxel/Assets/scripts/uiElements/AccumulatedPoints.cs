using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;


public class AccumulatedPoints : MonoBehaviour
{

    #region Component's Public Attributes [Allowed only classes that inherit MonoBehaviour or primitive types]
    public bool isScoreboard;
    #endregion

    #region Publics Properties [Aren't visible in Editor]

    #endregion

    #region Auxiliary Attributes or Properties  [Aren't visible in Editor]
    private bool updateScoreboard;
    #endregion

    #region Messages Methods of MonoBehaviour
    void Start()
    {
        transform.GetChild(1).GetComponent<Text>().text = "";
    }

    void Update()
    {
        //Change the title
        switch (LevelManager.CurrentLevelLanguage)
        {
            case Language.English:
                transform.GetChild(1).GetComponent<Text>().text = "Accumulated Points";
                break;

            case Language.BrazilianPortuguese:
                transform.GetChild(1).GetComponent<Text>().text = "Pontos Acumulados";
                break;
        }

        //Change the Points
        if (isScoreboard)
        {
            if (!updateScoreboard)
            {
                foreach (var points in ScoreboardManager.GetScoreboardLimitedList())
                {
                    transform.GetChild(2).GetComponent<Text>().text += "" + points + "\n";
                }

                updateScoreboard = true;
            }

            transform.GetChild(2).GetComponent<Text>().color = new Color(195 / 255f, 198 / 255f, 234 / 255f, 255 / 255f);
        }
        else
        {
            transform.GetChild(2).GetComponent<Text>().text = "" + (ScoreboardManager.CurrentAccumulatedPoints + ScoreboardManager.CurrentPoints);

            Color color = LevelManager.GetCurrentColorByCurrentLevelDifficulty();
            color.a = 152 / 255f;
            transform.GetChild(2).GetComponent<Text>().color = color;
        }

    }
    #endregion

    #region Other Methods

    #endregion

}