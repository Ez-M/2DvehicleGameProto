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
            if (isPlayerControlled)
            {
                turretPointer.PointGun(playerManager.InputManger.mouseWorldPosition, gunWeight, pivotLimit, initialRotation);
            }
            else
            {
                Vector3? autoAimTarget = CalculateAutoAimTarget();
                if(autoAimTarget != null){
                    turretPointer.PointGun((Vector3)autoAimTarget, gunWeight, pivotLimit, initialRotation);}
            }
        }
    }

    public Vector3? CalculateAutoAimTarget()
    {
        List<Vehicle> _enemyVehicles = owner.CalculateEnemyVehicles();
        List<GameObject> _enemyObjects = new List<GameObject>();
        foreach (Vehicle vehicle in _enemyVehicles)
        { _enemyObjects.Add(vehicle.gameObject); }

        List<GameObject> _validTargetsByAngle = ValidTargetsByAngle(_enemyObjects);

        GameObject _closestVehicle = ClosestGameObject(_validTargetsByAngle);
        if(_closestVehicle != null)
        {
            return _closestVehicle.transform.position;
        } else {return null;}
        
    }

    public List<GameObject> ValidTargetsByAngle(List<GameObject> _objectsList)
    {
        List<GameObject> _outputList = new List<GameObject>();

        foreach (GameObject _currentObject in _objectsList)
        {
            Vector3 _direction = (_currentObject.transform.position - gunPivot.transform.position).normalized;
            float targetAngle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;

            // Adjust the target angle based on the initial rotation
            float adjustedTargetAngle = targetAngle - initialRotation;

            // Normalize the adjusted target angle to be within the range of -180 to 180 degrees
            if (adjustedTargetAngle > 180f)
                adjustedTargetAngle -= 360f;
            else if (adjustedTargetAngle < -180f)
                adjustedTargetAngle += 360f;



            if (adjustedTargetAngle < pivotLimit && adjustedTargetAngle > -pivotLimit)
            {
                _outputList.Add(_currentObject);
            }
        }


        return _outputList;
    }

    public GameObject ClosestGameObject(List<GameObject> _ListObjects)
    { //this should probably go somewhere else to be honest

#nullable enable
        GameObject? closestObject = null;
#nullable disable
        foreach (var _gameObject in _ListObjects)
        {
            var distanceFromObject = Vector3.Distance(this.gameObject.transform.position, _gameObject.transform.position);
            if (closestObject == null || distanceFromObject < Vector3.Distance(this.transform.position, closestObject.transform.position))
            {
                closestObject = _gameObject;
            }
        }
        if (closestObject != null)
        {
            return closestObject;
        }
        else
        {
            return null;
        }

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
            if (test != null)
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



