using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;


public class ButtonNextScene : MonoBehaviour, ITimed
{

    #region Component's Public Attributes [Allowed only classes that inherit MonoBehaviour or primitive types]
    [Header("+ Bilingual Text Options")]
    [Space(4)]
    [TextArea(2, 2)]
    public string englishText;
    [TextArea(2, 2)]
    public string brazilianPortugueseText;

    [Space(5)]
    [Header("+ Next Scene Options")]
    [Space(4)]
    [Tooltip("Choose the next Scene")]
    public string nextSceneName;
    public GameObject loadingSceneEffect;
    public bool enableEnterToSelect = true;
    #endregion

    #region Publics Properties [Aren't visible in Editor]

    #endregion

    #region Auxiliary Attributes or Properties  [Aren't visible in Editor]
    private Chronometer soundTimer;
    #endregion

    #region Messages Methods of MonoBehaviour
    void Start()
    {
        soundTimer = new Chronometer(0.6f, this);
    }

    private bool timerHasStarted;

    void Update()
    {
        //update language
        switch (LevelManager.CurrentLevelLanguage)
        {
            case Language.English:
                transform.GetChild(0).GetComponent<Text>().text = englishText;
                break;

            case Language.BrazilianPortuguese:
                transform.GetChild(0).GetComponent<Text>().text = brazilianPortugueseText;
                break;
        }

        //Use "Enter" like click Next
        if (Input.GetKey(KeyCode.Return) && enableEnterToSelect)
        {
            OnClick_ChangeScene();
        }

        if (timerHasStarted)
        {
            soundTimer.UpdateCounter();
        }
    }
    #endregion

    #region Other Methods
    public void OnClick_ChangeScene()
    {
        if (GetComponent<AudioSource>() != null)
        {
            GetComponent<AudioSource>().Play();
        }

        soundTimer.Start();
        timerHasStarted = true;
    }

    public void OnReachTimeGoal()
    {
        if (loadingSceneEffect != null)
        {
            loadingSceneEffect.SetActive(true);
            loadingSceneEffect.GetComponent<LoadingSceneEffect>().SceneNameToLoading = nextSceneName;
            loadingSceneEffect.GetComponent<LoadingSceneEffect>().StartLoading();
            soundTimer.Stop();
        }
        else
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
    #endregion

}