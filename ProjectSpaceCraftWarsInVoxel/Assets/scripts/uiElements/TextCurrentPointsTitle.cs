using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;


public class TextCurrentPointsTitle : MonoBehaviour
{

    #region Component's Public Attributes [Allowed only classes that inherit MonoBehaviour or primitive types]
    //public LevelManager levelManager;
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
                GetComponent<Text>().text = "Current Points";
                break;

            case Language.BrazilianPortuguese:
                GetComponent<Text>().text = "Pontuação Atual";
                break;
        }
    }
    #endregion

    #region Other Methods
    
    #endregion

}