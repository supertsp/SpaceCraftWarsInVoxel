using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;


public class LevelTimerBorder : MonoBehaviour
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

    }

    void Update()
    {       
        Color color = LevelManager.GetCurrentColorByCurrentLevelDifficulty();
        color.a = 152 / 255f;
        GetComponent<Image>().color = color;
    }
    #endregion

    #region Other Methods

    #endregion

}