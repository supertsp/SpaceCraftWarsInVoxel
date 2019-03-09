using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;


public class BackgroundController : MonoBehaviour
{

    #region Component's Public Attributes [Allowed only classes that inherit MonoBehaviour or primitive types]
    public float paralaxSpeed;
    public bool enableChangeColor;
    #endregion

    #region Publics Properties [Aren't visible in Editor]

    #endregion

    #region Auxiliary Attributes or Properties  [Aren't visible in Editor]
    private const float INCREMENT = -0.005f;
    private float y;
    private bool componentAdded;
    private LevelDifficulty lastLevelDifficulty;
    #endregion

    #region Messages Methods of MonoBehaviour
    void Start()
    {
        lastLevelDifficulty = LevelManager.CurrentLevelDifficulty;
    }

    void Update()
    {
        y += INCREMENT * paralaxSpeed * LevelManager.GetMultiplierByCurrentLevelDifficulty();
        MaterialPropertiesHandler.SetOffset(GetComponent<MeshRenderer>(), 0, y);

        if (lastLevelDifficulty != LevelManager.CurrentLevelDifficulty)
        {
            lastLevelDifficulty = LevelManager.CurrentLevelDifficulty;
            Destroy(gameObject.GetComponent<ColorFadeEffect>());
            componentAdded = false;
        }

        if (!componentAdded)
        {
            ColorFadeEffect tempComponent = gameObject.AddComponent<ColorFadeEffect>();

            if (enableChangeColor)
            {
                tempComponent.renderColor = LevelManager.GetCurrentColorByCurrentLevelDifficulty();
                tempComponent.ResetComponent();
            }
            else
            {
                tempComponent.renderColor = Color.white;
            }

            componentAdded = true;
        }

        gameObject.GetComponent<ColorFadeEffect>().speedTransition *= LevelManager.GetMultiplierByCurrentLevelDifficulty() / 4f;
    }
    #endregion

    #region Other Methods

    #endregion

}