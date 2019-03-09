using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class TextFeedbackMission : MonoBehaviour
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
                if (LevelManager.PlayerWins)
                {
                    GetComponent<Text>().text = "Congratulations!\nYou\nCompleted the level\n:)";
                }
                else
                {
                    GetComponent<Text>().text = "That sad!\nYou will have\nTo start all\nover again\n:(";
                }
                break;

            case Language.BrazilianPortuguese:
                if (LevelManager.PlayerWins)
                {
                    GetComponent<Text>().text = "Parabéns!\nVocê\nCompletou a Fase\n:)";
                }
                else
                {
                    GetComponent<Text>().text = "Que triste!\nVocê terá que\nComeçar tudo de novo\n: (";
                }
                break;
        }
    }
    #endregion

    #region Other Methods

    #endregion

}