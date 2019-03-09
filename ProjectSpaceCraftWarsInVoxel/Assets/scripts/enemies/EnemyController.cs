using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


public class EnemyController : MonoBehaviour
{

    #region Component's Public Attributes [Allowed only classes that inherit MonoBehaviour or primitive types]
    [Header("+ Enemy Properties")]
    [Space(4)]
    public EnemyType enemyType;
    public int points = 5;
    public float positionYLimit;
    public bool enableActions = true;

    [Space(5)]
    [Header("+ Born Limits")]
    [Space(4)]
    public GameObject westLimit;
    public GameObject eastLimit;

    [Space(5)]
    [Header("+ Shot Configurations")]
    [Space(4)]
    public GameObject shot;
    public float timeBetweenShots = 0.25f;
    public bool shootable;

    [Space(5)]
    [Header("+ Speed Configurations")]
    [Space(4)]
    public bool useTorqueRotation = true;
    public Vector3 speedRotation;
    public float speedMovement = -1;

    [Space(5)]
    [Header("+ Explosion Configurations")]
    [Space(4)]
    public GameObject explosion;
    
    #endregion

    #region Publics Properties [Aren't visible in Editor]
    public bool ReceiveShot { get; set; }
    public bool ReceiveSelfShot { get; set; }
    public bool DestroyedByEnemy { get; set; }
    public bool DestroyedByPlayer { get; set; }
    #endregion

    #region Auxiliary Attributes or Properties  [Aren't visible in Editor]
    private Rigidbody myRigidbody;
    private Chronometer shotTimer;
    #endregion

    #region Messages Methods of MonoBehaviour
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        transform.position = new Vector3(
            Random.Range(westLimit.transform.position.x, eastLimit.transform.position.x),
            transform.position.y,
            transform.position.z
        );

        shotTimer = new Chronometer(timeBetweenShots);
        shotTimer.Start();
    }

    void Update()
    {
        if (transform.position.y > positionYLimit)
        {
            transform.position = new Vector3(
                transform.position.x,
                positionYLimit,
                transform.position.z
            );
        }
        
        if (enableActions && shootable)
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        if (enableActions)
        {
            if (useTorqueRotation)
            {
                myRigidbody.AddTorque(1000 * speedRotation * Time.deltaTime);
            }
            else
            {
                transform.Rotate(10 * speedRotation * Time.deltaTime);
            }

            float multiplier = enemyType == EnemyType.Asteroid ? 100 : 1000;

            Vector3 tempSpeed = new Vector3(
                0, 0,
                multiplier * speedMovement * Time.deltaTime
            );
            myRigidbody.AddForce(tempSpeed);
        }
    }

    private void OnDestroy()
    {
        if (ReceiveShot)
        {
            GameObject newExplosion = Instantiate(explosion);
            newExplosion.transform.position = transform.position;

            if (!DestroyedByEnemy)
            {
                ScoreboardManager.CurrentPoints += points;
            }
        }

    }

    #region Colliders e Collisions
    private void OnTriggerEnter(Collider other)
    {
        ShotController shotCollider = other.gameObject.GetComponent<ShotController>();
        
        if (shotCollider != null)
        {
            ValidateColliders(
                other.gameObject.tag,
                shotCollider.ShooterName
            );
        }
        else
        {
            ValidateColliders(
                other.gameObject.tag,
                ""
            );
        }        
    }

    private void OnCollisionEnter(Collision other)
    {
        ShotController shotCollider = other.gameObject.GetComponent<ShotController>();

        if (shotCollider != null)
        {
            ValidateColliders(
                other.gameObject.tag,
                shotCollider.ShooterName
            );
        }
        else
        {
            ValidateColliders(
                other.gameObject.tag,
                ""
            );
        }
    }
    #endregion

    #endregion

    #region Other Methods
    private void ValidateColliders(string tag, string shooterName)
    {
        if ((tag == "Shot" || tag == "EnemyShot") && shooterName != gameObject.name)
        {
            ReceiveShot = true;

            DestroyedByEnemy = tag == "EnemyShot" ? true : false;

            Destroy(gameObject);
        }
    }

    private void Shoot()
    {
        if (shotTimer.IsReachTimeGoal())
        {
            GameObject tempShot = Instantiate<GameObject>(shot);
            tempShot.transform.position = new Vector3(
                    transform.position.x,
                    transform.position.y + 1.4f,
                    transform.position.z + -9.0f
                );

            tempShot.transform.GetChild(0).GetComponent<ShotController>().ShooterName = gameObject.name;
            tempShot.transform.GetChild(1).GetComponent<ShotController>().ShooterName = gameObject.name;

            float tempSpeedShot = tempShot.transform.GetChild(0).GetComponent<ShotController>().movementSpeed * speedMovement * -1;
            ShotController left = tempShot.transform.GetChild(0).GetComponent<ShotController>();
            left.movementSpeed = tempSpeedShot;

            ShotController right = tempShot.transform.GetChild(1).GetComponent<ShotController>();
            right.movementSpeed = tempSpeedShot;


            shotTimer.Restart();
        }
    }
    #endregion

    #region enum EnemyType
    public enum EnemyType
    {
        Asteroid,
        Spaceship,
        Boss
    }
    #endregion

}