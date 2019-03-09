using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;


public class DisplayTimersController : MonoBehaviour
{

    #region Component's Public Attributes [Allowed only classes that inherit MonoBehaviour or primitive types]
    [Header("+ Timer Options")]
    [Space(4)]
    public List<float> timeMakers;

    [Space(5)]
    [Header("+ GameObject Options")]
    [Space(4)]
    public List<GameObject> objectsToShow;
    [Tooltip("To disable previous object when enable current object?")]
    public bool disablePrevious;
    #endregion

    #region Publics Properties [Aren't visible in Editor]

    #endregion

    #region Auxiliary Attributes or Properties  [Aren't visible in Editor]
    private List<Chronometer> timersToShow;
    private int indexExhibition;
    private bool reachEndList;    
    #endregion

    #region Messages Methods of MonoBehaviour
    void Start()
    {
        timersToShow = new List<Chronometer>(timeMakers.Count);

        for (int index = 0; index < timeMakers.Count; index++)
        {
            float time = timeMakers[index] == 0 ? 0.01f : timeMakers[index];
            timersToShow.Add(new Chronometer(time));
        }

        timersToShow[0].Start();        
    }

   

    void Update()
    {
        if (!reachEndList && timersToShow[indexExhibition].IsReachTimeGoal())
        {
            objectsToShow[indexExhibition].SetActive(true);

            if (indexExhibition > 0 && disablePrevious)
            {
                objectsToShow[indexExhibition - 1].SetActive(false);
            }

            indexExhibition++;

            if (indexExhibition >= timeMakers.Count)
            {
                reachEndList = true;
            }
            else
            {
                timersToShow[indexExhibition].Start();
            }
        }
    }
    #endregion

    #region Other Methods

    #endregion

}