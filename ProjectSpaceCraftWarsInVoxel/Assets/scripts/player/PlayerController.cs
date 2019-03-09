using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerController : MonoBehaviour
{

    #region Component's Public Attributes [Allowed only classes that inherit MonoBehaviour or primitive types]
    [Header("+ Moviment Options")]
    [Space(4)]
    public bool movimentsEnable = true;
    public float movementSpeed = 10;
    [Range(1, 20)]
    public float positionYLimit = 13;
    public float positionZLocked = -20;
    [Range(1, 20)]
    public float freeFallWaitTime = 1;

    [Space(5)]
    [Header("+ Teleportation Options")]
    [Space(4)]
    public GameObject PlayerBornLimits;
    public AudioClip teleportationSound;

    [Space(5)]
    [Header("+ Jump Options")]
    [Space(4)]
    public bool jumpEnable = true;
    [Range(1, 60)]
    public float jumpLimit = 20;
    [Range(0.1f, 2.0f)]
    public float timeBetweenSoundJumps = 1.6f;
    public Collider groundCollider;
    public AudioClip jumpingSound;

    [Space(5)]
    [Header("+ Shot Options")]
    [Space(4)]
    public bool shotEnable = true;
    public GameObject shotPrefab;
    [Range(0, 1)]
    public float timeBetweenShots = 0.15f;

    [Space(5)]
    [Header("+ Dead Options")]
    [Space(4)]
    public bool deadEnable = true;
    public GameObject explosion;

    [Space(5)]
    [Header("+ Text for Debug")]
    [Space(4)]
    public Text TextDebug;
    #endregion

    #region Publics Properties [Aren't visible in Editor]    
    public bool IsJumping { get; set; }
    public bool FreeFallEnded { get; set; }
    public bool Crashed { get; set; }
    #endregion

    #region Auxiliary Attributes or Properties  [Aren't visible in Editor]
    private Rigidbody myRigidbody;
    private AudioSource myAudioSource;
    private Chronometer shotTimer;
    private Chronometer jumpSoundTimer;

    private Chronometer freeFallTimer;
    private bool initiatedFreeFallTimer = false;
    #endregion

    #region Messages Methods of MonoBehaviour
    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myAudioSource = GetComponent<AudioSource>();

        positionZLocked = transform.position.z;

        Crashed = false;

        shotTimer = new Chronometer(timeBetweenShots);
        shotTimer.Start();

        jumpSoundTimer = new Chronometer(timeBetweenSoundJumps);
        jumpSoundTimer.Start();
        IsJumping = false;

        freeFallTimer = new Chronometer(freeFallWaitTime);
        initiatedFreeFallTimer = false;
        FreeFallEnded = false;
    }

    private void Update()
    {
        #region FreeFall Timer Validation        
        if (!initiatedFreeFallTimer)
        {
            freeFallTimer.Start();
            DisableAllActions();
            initiatedFreeFallTimer = true;
        }

        if (initiatedFreeFallTimer && freeFallTimer.IsReachTimeGoal())
        {
            EnableAllActions();
            freeFallTimer.Stop();
            FreeFallEnded = true;
        }
        #endregion

        if (FreeFallEnded)
        {
            if (!IsJumping && transform.position.y > positionYLimit)
            {
                KeepSpaceshipOnHigthLimit();
            }
        }
    }

    private void OnDestroy()
    {
        if (Crashed)
        {
            GameObject newExplosion = Instantiate(explosion);
            newExplosion.transform.position = transform.position;
        }
    }

    #region  Colliders e Collisions
    private void OnTriggerEnter(Collider other)
    {
        KeepSpaceshipOnWidthLimit(other);

        ValidateCollision(other.gameObject.tag);
    }

    private void OnCollisionEnter(Collision other)
    {
        ValidateCollision(other.gameObject.tag);
    }
    #endregion

    #endregion

    #region Other Methods
    public void EnableAllActions()
    {
        movimentsEnable = true;
        jumpEnable = true;
        shotEnable = true;
        deadEnable = true;
    }

    public void DisableAllActions()
    {
        ActionNone();
        movimentsEnable = false;
        jumpEnable = false;
        shotEnable = false;
        deadEnable = false;
    }

    public bool IsEnableActions()
    {
        return movimentsEnable && jumpEnable && shotEnable && deadEnable;
    }

    public void ValidateCollision(string tag)
    {
        if (deadEnable)
        {
            switch (tag)
            {
                case "Asteroid":
                case "Spaceship":
                case "EnemyShot":
                    Crashed = true;
                    Destroy(gameObject);
                    break;
            }
        }
    }

    public void StopMoviments()
    {
        myRigidbody.Sleep();
    }

    #region SpaceshipActions: KeepSpaceshipOnHigthLimit(), KeepSpaceshipOnWidthLimit(), ActionNone(), ActionLeft(), ActionRight(), ActionFire() and ActionJump()

    #region enum SpaceshipAction
    public enum SpaceshipAction
    {
        None = 0,
        MoveLeft = 1,
        MoveRight = 3,
        Fire = 5,
        Jump = 7
    }
    #endregion

    #region KeepSpaceshipOnHigthLimit()
    public void KeepSpaceshipOnHigthLimit()
    {
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y > positionYLimit ? positionYLimit : transform.position.y,
            transform.position.z
        );
        transform.position = new Vector3(transform.position.x, transform.position.y, positionZLocked);
    }
    #endregion

    #region KeepSpaceshipOnWidthLimit() - Continuous Movement on the Sides: WestWall & EastWall
    public void KeepSpaceshipOnWidthLimit(Collider other)
    {
        if (other.gameObject.CompareTag("WestWall"))
        {
            myAudioSource.clip = teleportationSound;
            myAudioSource.Play();

            myRigidbody.Sleep();
            float x = PlayerBornLimits.transform.GetChild(1).transform.position.x;
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
            myRigidbody.AddForce(INITIAL_FORCE * -movementSpeed * 7, 0, 0);
        }

        if (other.gameObject.CompareTag("EastWall"))
        {
            myAudioSource.clip = teleportationSound;
            myAudioSource.Play();

            myRigidbody.Sleep();
            float x = PlayerBornLimits.transform.GetChild(0).transform.position.x;
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
            myRigidbody.AddForce(INITIAL_FORCE * movementSpeed * 7, 0, 0);
        }
    }
    #endregion

    #region ActionNone()
    public void ActionNone()
    {
        if (movimentsEnable)
        {
            GetComponent<Animator>().SetInteger("CurrentAction", (int)SpaceshipAction.None);

            if (myRigidbody != null)
            {
                myRigidbody.AddForce(0, 0, 0);
            }
        }
    }
    #endregion

    private const float INITIAL_FORCE = 10;

    #region ActionLeft()
    public void ActionLeft()
    {
        if (movimentsEnable)
        {
            GetComponent<Animator>().SetInteger("CurrentAction", (int)SpaceshipAction.MoveLeft);

            if (myRigidbody != null)
            {
                myRigidbody.AddForce(INITIAL_FORCE * -movementSpeed, 0, 0);
            }
        }
    }
    #endregion

    #region ActionRight()
    public void ActionRight()
    {
        if (movimentsEnable)
        {
            GetComponent<Animator>().SetInteger("CurrentAction", (int)SpaceshipAction.MoveRight);

            if (myRigidbody != null)
            {
                myRigidbody.AddForce(INITIAL_FORCE * movementSpeed, 0, 0);
            }
        }
    }
    #endregion

    #region ActionFire()
    public void ActionFire()
    {
        if (shotEnable && gameObject.activeSelf)
        {
            GetComponent<Animator>().SetInteger("CurrentAction", (int)SpaceshipAction.None);
            if (shotTimer.IsReachTimeGoal())
            {
                GameObject tempShot = Instantiate<GameObject>(shotPrefab);
                tempShot.transform.position = new Vector3(
                        transform.position.x,
                        transform.position.y + 1.5f,
                        transform.position.z + 5.5f
                    );
                shotTimer.Restart();
            }
        }
    }
    #endregion

    #region ActionJump()
    public void ActionJump()
    {
        if (movimentsEnable && jumpEnable && gameObject.activeSelf)
        {
            //control Jump Sound
            if (jumpSoundTimer.IsReachTimeGoal() && groundCollider.bounds.Intersects(GetComponent<Collider>().bounds))
            {
                myAudioSource.clip = jumpingSound;
                myAudioSource.Play();
                jumpSoundTimer.Restart();
            }

            if (!IsJumping)
            {
                IsJumping = true;
            }

            if (IsJumping && transform.position.y < jumpLimit)
            {
                GetComponent<Animator>().SetInteger("CurrentAction", 0);
                transform.position = Vector3.Lerp(
                    transform.position,
                    new Vector3(transform.position.x, jumpLimit + 0.6f, transform.position.z),
                    Time.deltaTime * 3.5f
                );
            }
            else
            {
                print("Is NOT Jumping");
                IsJumping = false;
            }
        }
    }
    #endregion

    #endregion

    #endregion



}
