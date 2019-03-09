using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;


public class MusicLevelController : MonoBehaviour
{

    #region Component's Public Attributes [Allowed only classes that inherit MonoBehaviour or primitive types]
    public List<AudioClip> levelMusics;
    #endregion

    #region Publics Properties [Aren't visible in Editor]

    #endregion

    #region Auxiliary Attributes or Properties  [Aren't visible in Editor]
    private LevelDifficulty lastDifficulty;
    #endregion

    #region Messages Methods of MonoBehaviour
    void Start()
    {
        lastDifficulty = LevelManager.CurrentLevelDifficulty;

        GetComponent<AudioSource>().clip = levelMusics[(int)LevelManager.CurrentLevelDifficulty];
        GetComponent<AudioSource>().Play();
        lastDifficulty = LevelManager.CurrentLevelDifficulty;
    }

    void Update()
    {
        if (lastDifficulty != LevelManager.CurrentLevelDifficulty)
        {
            GetComponent<AudioSource>().clip = levelMusics[(int)LevelManager.CurrentLevelDifficulty];
            GetComponent<AudioSource>().Play();
            lastDifficulty = LevelManager.CurrentLevelDifficulty;
        }
    }
    #endregion

    #region Other Methods

    #endregion

}