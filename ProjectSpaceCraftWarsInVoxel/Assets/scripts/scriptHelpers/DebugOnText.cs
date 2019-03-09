using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;


public class DebugOnText
{

    public static int MAX_TEXT_LINES = 30;

    public static void Show(Text textComponent, object textMessage)
    {
        textComponent.text = textMessage.ToString();

        char[] limiter = new char[1];
        limiter[0] = '\n';
        if (textComponent.text.Split(limiter).Length > MAX_TEXT_LINES)
        {
            textComponent.text = "";
        }
    }

    public static void ShowConcatenated(Text textComponent, object textMessage)
    {
        textComponent.text += textMessage.ToString();

        char[] limiter = new char[1];
        limiter[0] = '\n';
        if (textComponent.text.Split(limiter).Length > MAX_TEXT_LINES)
        {
            textComponent.text = "";
        }
    }
    public static void ShowLine(Text textComponent, object textMessage)
    {
        textComponent.text += "\n" + textMessage.ToString();

        char[] limiter = new char[1];
        limiter[0] = '\n';
        if (textComponent.text.Split(limiter).Length > MAX_TEXT_LINES)
        {
            textComponent.text = "";
        }
    }


}