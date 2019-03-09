using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;


public class ButtonNextSceneToFeedback : MonoBehaviour
{

    #region Component's Public Attributes [Allowed only classes that inherit MonoBehaviour or primitive types]
    [Header("+ Next Scene Options for FeedbackMission")]
    [Space(4)]
    [Tooltip("Choose the next Scene to Win")]
    public string nextSceneToWin;
    [Tooltip("Choose the next Scene to Lose")]
    public string nextSceneToLose;
    [Tooltip("Choose the next Scene to Good GameOver")]
    public string nextSceneToGoodGameOver;
    public GameObject loadingSceneEffect;
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
        //update language
        switch (LevelManager.CurrentLevelLanguage)
        {
            case Language.English:
                if (LevelManager.PlayerWins)
                {
                    transform.GetChild(0).GetComponent<Text>().text = "Next";
                }
                else
                {
                    transform.GetChild(0).GetComponent<Text>().text = "Finish";
                }
                break;

            case Language.BrazilianPortuguese:
                if (LevelManager.PlayerWins)
                {
                    transform.GetChild(0).GetComponent<Text>().text = "Próximo";
                }
                else
                {
                    transform.GetChild(0).GetComponent<Text>().text = "Terminar";
                }
                break;
        }

        //Use "Enter" like click Next
        if (Input.GetKey(KeyCode.Return))
        {
            OnClick_ChangeScene();
        }
    }
    #endregion

    #region Other Methods
    public void OnClick_ChangeScene()
    {
        if (LevelManager.PlayerWins)
        {
            ScoreboardManager.CurrentAccumulatedPoints += ScoreboardManager.CurrentPoints;
            ScoreboardManager.CurrentPoints = 0;

            LevelManager.PlayerWins = false;
            bool goodGameOver = !LevelManager.IncreaseLevelDifficulty();

            if (goodGameOver)
            {
                ScoreboardManager.SaveCurrentAccumulatedPointsToScoreXML();
                ScoreboardManager.CurrentAccumulatedPoints = 0;

                if (loadingSceneEffect != null)
                {
                    loadingSceneEffect.SetActive(true);
                    loadingSceneEffect.GetComponent<LoadingSceneEffect>().SceneNameToLoading = nextSceneToGoodGameOver;
                    loadingSceneEffect.GetComponent<LoadingSceneEffect>().StartLoading();
                }
                else
                {
                    SceneManager.LoadScene(nextSceneToGoodGameOver);
                }
            }
            else
            {
                if (loadingSceneEffect != null)
                {
                    loadingSceneEffect.SetActive(true);
                    loadingSceneEffect.GetComponent<LoadingSceneEffect>().SceneNameToLoading = nextSceneToWin;
                    loadingSceneEffect.GetComponent<LoadingSceneEffect>().StartLoading();
                }
                else
                {
                    SceneManager.LoadScene(nextSceneToWin);
                }
            }
        }
        else
        {
            ScoreboardManager.CurrentAccumulatedPoints += ScoreboardManager.CurrentPoints;
            ScoreboardManager.SaveCurrentAccumulatedPointsToScoreXML();

            ScoreboardManager.CurrentPoints = 0;
            ScoreboardManager.CurrentAccumulatedPoints = 0;

            LevelManager.PlayerWins = false;
            LevelManager.CurrentLevelDifficulty = LevelDifficulty.Trainee;

            if (loadingSceneEffect != null)
            {
                loadingSceneEffect.SetActive(true);
                loadingSceneEffect.GetComponent<LoadingSceneEffect>().SceneNameToLoading = nextSceneToLose;
                loadingSceneEffect.GetComponent<LoadingSceneEffect>().StartLoading();
            }
            else
            {
                SceneManager.LoadScene(nextSceneToLose);
            }
        }
    }
    #endregion

}