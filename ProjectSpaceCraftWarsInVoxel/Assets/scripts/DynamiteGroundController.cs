using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;


public class DynamiteGroundController : MonoBehaviour
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
        
    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject);
    }
    #endregion

    #region Other Methods

    #endregion

}