using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Xml;
using System;
using System.Collections;
using System.Collections.Generic;


public class ScoreboardManager : MonoBehaviour
{

    #region Component's Public Attributes [Allowed only classes that inherit MonoBehaviour or primitive types]
    [Header("+ Exhibition Options")]
    [Space(4)]
    [Tooltip("The maximum number of points for GetScoreboardList() method")]
    public int maximumNumberOfPoints = 5;

    [Space(5)]
    [Header("+ Debug Options")]
    [Space(4)]
    [Tooltip("Enable Debug on Text Canvas element")]
    public bool enableDebugOnCanvas;
    public Text textDebug;

    #endregion

    #region Publics Properties [Aren't visible in Editor]
    public static int CurrentPoints { get; set; }
    public static int CurrentAccumulatedPoints { get; set; }
    public static int MaximumNumberOfPoints { get; set; }
    #endregion

    #region Auxiliary Attributes or Properties  [Aren't visible in Editor]
    private static FileXML scoreXML;
    #endregion

    #region Messages Methods of MonoBehaviour
    private void Awake()
    {
        scoreXML = new FileXML("ScoreXML");

        try
        {
            scoreXML.GetElementByTagNameAndIndex("scoreboard", 0);
        }
        catch (Exception e)
        {
            string m = e.Message;
            scoreXML.AddElement("scoreboard");
        }
    }

    void Start()
    {
        if (enableDebugOnCanvas)
        {
            DebugOnText.ShowConcatenated(textDebug, scoreXML.CurrentFilePath);
        }
    }

    void Update()
    {
        MaximumNumberOfPoints = maximumNumberOfPoints;
    }
    #endregion

    #region Other Methods
    public static void SaveCurrentAccumulatedPointsToScoreXML()
    {
        scoreXML.AddElementBeforeAsChildOf("scoreboard", "points", "value", CurrentAccumulatedPoints);

        List<int> scoreList = GetScoreboardList();
        scoreList.Sort();

        scoreXML.DeleteAllXMLElements();
        scoreXML.AddElement("scoreboard");

        foreach (var item in scoreList)
        {
            scoreXML.AddElementBeforeAsChildOf("scoreboard", "points", "value", item);
        }
    }

    public static List<int> GetScoreboardLimitedList()
    {
        List<string> pointsString = scoreXML.GetAttributeValuesByElementsTagName("points", 0);
        List<int> pointsInt = new List<int>(MaximumNumberOfPoints);

        for (int index = 0; index < pointsString.Count; index++)
        {
            if (index < MaximumNumberOfPoints)
            {
                pointsInt.Add(int.Parse(pointsString[index]));
            }
            else
            {
                break;
            }
        }

        return pointsInt;
    }

    public static List<int> GetScoreboardList()
    {
        List<string> pointsString = scoreXML.GetAttributeValuesByElementsTagName("points", 0);
        List<int> pointsInt = new List<int>(MaximumNumberOfPoints);

        for (int index = 0; index < pointsString.Count; index++)
        {
            if (index < MaximumNumberOfPoints)
            {
                pointsInt.Add(int.Parse(pointsString[index]));
            }
            else
            {
                break;
            }
        }

        return pointsInt;
    }
    #endregion

}