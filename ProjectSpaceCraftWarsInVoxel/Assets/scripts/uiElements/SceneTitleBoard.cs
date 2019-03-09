using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;


public class SceneTitleBoard : MonoBehaviour
{

    #region Component's Public Attributes [Allowed only classes that inherit MonoBehaviour or primitive types]
    [Header("+ Bilingual Text Options")]
    [Space(4)]
    [TextArea()]
    public string englishTitle;
    [TextArea()]
    public string portugueseTitle;

    [Space(5)]
    [Header("+ Change the Color According to the Levels")]
    [Space(4)]
    public bool enableChangeColor;

    [Space(5)]
    [Header("+ Prefer to use these colors")]
    [Space(4)]
    public bool enablePreferedColors;
    public Color backColor = Color.white;
    public Color frontColor = Color.gray;

    [Space(5)]
    [Header("+ Highlight Only the Front Text")]
    [Space(4)]
    public bool enableHighlightOnFrontText;


    #endregion

    #region Publics Properties [Aren't visible in Editor]

    #endregion

    #region Auxiliary Attributes or Properties  [Aren't visible in Editor]
    private bool componentAdded;
    private LevelDifficulty lastLevelDifficulty;
    private bool lastEnableChangeColor;
    private bool lastEnableHighlight;
    #endregion

    #region Messages Methods of MonoBehaviour
    void Start()
    {
        lastLevelDifficulty = LevelManager.CurrentLevelDifficulty;
        lastEnableChangeColor = enableChangeColor;
        lastEnableHighlight = enableHighlightOnFrontText;
    }

    void Update()
    {
        switch (LevelManager.CurrentLevelLanguage)
        {
            case Language.English:
                transform.GetChild(0).gameObject.GetComponent<Text>().text = englishTitle;
                transform.GetChild(1).gameObject.GetComponent<Text>().text = englishTitle;
                break;

            case Language.BrazilianPortuguese:
                transform.GetChild(0).gameObject.GetComponent<Text>().text = portugueseTitle;
                transform.GetChild(1).gameObject.GetComponent<Text>().text = portugueseTitle;
                break;
        }

        if (enableChangeColor)
        {
            transform.GetChild(1).gameObject.GetComponent<Text>().color = LevelManager.GetCurrentColorByCurrentLevelDifficulty();
        }
        else if (enablePreferedColors)
        {
            transform.GetChild(0).gameObject.GetComponent<Text>().color = backColor;
            transform.GetChild(1).gameObject.GetComponent<Text>().color = frontColor;
        }
        else
        {
            transform.GetChild(1).gameObject.GetComponent<Text>().color = Color.gray;
        }

        if (lastLevelDifficulty != LevelManager.CurrentLevelDifficulty || 
            lastEnableHighlight != enableHighlightOnFrontText || lastEnableChangeColor != enableChangeColor)
        {
            componentAdded = false;
            lastLevelDifficulty = LevelManager.CurrentLevelDifficulty;
            lastEnableChangeColor = enableChangeColor;
            lastEnableHighlight = enableHighlightOnFrontText;

            Destroy(transform.GetChild(0).gameObject.GetComponent<ColorFadeEffect>());
            Destroy(transform.GetChild(1).gameObject.GetComponent<ColorFadeEffect>());
        }

        if (!componentAdded)
        {
            componentAdded = true;

            if (enableHighlightOnFrontText)
            {
                transform.GetChild(1).gameObject.AddComponent<ColorFadeEffect>();
            }
            else
            {
                transform.GetChild(0).gameObject.AddComponent<ColorFadeEffect>();
            }
        }
    }
    #endregion

    #region Other Methods

    #endregion

}