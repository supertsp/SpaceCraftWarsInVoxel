using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


public class EnemiesManager : MonoBehaviour, ITimed
{

    #region Component's Public Attributes [Allowed only classes that inherit MonoBehaviour or primitive types]
    public bool raffleEnemies = true;
    public List<GameObject> typesOfEnemies;

    #endregion

    #region Publics Properties [Aren't visible in Editor]
    public static bool IsRaffleEnemies { get; set; }
    #endregion

    #region Auxiliary Attributes or Properties  [Aren't visible in Editor]
    private bool lastRaffleEnemies;
    private bool lastIsRaffleEnemies;
    private Chronometer newEnemyTimer;
    #endregion

    #region Messages Methods of MonoBehaviour
    void Start()
    {
        lastRaffleEnemies = raffleEnemies;
        IsRaffleEnemies = raffleEnemies;
        lastIsRaffleEnemies = IsRaffleEnemies;

        newEnemyTimer = new Chronometer(GetRandomTimeByDifficulty(), this);
        newEnemyTimer.Start();
    }

    void Update()
    {
        if (lastRaffleEnemies != raffleEnemies)
        {
            lastRaffleEnemies = raffleEnemies;
            IsRaffleEnemies = raffleEnemies;
            lastIsRaffleEnemies = raffleEnemies;
        }

        if (lastIsRaffleEnemies != IsRaffleEnemies)
        {
            raffleEnemies = IsRaffleEnemies;
            lastRaffleEnemies = IsRaffleEnemies;
            lastIsRaffleEnemies = IsRaffleEnemies;
        }

        newEnemyTimer.UpdateCounter();
    }


    #endregion

    #region Other Methods
    public void OnReachTimeGoal()
    {
        newEnemyTimer = new Chronometer(GetRandomTimeByDifficulty(), this);

        if (raffleEnemies)
        {
            InitializeEnemies();
        }

        newEnemyTimer.Start();
    }

    #region GetRandom...    
    public int GetRandomNumberByDifficulty()
    {
        switch (LevelManager.CurrentLevelDifficulty)
        {
            case LevelDifficulty.Trainee:
            default:
                return Random.Range(0, 2);

            case LevelDifficulty.Easy:
                return Random.Range(1, 2);

            case LevelDifficulty.Beginner:
                return Random.Range(1, 3);

            case LevelDifficulty.Normal:
                return Random.Range(1, 4);

            case LevelDifficulty.Defiant:
                return Random.Range(2, 4);

            case LevelDifficulty.Experient:
                return Random.Range(2, 5);

            case LevelDifficulty.Extremist:
                return Random.Range(3, 5);
        }
    }

    public float GetRandomTimeByDifficulty()
    {
        switch (LevelManager.CurrentLevelDifficulty)
        {
            case LevelDifficulty.Trainee:
            default:
                return Random.Range(2.9f, 4.5f);

            case LevelDifficulty.Easy:
                return Random.Range(2.9f, 4.0f);

            case LevelDifficulty.Beginner:
                return Random.Range(2.9f, 3.8f);

            case LevelDifficulty.Normal:
                return Random.Range(2.5f, 3.5f);

            case LevelDifficulty.Defiant:
                return Random.Range(1.9f, 3.0f);

            case LevelDifficulty.Experient:
                return Random.Range(1.5f, 2.8f);

            case LevelDifficulty.Extremist:
                return Random.Range(1.0f, 2.5f);
        }
    }
    #endregion

    private void InitializeEnemies()
    {
        int numerOfEnemies = GetRandomNumberByDifficulty();

        for (int count = 0; count < numerOfEnemies; count++)
        {
            int enemyIndex = Random.Range(0, typesOfEnemies.Count);
            GameObject enemy = Instantiate<GameObject>(typesOfEnemies[enemyIndex]);
            
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                int randomSpeed = GetRandomNumberByDifficulty();
                randomSpeed = randomSpeed == 0 ? 1 : randomSpeed;
                enemyController.speedMovement = enemyController.speedMovement * randomSpeed;
            }
        }
    }
    #endregion

}