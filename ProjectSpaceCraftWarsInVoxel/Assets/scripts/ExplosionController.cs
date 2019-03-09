using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;


public class ExplosionController : MonoBehaviour, ITimed
{

    #region Component's Public Attributes [Allowed only classes that inherit MonoBehaviour or primitive types]
    public float lifeTime;
    #endregion

    #region Publics Properties [Aren't visible in Editor]

    #endregion

    #region Auxiliary Attributes or Properties  [Aren't visible in Editor]
    private Chronometer durationTimer;
    #endregion

    #region Messages Methods of MonoBehaviour
    void Start()
    {
        durationTimer = new Chronometer(lifeTime, this);
        durationTimer.Start();

        if (VibrateManager.Enable)
        {
            Handheld.Vibrate();
        }
    }

    void Update()
    {
        durationTimer.UpdateCounter();
    }

    private void OnDestroy()
    {

    }
    #endregion

    #region Other Methods
    public void OnReachTimeGoal()
    {
        Destroy(gameObject);
    }
    #endregion

}