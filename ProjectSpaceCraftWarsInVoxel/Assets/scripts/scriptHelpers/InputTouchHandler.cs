using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;


public class InputTouchHandler
{

    #region GetLastActionInWorldPosition: Touch
    /// <summary>
    /// Returns de Touch Position in World Units. 
    /// If a Touch action is not identified, a Vector3 with float.MaxValue will be returned.
    /// </summary>
    /// <param name="indexTouch">The finger touch identifier.</param>
    /// <returns>Vector3</returns>
    public static Vector3 GetLastActionInWorldPosition(int indexTouch)
    {
        Vector3 endPoint = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        RaycastHit hit;
        Ray ray;

        if (Input.anyKey)
        {
            ray = Camera.main.ScreenPointToRay(Input.GetTouch(indexTouch).position);

            if (Physics.Raycast(ray, out hit))
            {
                endPoint = hit.point;
            }
        }

        return endPoint;
    }

    /// <summary>
    /// Returns de Touch Position in World Units. 
    /// If a Touch action is not identified, a Vector3 with float.MaxValue will be returned.
    /// </summary>
    /// <param name="touchFinger">The object of finger touch identifier.</param>
    /// <returns>Vector3</returns>
    public static Vector3 GetLastActionInWorldPosition(Touch touchFinger)
    {
        Vector3 endPoint = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        RaycastHit hit;
        Ray ray;

        if (Input.anyKey)
        {
            ray = Camera.main.ScreenPointToRay(touchFinger.position);

            if (Physics.Raycast(ray, out hit))
            {
                endPoint = hit.point;
            }
        }

        return endPoint;
    }
    #endregion

    #region GetLastActionInWorldPosition: Mouse
    /// <summary>
    /// Returns de Mouse Position in World Units. If a Mouse action is not identified, a Vector3 with float.MaxValue will be returned.
    /// </summary>
    /// <returns>Vector3</returns>
    public static Vector3 GetLastActionInWorldPosition()
    {
        Vector3 endPoint = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        RaycastHit hit;
        Ray ray;

        if (Input.anyKey)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                endPoint = hit.point;
            }
        }

        return endPoint;
    }    
    #endregion

}