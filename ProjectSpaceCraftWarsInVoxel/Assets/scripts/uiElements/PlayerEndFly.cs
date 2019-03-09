using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;


public class PlayerEndFly : MonoBehaviour
{

    #region Component's Public Attributes [Allowed only classes that inherit MonoBehaviour or primitive types]
    public float speedMovement;
    public float timeToStartMovement;
    public AudioClip spaceshipSound;
    public float timeToDestroySpaceship;
    #endregion

    #region Publics Properties [Aren't visible in Editor]

    #endregion

    #region Auxiliary Attributes or Properties  [Aren't visible in Editor]    
    private Chronometer timerToStartMovement;
    private Chronometer timerToDestroySpaceship;
    #endregion

    #region Messages Methods of MonoBehaviour
    void Start()
    {
        timerToStartMovement = new Chronometer(timeToStartMovement);
        timerToStartMovement.Start();

        timerToDestroySpaceship = new Chronometer(timeToDestroySpaceship);
    }

    private int indexExhibition;

    void Update()
    {
        if (timerToStartMovement.IsReachTimeGoal())
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(100 * speedMovement, 0, 100 * speedMovement) * Time.deltaTime);

            if (!GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().clip = spaceshipSound;
                GetComponent<AudioSource>().Play();

                timerToDestroySpaceship.Start();
            }

            if (timerToDestroySpaceship.IsReachTimeGoal())
            {
                Destroy(gameObject);
            }

        }
    }
    #endregion

    #region Other Methods

    #endregion

}