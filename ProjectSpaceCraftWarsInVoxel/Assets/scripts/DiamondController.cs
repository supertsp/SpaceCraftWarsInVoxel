using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;


public class DiamondController : MonoBehaviour
{

    #region Component's Public Attributes [Allowed only classes that inherit MonoBehaviour or primitive types]
    public int points = 50;
    public bool enableChangeColor = true;
    public bool isFeedbackSceneDiamond;
    #endregion

    #region Publics Properties [Aren't visible in Editor]

    #endregion

    #region Auxiliary Attributes or Properties  [Aren't visible in Editor]

    #endregion

    #region Messages Methods of MonoBehaviour
    void Start()
    {
        if (enableChangeColor)
        {
            ChangeColors();
        }
    }

    void Update()
    {
        if (enableChangeColor)
        {
            ChangeColors();
        }

        if (!LevelManager.PlayerWins && isFeedbackSceneDiamond)
        {
            gameObject.SetActive(false);
        }
    }
    #endregion

    #region Other Methods
    private void ChangeColors()
    {
        Renderer modelRenderer = transform.GetChild(0).GetComponent<Renderer>();
        MaterialPropertiesHandler.SetAlbedoColor(modelRenderer, LevelManager.GetCurrentColorByCurrentLevelDifficulty());

        if (transform.childCount > 1)
        {
            ParticleSystem.MainModule mainModule = transform.GetChild(1).GetComponent<ParticleSystem>().main;
            mainModule.startColor = new ParticleSystem.MinMaxGradient(LevelManager.GetCurrentColorByCurrentLevelDifficulty());
        }
    }
    #endregion

}