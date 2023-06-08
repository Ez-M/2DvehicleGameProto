using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AutoTurretAimStuff;
using static AttackStuff;

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
                Vector3? autoAimTarget = CalculateAutoAimTarget(_turret: this);
                if(autoAimTarget != null){
                    turretPointer.PointGun((Vector3)autoAimTarget, gunWeight, pivotLimit, initialRotation);}
            }
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

    public void SetPlayerControlled(bool _setTo)
    {
        if(_setTo != isPlayerControlled)
        {
            isPlayerControlled = _setTo;
            UpdateTurretInputSub();
        }
    }

    private void UpdateTurretInputSub()
    {
        if (isPlayerControlled)
        { playerManager.InputManger.PlayerShoot += FireGun; }
        else { playerManager.InputManger.PlayerShoot -= FireGun; }
    }
}

public static class AttackStuff
{





    #region  attackTypeMethods
    public static void hitscanAttack(Turret attackSource)
    {



        float angle = attackSource.gunPivot.transform.rotation.eulerAngles.z;
        Quaternion rotationAngle = Quaternion.Euler(0f, 0f, angle);

        Vector2 targetDir = attackSource.gunPivot.transform.right.normalized;
        RaycastHit2D hit = Physics2D.Raycast(attackSource.gunEnd.transform.position, targetDir, attackSource.weaponData.baseAttackRange);
        attackSource.lineRenderer.SetPosition(0, attackSource.gunPivot.transform.position);
        if (hit.collider != null)
        {


            attackSource.lineRenderer.SetPosition(1, hit.point);
            var test = hit.collider.transform.root.GetComponent<IAttackable>();
            if (test != null)
            {
                test.IsAttacked(attackSource.gameObject, attackSource.weaponData);
            }


        }
        else
        {
            attackSource.lineRenderer.SetPosition(1, (Vector2)attackSource.gunEnd.transform.position + targetDir * attackSource.weaponData.baseAttackRange);
        }

    }


    public static void projectileAttack(Turret attackSource)
    {
        throw new NotImplementedException();
    }

    public static void areaAttack(Turret attackSource)
    {
        throw new NotImplementedException();
    }



    #endregion
}



public static class AutoTurretAimStuff
{
        public static Vector3? CalculateAutoAimTarget(Turret _turret)
    {
        List<Vehicle> _enemyVehicles = _turret.owner.CalculateEnemyVehicles();
        List<GameObject> _enemyObjects = new List<GameObject>();
        foreach (Vehicle vehicle in _enemyVehicles)
        { _enemyObjects.Add(vehicle.gameObject); }

        List<GameObject> _validTargetsByAngle = ValidTargetsByAngle(_objectsList: _enemyObjects, _turret: _turret);

        GameObject _closestVehicle = ClosestGameObject(_objectsList: _validTargetsByAngle, _turret: _turret);
        if(_closestVehicle != null)
        {
            return _closestVehicle.transform.position;
        } else {return null;}
        
    }

    public static List<GameObject> ValidTargetsByAngle(List<GameObject> _objectsList, Turret _turret)
    {
        List<GameObject> _outputList = new List<GameObject>();

        foreach (GameObject _currentObject in _objectsList)
        {
            Vector3 _direction = (_currentObject.transform.position - _turret.gunPivot.transform.position).normalized;
            float targetAngle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;

            // Adjust the target angle based on the initial rotation
            float adjustedTargetAngle = targetAngle - _turret.initialRotation;

            // Normalize the adjusted target angle to be within the range of -180 to 180 degrees
            if (adjustedTargetAngle > 180f)
                adjustedTargetAngle -= 360f;
            else if (adjustedTargetAngle < -180f)
                adjustedTargetAngle += 360f;



            if (adjustedTargetAngle < _turret.pivotLimit && adjustedTargetAngle > -_turret.pivotLimit)
            {
                _outputList.Add(_currentObject);
            }
        }


        return _outputList;
    }

    public static GameObject ClosestGameObject(List<GameObject> _objectsList, Turret _turret)
    { //this should probably go somewhere else to be honest

    #nullable enable
        GameObject? closestObject = null;
    #nullable disable
        foreach (var _gameObject in _objectsList)
        {
            var distanceFromObject = Vector3.Distance(_turret.gameObject.transform.position, _gameObject.transform.position);
            if (closestObject == null || distanceFromObject < Vector3.Distance(_turret.gameObject.transform.position, closestObject.transform.position))
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
}