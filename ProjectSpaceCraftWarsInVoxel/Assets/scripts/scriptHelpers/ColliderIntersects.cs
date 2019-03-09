using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;


public class ColliderIntersects
{

    public static bool WithVector(Collider targetObject, Vector3 evaluatedVector)
    {
        return targetObject.bounds.Contains(evaluatedVector);
    }

    public static bool WithVector(GameObject targetObject, Vector3 evaluatedVector)
    {
        return targetObject.GetComponent<Collider>().bounds.Contains(evaluatedVector);
    }
    public static bool WithVectorFromMouse(GameObject targetObject)
    {
        return targetObject.GetComponent<Collider>().bounds.Contains(InputTouchHandler.GetLastActionInWorldPosition());
    }

    public static bool WithVectorFromTouch(Collider targetObject, int indexTouch)
    {
        return targetObject.bounds.Contains(InputTouchHandler.GetLastActionInWorldPosition(indexTouch));
    }

    public static bool WithVectorFromTouch(GameObject targetObject, int indexTouch)
    {
        return targetObject.GetComponent<Collider>().bounds.Contains(InputTouchHandler.GetLastActionInWorldPosition(indexTouch));
    }

}