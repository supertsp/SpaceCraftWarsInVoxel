using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;


public class OptionsSelectedController : MonoBehaviour
{

    #region Component's Public Attributes [Allowed only classes that inherit MonoBehaviour or primitive types]
    [Header("+ Language Selection")]
    [Space(4)]
    public Toggle english;
    public Toggle brazilianPortuguese;

    [Space(5)]
    [Header("+ Sound Selection")]
    [Space(4)]
    public Toggle soundOn;
    public Toggle soundOff;

    [Space(5)]
    [Header("+ Vibrate Selection")]
    [Space(4)]
    public Toggle vibrateOn;
    public Toggle vibrateOff;
    #endregion

    #region Publics Properties [Aren't visible in Editor]

    #endregion

    #region Auxiliary Attributes or Properties  [Aren't visible in Editor]   

    #endregion

    #region Messages Methods of MonoBehaviour
    void Start()
    {
        UpdateTogglesOnStart();
    }

    void Update()
    {
        UpdateTogglesOnClick();
    }
    #endregion

    #region Other Methods
    public void ChangeToggleLanguage(bool value)
    {
        if (value)
        {
            english.isOn = true;
            LevelManager.CurrentLevelLanguage = Language.English;
        }
        else
        {
            brazilianPortuguese.isOn = true;
            LevelManager.CurrentLevelLanguage = Language.BrazilianPortuguese;
        }
    }

    public void ChangeToggleSound(bool value)
    {
        if (value)
        {
            soundOn.isOn = true;
            SoundManager.Mute = false;
        }
        else
        {
            soundOff.isOn = true;
            SoundManager.Mute = true;
        }
    }

    public void ChangeToggleVibrate(bool value)
    {
        if (value)
        {
            vibrateOn.isOn = true;
            VibrateManager.Enable = true;
        }
        else
        {
            vibrateOff.isOn = true;
            VibrateManager.Enable = false;
        }
    }

    private bool vibrateOnce;

    private void UpdateTogglesOnClick()
    {
        if (english.isOn)
        {
            LevelManager.CurrentLevelLanguage = Language.English;
        }
        else if (brazilianPortuguese.isOn)
        {
            LevelManager.CurrentLevelLanguage = Language.BrazilianPortuguese;
        }

        if (soundOn.isOn)
        {
            SoundManager.Mute = false;
        }
        else if (soundOff.isOn)
        {
            SoundManager.Mute = true;
        }

        if (vibrateOn.isOn)
        {
            VibrateManager.Enable = true;

            if (!vibrateOnce)
            {
                Handheld.Vibrate();
                vibrateOnce = true;
            }
        }
        else if (vibrateOff.isOn)
        {
            VibrateManager.Enable = false;
            vibrateOnce = false;
        }
    }

    private void UpdateTogglesOnStart()
    {
        if (LevelManager.CurrentLevelLanguage == Language.English)
        {
            ChangeToggleLanguage(true);
        }
        else
        {
            ChangeToggleLanguage(false);
        }

        if (SoundManager.Mute == true)
        {
            ChangeToggleSound(false);
        }
        else
        {
            ChangeToggleSound(true);
        }

        if (VibrateManager.Enable == true)
        {
            ChangeToggleVibrate(true);
        }
        else
        {
            ChangeToggleVibrate(false);
        }
    }
    #endregion

}