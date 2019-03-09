using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;


public class LevelManager : MonoBehaviour, ITimed
{

    #region Component's Public Attributes [Allowed only classes that inherit MonoBehaviour or primitive types]
    [Header("+ Basic Level Options")]
    [Space(4)]
    public Language languageSelected = Language.English;
    public LevelDifficulty difficultySelected = LevelDifficulty.Normal;
    public bool soundEnable = true;
    public Text textInitialMessage;

    [Space(5)]
    [Header("+ Game Time Options")]
    [Space(4)]
    public GameObject levelTimer;
    [Range(5, 60)]
    public float timeLimit = 15;

    [Space(5)]
    [Header("+ Level End Options")]
    [Space(4)]
    public bool enableLevelEnd = true;
    public string nextSceneName;
    [Range(1, 20)]
    public float timeToExit = 1;
    public GameObject diamondForWinners;
    public GameObject textDiamondPoints;
    public GameObject textForLosers;
    public GameObject loadingSceneEffect;

    [Space(5)]
    [Header("+ Player Options")]
    [Space(4)]
    public GameObject player;
    public bool IsPlayerAlive = true;
    public bool enablePlayerActions = true;
    #endregion

    #region Publics Properties [Aren't visible in Editor]
    public static bool PlayerWins { get; set; }
    public static Language CurrentLevelLanguage { get; set; }
    public static LevelDifficulty CurrentLevelDifficulty { get; set; }
    #endregion

    #region Auxiliary Attributes or Properties  [Aren't visible in Editor]
    private Language lastLanguageSelected;
    private bool languageSelectedWasChanged;

    private LevelDifficulty lastDifficultySelected;
    private bool difficultySelectedWasChanged;

    private Chronometer timerToChangeScene;
    private bool timerToChangeSceneInitiated;

    private Chronometer levelClock;
    private bool levelClockInitiated;

    private int currentSceneTime;

    public static bool firstRunScene = true;
    #endregion

    #region Messages Methods of MonoBehaviour    
    void Start()
    {
        levelClock = new Chronometer(timeLimit, this);
        timerToChangeScene = new Chronometer(timeToExit);

        IsPlayerAlive = true;

        if (firstRunScene)
        {
            if (languageSelected != CurrentLevelLanguage)
            {
                CurrentLevelLanguage = languageSelected;
            }

            if (difficultySelected != CurrentLevelDifficulty)
            {
                CurrentLevelDifficulty = difficultySelected;
            }
        }
        else
        {
            languageSelected = CurrentLevelLanguage;
            difficultySelected = CurrentLevelDifficulty;
        }


    }

    private void OnDisable()
    {
        firstRunScene = false;
    }

    private bool enableLoadingScene;

    void Update()
    {
        #region basic Updates
        //Update Player Actions
        if (player != null && player.GetComponent<PlayerController>() != null)
        {
            if (enablePlayerActions)
            {
                player.GetComponent<PlayerController>().EnableAllActions();

            }
            else
            {
                player.GetComponent<PlayerController>().DisableAllActions();

            }

            if (player.GetComponent<PlayerController>().FreeFallEnded)
            {
                EnemiesManager.IsRaffleEnemies = true;
            }
            else
            {
                EnemiesManager.IsRaffleEnemies = false;
            }
        }

        UpdateLevelDifficulty();

        UpdateLanguageText();

        UpdatePlayerLiveStatus();

        if (!IsPlayerAlive && player != null && player.GetComponent<PlayerController>() != null)
        {
            EnemiesManager.IsRaffleEnemies = false;
        }

        UpdatePlayerWinsOrLoses();
        #endregion

        //Init Level Clock
        if (player != null && player.GetComponent<PlayerController>() != null &&
                player.GetComponent<PlayerController>().FreeFallEnded &&
                player.GetComponent<PlayerController>().IsEnableActions() &&
                !levelClockInitiated)
        {
            textInitialMessage.gameObject.SetActive(false);
            levelClock.Start();
            levelClockInitiated = true;
        }

        //Scene timer
        UpdateLevelClock();
        UpdateTimerColorOnLastSeconds();

        //Change Scene
        if ((!IsPlayerAlive || PlayerWins) && timerToChangeScene.IsReachTimeGoal() && enableLevelEnd)
        {
            if (loadingSceneEffect != null && !enableLoadingScene)
            {
                enableLoadingScene = true;
                loadingSceneEffect.SetActive(true);
                loadingSceneEffect.GetComponent<LoadingSceneEffect>().SceneNameToLoading = nextSceneName;
                loadingSceneEffect.GetComponent<LoadingSceneEffect>().StartLoading();
            }
            else
            {
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }
    #endregion

    #region Other Methods
    public void OnReachTimeGoal()
    {
        if (IsPlayerAlive)
        {
            //Instantiate Diamond
            GameObject diamond = Instantiate<GameObject>(diamondForWinners);
            diamond.GetComponent<DiamondController>().points = (int)(
                diamond.GetComponent<DiamondController>().points * GetMultiplierByCurrentLevelDifficulty()
            );

            textDiamondPoints.SetActive(true);
            textDiamondPoints.GetComponent<Text>().text = "+" + diamond.GetComponent<DiamondController>().points;

            ScoreboardManager.CurrentPoints += diamond.GetComponent<DiamondController>().points;
            PlayerWins = true;
            enablePlayerActions = false;
        }
        else
        {
            textForLosers.SetActive(true);
            PlayerWins = false;
        }

        levelClock.Stop();
    }

    private void UpdateLanguageText()
    {
        //Update Language
        if (lastLanguageSelected != languageSelected)
        {
            languageSelectedWasChanged = true;
        }

        if (languageSelectedWasChanged)
        {
            CurrentLevelLanguage = languageSelected;
            languageSelectedWasChanged = false;
        }
        else
        {
            languageSelected = CurrentLevelLanguage;
        }

        lastLanguageSelected = languageSelected;
    }

    private void UpdateLevelDifficulty()
    {
        if (lastDifficultySelected != difficultySelected)
        {
            difficultySelectedWasChanged = true;
        }

        if (difficultySelectedWasChanged)
        {
            CurrentLevelDifficulty = difficultySelected;
            difficultySelectedWasChanged = false;
        }
        else
        {
            difficultySelected = CurrentLevelDifficulty;
        }

        lastDifficultySelected = difficultySelected;
    }

    private void UpdatePlayerWinsOrLoses()
    {
        //You Win
        if (IsPlayerAlive && PlayerWins && !timerToChangeSceneInitiated)
        {
            player.GetComponent<PlayerController>().DisableAllActions();
            timerToChangeScene.Start();
            timerToChangeSceneInitiated = true;
        }

        //You Lose
        if (!IsPlayerAlive && !PlayerWins && !timerToChangeSceneInitiated)
        {
            timerToChangeScene.Start();

            if (textForLosers != null)
            {
                textForLosers.SetActive(true);
            }

            levelClock.Pause();

            timerToChangeSceneInitiated = true;
        }
    }

    private void UpdateLevelClock()
    {
        if (levelClock.IsStopped || levelClock.IsReachTimeGoal())
        {
            currentSceneTime = 0;
        }
        else
        {
            currentSceneTime = Mathf.Abs((((int)levelClock.GetElapsed()) - (int)timeLimit));
        }

        if (levelTimer != null)
        {
            levelTimer.transform.GetChild(0).GetComponent<Text>().text = "" + currentSceneTime;
            levelTimer.transform.GetChild(1).GetComponent<Text>().text = "" + currentSceneTime;
        }
    }

    private bool componentAdded10s;
    private bool componentAdded4s;
    private Color red = new Color(199f / 255f, 7f / 255f, 7f / 255f, 216f / 255f);
    private Color yellow = new Color(133f / 255f, 129f / 255f, 0f / 255f, 216f / 255f);

    private void UpdateTimerColorOnLastSeconds()
    {
        #region Change Color to Yellow = 10s
        if (currentSceneTime <= 10 && !componentAdded10s)
        {
            if (levelTimer != null)
            {
                levelTimer.transform.GetChild(1).GetComponent<Text>().color = yellow;
                Destroy(levelTimer.transform.GetChild(0).GetComponent<ColorFadeEffect>());
                Destroy(levelTimer.transform.GetChild(1).GetComponent<ColorFadeEffect>());

                ColorFadeEffect tempComponent0 = levelTimer.transform.GetChild(0).gameObject.AddComponent<ColorFadeEffect>();
                ColorFadeEffect tempComponent1 = levelTimer.transform.GetChild(1).gameObject.AddComponent<ColorFadeEffect>();

                tempComponent0.speedTransition = 2;
                tempComponent1.speedTransition = 2;

            }

            componentAdded10s = true;
        }
        #endregion

        #region Change Color to Red = 4s
        if (currentSceneTime <= 4 && !componentAdded4s)
        {
            //sceneTimer.transform.GetChild(0).GetComponent<Text>().color = ???;
            levelTimer.transform.GetChild(1).GetComponent<Text>().color = red;

            Destroy(levelTimer.transform.GetChild(0).GetComponent<ColorFadeEffect>());
            Destroy(levelTimer.transform.GetChild(1).GetComponent<ColorFadeEffect>());

            ColorFadeEffect tempComponent0 = levelTimer.transform.GetChild(0).gameObject.AddComponent<ColorFadeEffect>();
            ColorFadeEffect tempComponent1 = levelTimer.transform.GetChild(1).gameObject.AddComponent<ColorFadeEffect>();

            tempComponent0.speedTransition = 2;
            tempComponent1.speedTransition = 2;

            componentAdded4s = true;
        }
        #endregion
    }


    private void UpdatePlayerLiveStatus()
    {
        IsPlayerAlive = player != null && !player.GetComponent<PlayerController>().Crashed ? true : false;
    }

    public static float GetMultiplierByCurrentLevelDifficulty()
    {
        switch (CurrentLevelDifficulty)
        {
            default:
            case LevelDifficulty.Trainee:
                return 1.0f;

            case LevelDifficulty.Easy:
                return 1.5f;

            case LevelDifficulty.Beginner:
                return 2.0f;

            case LevelDifficulty.Normal:
                return 2.5f;

            case LevelDifficulty.Defiant:
                return 3.0f;

            case LevelDifficulty.Experient:
                return 3.5f;

            case LevelDifficulty.Extremist:
                return 4.0f;
        }
    }

    public static Color GetCurrentColorByCurrentLevelDifficulty()
    {
        switch (CurrentLevelDifficulty)
        {
            default:
            case LevelDifficulty.Trainee:
                return new Color(206 / 255f, 92 / 255f, 255 / 255f, 255 / 255f);

            case LevelDifficulty.Easy:
                return new Color(255 / 255f, 58 / 255f, 144 / 255f, 255 / 255f);

            case LevelDifficulty.Beginner:
                return new Color(58 / 255f, 100 / 255f, 255 / 255f, 255 / 255f);

            case LevelDifficulty.Normal:
                return new Color(58 / 255f, 255 / 255f, 222 / 255f, 255 / 255f);

            case LevelDifficulty.Defiant:
                return new Color(58 / 255f, 255 / 255f, 68 / 255f, 255 / 255f);

            case LevelDifficulty.Experient:
                return new Color(255 / 255f, 168 / 255f, 58 / 255f, 255 / 255f);

            case LevelDifficulty.Extremist:
                return new Color(255 / 255f, 255 / 255f, 0 / 255f, 255 / 255f);
        }
    }

    public static bool IncreaseLevelDifficulty()
    {

        int current = (int)CurrentLevelDifficulty;
        current++;

        int length = Enum.GetValues(typeof(LevelDifficulty)).Length;

        if (current == length)
        {
            CurrentLevelDifficulty = LevelDifficulty.Trainee;
            return false;
        }

        CurrentLevelDifficulty = (LevelDifficulty)current;

        return true;
    }
    #endregion

}

#region enum Difficulty
public enum LevelDifficulty
{
    Trainee = 0,
    Easy = 1,
    Beginner = 2,
    Normal = 3,
    Defiant = 4,
    Experient = 5,
    Extremist = 6
}
#endregion

#region enum Language
public enum Language
{
    English,
    BrazilianPortuguese
}
#endregion