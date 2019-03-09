using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;


public class TextTitleGameOver : MonoBehaviour
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
        //Change color
        transform.GetChild(1).gameObject.GetComponent<Text>().color = LevelManager.GetCurrentColorByCurrentLevelDifficulty();

        //Add Component
        transform.GetChild(0).gameObject.AddComponent<ColorFadeEffect>();
        transform.GetChild(1).gameObject.AddComponent<ColorFadeEffect>();
    }

    void Update()
    {
        switch (LevelManager.CurrentLevelLanguage)
        {
            case Language.English:
                transform.GetChild(0).gameObject.GetComponent<Text>().text = "Game\nOver";
                transform.GetChild(1).gameObject.GetComponent<Text>().text = "Game\nOver";
                break;

            case Language.BrazilianPortuguese:
                transform.GetChild(0).gameObject.GetComponent<Text>().text = "Fim\nDe Jogo";
                transform.GetChild(1).gameObject.GetComponent<Text>().text = "Fim\nDe Jogo";
                break;
        }
    }
    #endregion

    #region Other Methods
    
    #endregion

}