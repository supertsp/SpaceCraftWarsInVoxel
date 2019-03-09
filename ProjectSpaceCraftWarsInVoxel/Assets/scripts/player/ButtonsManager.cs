using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;


public class ButtonsManager : MonoBehaviour
{

    #region Component's Public Attributes [Allowed only classes that inherit MonoBehaviour or primitive types]    
    public TouchButtonController buttonLeft;
    public TouchButtonController buttonRight;
    public TouchButtonController buttonFire;
    public GameObject buttonJumping;
    #endregion

    #region Publics Properties [Aren't visible in Editor]
    public PlayerController player;
    #endregion

    #region Auxiliary Attributes or Properties  [Aren't visible in Editor]
    private bool jumping;
    #endregion

    #region Messages Methods of MonoBehaviour
    void Start()
    {

    }

    void Update()
    {
        if (player != null && player.gameObject.activeSelf && player.FreeFallEnded)
        {
            UpdateTouchInput();
            UpdateKeyboardInput();
        }

        UpdateLanguageText();
    }
    #endregion

    #region Other Methods

    private void UpdateLanguageText()
    {
        switch (LevelManager.CurrentLevelLanguage)
        {
            case Language.English:
                buttonLeft.transform.GetChild(0).GetComponent<Text>().text = "L";
                buttonRight.transform.GetChild(0).GetComponent<Text>().text = "R";
                buttonFire.transform.GetChild(0).GetComponent<Text>().text = "fire";
                buttonJumping.transform.GetChild(0).GetComponent<Text>().text = "jumping";
                break;

            case Language.BrazilianPortuguese:
                buttonLeft.transform.GetChild(0).GetComponent<Text>().text = "E";
                buttonRight.transform.GetChild(0).GetComponent<Text>().text = "D";
                buttonFire.transform.GetChild(0).GetComponent<Text>().text = "tiro";
                buttonJumping.transform.GetChild(0).GetComponent<Text>().text = "pulando";
                break;
        }
    }

    #region UpdateInputs...       
    private void UpdateTouchInput()
    {
        //JUMP FIRE
        if (buttonLeft.HasActionPhase() && buttonRight.HasActionPhase() && buttonFire.HasActionPhase())
        {
            JumpFire();
        }
        //JUMP
        //if (buttonLeft.HasActionPhase() && buttonRight.HasActionPhase())
        else if (buttonLeft.HasActionPhase() && buttonRight.HasActionPhase())
        {
            Jump();
        }
        //LEFT FIRE
        else if (buttonLeft.HasActionPhase() && buttonFire.HasActionPhase())
        {
            LeftFire();
        }
        //RIGHT FIRE
        else if (buttonRight.HasActionPhase() && buttonFire.HasActionPhase())
        {
            RightFire();
        }
        //LEFT
        else if (buttonLeft.HasActionPhase())
        {
            Left();
        }
        //RIGHT
        else if (buttonRight.HasActionPhase())
        {
            Right();
        }
        //FIRE
        else if (buttonFire.HasActionPhase())
        {
            Fire();
        }
        //NONE
        else
        {
            None();
        }
    }

    private void UpdateKeyboardInput()
    {
        //JUMP FIRE
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.Space))
        {
            JumpFire();
        }
        //JUMP
        else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            Jump();
        }
        //LEFT FIRE
        else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.Space))
        {
            LeftFire();
        }
        //RIGHT FIRE
        else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.Space))
        {
            RightFire();
        }
        //LEFT
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            Left();
        }
        //RIGHT
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Right();
        }
        //FIRE
        else if (Input.GetKey(KeyCode.Space))
        {
            Fire();
        }
        //NONE
        else
        {
            None();
        }
    }
    #endregion

    #region Actions
    private void JumpFire()
    {
        player.ActionJump();
        buttonJumping.SetActive(true);
        buttonJumping.transform.GetChild(0).gameObject.SetActive(true);
        player.ActionFire();
        //Player.IsJumping = true;
    }

    private void Jump()
    {
        player.ActionJump();
        buttonJumping.SetActive(true);
        buttonJumping.transform.GetChild(0).gameObject.SetActive(true);
        //Player.IsJumping = true;
    }

    private void LeftFire()
    {
        player.ActionFire();
        player.ActionLeft();
        //Player.IsJumping = false;
    }

    private void RightFire()
    {
        player.ActionFire();
        player.ActionRight();
        //Player.IsJumping = false;
    }

    private void Left()
    {
        player.ActionLeft();
        //Player.IsJumping = false;
    }

    private void Right()
    {
        player.ActionRight();
        //Player.IsJumping = false;
    }

    private void Fire()
    {
        player.ActionFire();
        //Player.IsJumping = false;
    }

    private void None()
    {
        player.ActionNone();
        //Player.IsJumping = false;
    }
    #endregion

    #endregion

}