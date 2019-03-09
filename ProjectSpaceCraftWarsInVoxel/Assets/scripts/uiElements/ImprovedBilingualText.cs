using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


public class ImprovedBilingualText : MonoBehaviour
{

    #region Component's Public Attributes [Allowed only classes that inherit MonoBehaviour or primitive types]
    [Header("+ Bilingual Text Options")]
    [Space(4)]
    [TextArea(6, 10)]
    public string englishText;
    [TextArea(6, 10)]
    public string brazilianPortugueseText;

    [Space(5)]
    [Header("+ Change the Color According to the Levels")]
    [Space(4)]
    [Tooltip("Enable Change Color")]
    public bool enableChangeColor;

    [Space(5)]
    [Header("+ Text Highlighting Effect Options")]
    [Space(4)]
    [Tooltip("Enable Highlight Text Effect")]
    public bool enableHighlightText;
    [Tooltip("Repeat Highlight Text Effect")]
    public bool repeatHighlightEffect;
    [Range(0.1f, 10f)]
    public float speedTransition = 1f;
    [Range(0.0f, 1.0f)]
    public float alphaIntensity = 1f;
    [Tooltip("Fade In   Effetc = Black to Color [true].\nFade Out Effect = Color to Black [false].")]
    public bool applyFadeIn;

    [Space(5)]
    [Header("+ Timer of Highlight Effect")]
    [Space(4)]
    [Tooltip("The Duration of the Effect")]
    [Range(0.0f, 10.0f)]
    public float effectHighlightRuntime;
    [Tooltip("Disable GameObject at end of effect runtime")]
    public bool disableGameObject;
    [Tooltip("Destroy GameObject at end of effect runtime")]
    public bool destroyGameObject;

    [Space(5)]
    [Header("+ TypeWriter Effect Options")]
    [Space(4)]
    [Tooltip("Enable the Type Writer Effect")]
    public bool enableTypeWriter;
    [Tooltip("Repeat the Type Writer Effect")]
    public bool repeatTypeWriterEffect;
    [Tooltip("The Duration of the Type Writer Effect")]
    [Range(1f, 60.0f)]
    public float typeWriterRuntime = 1f;

    [Space(5)]
    [Header("+ Scroll Text Effect Options")]
    [Space(4)]
    [Tooltip("Enable the Scroll Text Effect")]
    public bool enableScrollEffect;
    [Tooltip("Repeat the Scoll Text Effect")]
    public bool repeatScrollEffect;
    [Tooltip("The Duration of the Scroll Text Effect")]
    [Range(1f, 300.0f)]
    public float scrollRuntime = 1f;
    #endregion

    #region Publics Properties [Aren't visible in Editor]

    #endregion

    #region Auxiliary Attributes or Properties  [Aren't visible in Editor]
    private bool componentAdded;
    private LevelDifficulty lastLevelDifficulty;
    private bool lastEnableChangeColor;
    private bool lastEnableHighlight;

    private bool isRunningTypeWriter;
    private bool ranTypeWriterOnce;

    private bool isRunningScrollText;
    private bool ranScrollTextOnce;
    #endregion

    #region Messages Methods of MonoBehaviour
    void Start()
    {
        lastLevelDifficulty = LevelManager.CurrentLevelDifficulty;
        lastEnableChangeColor = enableChangeColor;
        lastEnableHighlight = enableHighlightText;

        if (enableHighlightText)
        {
            componentAdded = false;
        }
        else
        {
            componentAdded = true;
        }
    }

    void Update()
    {
        UpdateLanguage();

        UpdateHighlightText();

        UpdateTypeWriterEffect();

        UpdateScrollTextEffect();
    }
    #endregion

    #region Other Methods
    private void UpdateLanguage()
    {
        if (!isRunningScrollText && !isRunningTypeWriter)
        {
            switch (LevelManager.CurrentLevelLanguage)
            {
                case Language.English:
                    GetComponent<Text>().text = englishText;
                    break;

                case Language.BrazilianPortuguese:
                    GetComponent<Text>().text = brazilianPortugueseText;
                    break;
            }
        }
    }

    private void UpdateHighlightText()
    {
        if (enableChangeColor)
        {
            Color color = LevelManager.GetCurrentColorByCurrentLevelDifficulty();
            color.a = 152 / 255f;
            GetComponent<Text>().color = color;
        }

        if (lastLevelDifficulty != LevelManager.CurrentLevelDifficulty ||
            lastEnableHighlight != enableHighlightText ||
            lastEnableChangeColor != enableChangeColor)
        {
            Start();
        }

        if (!componentAdded)
        {
            componentAdded = true;

            if (GetComponent<ColorFadeEffect>() != null)
            {
                Destroy(GetComponent<ColorFadeEffect>());
            }

            ColorFadeEffect colorEffect = gameObject.AddComponent<ColorFadeEffect>();
            colorEffect.repeat = repeatHighlightEffect;
            colorEffect.speedTransition = speedTransition;
            colorEffect.alphaIntensity = alphaIntensity;
            colorEffect.applyFadeIn = applyFadeIn;
            colorEffect.effectRuntime = effectHighlightRuntime;
            colorEffect.disableGameObject = disableGameObject;
            colorEffect.destroyGameObject = destroyGameObject;
        }
    }

    private void UpdateTypeWriterEffect()
    {
        if (enableTypeWriter)
        {
            if (!isRunningTypeWriter)
            {
                if (repeatTypeWriterEffect)
                {
                    StartCoroutine(TypeWriterEffectCoroutine());
                }
                else if (!ranTypeWriterOnce)
                {
                    StartCoroutine(TypeWriterEffectCoroutine());
                    ranTypeWriterOnce = true;
                }
            }
        }
    }

    private IEnumerator TypeWriterEffectCoroutine()
    {
        isRunningTypeWriter = true;

        GetComponent<Text>().text = "";

        char tempChar = ' ';
        int length = (LevelManager.CurrentLevelLanguage == Language.English ? englishText : brazilianPortugueseText).Length;

        float maxWordTime = typeWriterRuntime / length;

        for (int index = 0; index < length; index++)
        {
            switch (LevelManager.CurrentLevelLanguage)
            {
                case Language.English:
                    tempChar = englishText[index];
                    GetComponent<Text>().text += tempChar;
                    break;

                case Language.BrazilianPortuguese:
                    tempChar = brazilianPortugueseText[index];
                    GetComponent<Text>().text += tempChar;
                    break;
            }

            yield return new WaitForSeconds(maxWordTime);
        }

        isRunningTypeWriter = false;
    }

    private void UpdateScrollTextEffect()
    {
        if (enableScrollEffect)
        {
            if (!isRunningScrollText)
            {
                if (repeatScrollEffect)
                {
                    StartCoroutine(ScrollEffectCoroutine());
                }
                else if (!ranScrollTextOnce)
                {
                    StartCoroutine(ScrollEffectCoroutine());
                    ranScrollTextOnce = true;
                }
            }
        }
    }

    private IEnumerator ScrollEffectCoroutine()
    {
        isRunningScrollText = true;

        char delimiter = '\n';
        string[] tempArray = LevelManager.CurrentLevelLanguage == Language.English ? englishText.Split(delimiter) : brazilianPortugueseText.Split(delimiter);
        int length = tempArray.Length;

        float maxLineTime = scrollRuntime / length;

        for (int indexDeadLine = -1; indexDeadLine < length; indexDeadLine++)
        {
            string tempText = "";

            if (!isRunningTypeWriter)
            {
                for (int indexFullText = indexDeadLine + 1; indexFullText < length; indexFullText++)
                {
                    tempText += tempArray[indexFullText] + "\n";
                }
            }
            else
            {
                tempText = GetComponent<Text>().text.Substring(GetComponent<Text>().text.IndexOf('\n') + 1);
            }
            GetComponent<Text>().text = tempText;

            yield return new WaitForSeconds(maxLineTime);
        }

        isRunningScrollText = false;
    }
    #endregion

}