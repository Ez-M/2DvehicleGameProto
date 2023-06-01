using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    public GameObject gunBarrel;
    public GameObject gunPivot;
    public GameObject gunEnd;
    public bool isFunctional;
    public PlayerManager playerManager;
    public bool isPlayerControlled;
    public Vehicle owner;
    public LineRenderer lineRenderer;
    public TurretPointer turretPointer;
    public float initialRotation;
    public float gunWeight;
    public int pivotLimit;
    public float weaponRange;
    public WeaponData weaponData;
    public float nextShotTime;

    void Awake()
    {
        playerManager = PlayerManager.Instance;
        turretPointer = new TurretPointer();





    }
    
    void OnEnable()
    {
        playerManager.InputManger.PlayerShoot += FireGun;

    }

    void OnDisable()
    {
        playerManager.InputManger.PlayerShoot -= FireGun;

    }

    void Start()
    {
        initialRotation = gunPivot.transform.rotation.eulerAngles.z;
        turretPointer.gunPivot = gunPivot.transform;
    }

    void Update()
    {
        if (isFunctional)
        {
            if(isPlayerControlled)
            {
                turretPointer.PointGun(playerManager.InputManger.mouseWorldPosition, gunWeight, pivotLimit, initialRotation);
            } else
            {
                Vector3 autoAimTarget = CalculateAutoAimTarget();
                turretPointer.PointGun(autoAimTarget, gunWeight, pivotLimit, initialRotation);
            }
        }
    }

    public Vector3 CalculateAutoAimTarget()
    {
        var _enemyVehicles = owner.CalculateEnemyVehicles();
        // List<Vector3> enemyPositions = new List<Vector3>();
        // List<float> distanceFromEnemies = new List<float>();
        #nullable enable
        Vehicle? closestVehicle = null;
        #nullable disable
        // float shortestDistance = 1000f;
        foreach(var vehicle in _enemyVehicles)
        {
            var distanceFromVehicle = Vector3.Distance(this.gameObject.transform.position, vehicle.gameObject.transform.position);
            if(closestVehicle == null || distanceFromVehicle < Vector3.Distance(this.transform.position, closestVehicle.transform.position))
            {
                closestVehicle = vehicle;
            }
            // distanceFromEnemies.Add(distanceFromVehicle);
        }
        
        return closestVehicle.gameObject.transform.position;
        // list of all enemy vehicles -> for each vehicle if(Distance(this, _vehicle)) then         -> return _closestVehicle.gameobject.transform.position;
    }


    public void FireGun()
    {

        if (Time.time >= nextShotTime)
        {
            switch (weaponData.attackType)
            {
                case AttackType.hitScan:
                    hitscanAttack(attackSource: this);
                    break;
                case AttackType.projectile:
                    projectileAttack(attackSource: this);
                    break;
                case AttackType.area:
                    areaAttack(attackSource: this);
                    break;
            }

            nextShotTime = Time.time + (60 / weaponData.baseFireRate);

            if (weaponData.isRapidFire && playerManager.InputManger.fireInputHeld == true)
            {
                StartCoroutine(AutoFire());
            }
        }

    }


    private IEnumerator AutoFire()
    {
        while (playerManager.InputManger.fireInputHeld == true)
        {
            FireGun();

            yield return new WaitForSeconds(nextShotTime - Time.time);
        }
    }




    #region  attackTypeMethods
    void hitscanAttack(Turret attackSource)
    {

        // var weaponData = attackSource.weaponData;
        // var gunPivot = attackSource.gunPivot;
        // var lineRenderer = attackSource.lineRenderer;
        // var gunEnd = attackSource.gunEnd;
        // var weaponRange = attackSource.weaponRange;


        float angle = gunPivot.transform.rotation.eulerAngles.z;
        Quaternion rotationAngle = Quaternion.Euler(0f, 0f, angle);

        Vector2 targetDir = gunPivot.transform.right.normalized;
        RaycastHit2D hit = Physics2D.Raycast(gunEnd.transform.position, targetDir, weaponRange);
        lineRenderer.SetPosition(0, gunPivot.transform.position);
        if (hit.collider != null)
        {


            lineRenderer.SetPosition(1, hit.point);
            var test = hit.collider.transform.root.GetComponent<IAttackable>();
            if( test != null)
            {
                test.IsAttacked(this.gameObject, this.weaponData);
            }


        }
        else
        {
            lineRenderer.SetPosition(1, (Vector2)gunEnd.transform.position + targetDir * weaponRange);
        }

    }


    void projectileAttack(Turret attackSource)
    {
        throw new NotImplementedException();
    }

    void areaAttack(Turret attackSource)
    {
        throw new NotImplementedException();
    }



    #endregion




}



