using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;


public class ShotController : MonoBehaviour
{

    #region Component's Public Attributes [Allowed only classes that inherit MonoBehaviour or primitive types]
    public float movementSpeed = 25;
    public bool enableEnemyShot;
    #endregion

    #region Publics Properties [Aren't visible in Editor]
	public string ShooterName { get; set; }
    #endregion

    #region Auxiliary Attributes or Properties  [Aren't visible in Editor]
    private const float INITIAL_FORCE = 10;
    private Rigidbody myRigidbody;
    private Text textDebug;
    #endregion

    #region Messages Methods of MonoBehaviour
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        myRigidbody.AddForce(0, 0, INITIAL_FORCE * movementSpeed);
    }

    #region Colliders e Collisions
    private void OnTriggerEnter(Collider other)
    {
        ValidateColliders(other.gameObject.tag);
    }

    private void OnCollisionEnter(Collision other)
    {
        ValidateColliders(other.gameObject.tag);
    }
    #endregion

    #endregion

    #region Other Methods
    private void ValidateColliders(string tag)
    {
        if (enableEnemyShot)
        {
            switch (tag)
            {
                case "SouthWall":
                case "Asteroid":
                    Destroy(gameObject.transform.parent.gameObject);
                    break;
            }
        }
        else
        {
            switch (tag)
            {
                case "NorthWall":
                case "SouthWall":
                case "Asteroid":
                    Destroy(gameObject.transform.parent.gameObject);
                    break;
            }
        }
    }
    #endregion

}