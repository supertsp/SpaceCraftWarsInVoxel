using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;


public class SoundManager : MonoBehaviour
{

    #region Component's Public Attributes [Allowed only classes that inherit MonoBehaviour or primitive types]

    #endregion

    #region Publics Properties [Aren't visible in Editor]

    #endregion

    #region Auxiliary Attributes or Properties  [Aren't visible in Editor]
    public static bool Mute { get; set; }
    #endregion

    #region Messages Methods of MonoBehaviour
    void Start()
    {
        
    }

    void Update()
    {
        AudioListener.pause = Mute;
    }
    #endregion

    #region Other Methods

    #endregion

}