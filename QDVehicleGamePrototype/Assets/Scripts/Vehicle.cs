using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Faction
{
    Player,
    NonPlayer,

}

public class Vehicle : MonoBehaviour, IAttackable
{   //this goes on the empty parent object of the vehicle entity

    public VehicleData vehicleData;

    public GameObject VehiclePrefab;
    public GameObject VehicleBody;
    [SerializeField]
    private Health healthComponent;
    public Health HealthComponent { get => healthComponent; }
    public EnemyManager enemyManager;

    public bool isPlayerControlled;
    public bool isFriendly;
    public bool canMove;
    public Faction faction;

    public GameObject currentTarget;

    
    private PlayerManager playerManager;
    public GameObject targetMCV;///probably always the player's main vehicle. MCV = main convoy vehicle

    public Vector2 moveTarget;

    public float verticalSpeed;
    public float horizontalSpeed;
    public float minAvoidRange;


    #region --Functions--
    void Awake()
    {
        playerManager = PlayerManager.Instance;
        enemyManager = EnemyManager.Instance;

        enemyManager.AddToAllVehicles(this);
    }

    private void Update()
    {
        Think();
        Do();
    }

    public void Think()
    {
        if(isPlayerControlled != true){CalcualteMoveTarget();}
        // CalculateAttackTarget();
    }


    public void Do()
    {
        if(isPlayerControlled != true)
        {
            if(canMove==true)
            {
                MoveTo();
            }
        }
        // Attack();
    }


    private void CalcualteMoveTarget()
    {
        Vector2 targetPosition = targetMCV.transform.position;
        Vector2 thisPosition = this.transform.position;
        Vector2 directionToTarget = targetMCV.transform.position - this.transform.position;
        float distanceFromTarget = directionToTarget.magnitude;

        if(distanceFromTarget <= minAvoidRange)
        {
            // moveTarget = targetPosition + (directionToTarget.normalized*minAvoidRange);
            moveTarget = thisPosition - (directionToTarget.normalized * (minAvoidRange - distanceFromTarget));
        }
        else
        {
            moveTarget = targetPosition + (directionToTarget.normalized * minAvoidRange);
        }
    }

    
    private void MoveTo()
    {
    float horizontalStep = horizontalSpeed * Time.deltaTime;
    float verticalStep = verticalSpeed * Time.deltaTime;

    Vector2 currentPosition = this.transform.position;
    Vector2 targetPosition = moveTarget;

    float newX = Mathf.MoveTowards(currentPosition.x, targetPosition.x, horizontalStep);
    float newY = Mathf.MoveTowards(currentPosition.y, targetPosition.y, verticalStep);

    this.transform.position = new Vector2(newX, newY);

    }

    private void CalculateAttackTarget()
    {
        // if(currentTarget != null){currentTarget = }
    }

    public List<Vehicle> CalculateEnemyVehicles()
    {
        var _outputList = new List<Vehicle>();
        foreach(var item in enemyManager.allVehiclesByID)
        {
            var _vehicle = item.Key;
            if(_vehicle.faction != this.faction)
            {
                _outputList.Add(_vehicle);
            }
        }
        return _outputList;
    }

    private void Attack()
    {
        throw new NotImplementedException();
    }



    public void IsAttacked(GameObject _attacker, WeaponData _weaponData)
    {
        var damage = _weaponData.baseDamage;
        HealthComponent.DamageHealth(damage);
        if(HealthComponent.canDie && HealthComponent.CurrentHealth <= 0)
        {
            StopCoroutine(PainEffect());
            StartCoroutine(PainEffect());
            string deathMessage =_attacker.name + " has destroyed"  + this.gameObject.name + " with " + damage + " damage";
            OnDeath(deathMessage);
        } else 
        {
            StopCoroutine(PainEffect());
            StartCoroutine(PainEffect());
            Debug.Log(_attacker.name + " has done " + damage + " damage to " + this.gameObject.name + "current health is now " + HealthComponent.CurrentHealth);
        }

    }




    public void OnDeath(string _deathMessage)
    {
        Debug.Log(_deathMessage);
        Destroy(this.gameObject);
    }


    public IEnumerator PainEffect()
    {
      var _sprite =  VehicleBody.GetComponent<SpriteRenderer>();
      _sprite.color = Color.red;
      
        while (_sprite.color.g < 1)
        {
            _sprite.color += new Color(0F, 0.2F, 0.2F);
            yield return new WaitForFixedUpdate();
        }
    }

    public void InitializeFromStats(VehicleData _inputData)
    {
        vehicleData = _inputData;
        healthComponent = this.gameObject.AddComponent<Health>();
        healthComponent.SetBaseHealth(vehicleData.baseMaxHealth);
        healthComponent.SetMaxHealth(_inputData.currentHealth);
        healthComponent.SetCurrentHealth(_inputData.currentHealth);
        canMove = _inputData.canMove;
        verticalSpeed = _inputData.verticalSpeed;
        horizontalSpeed = _inputData.horizontalSpeed;
    }

    #endregion


}
[Serializable]
public struct VehicleData
{
    public int vehicleID;
    public int chassisID; //The scriptableObject that originates this type of vehicle
    public int baseMaxHealth;
    public int currentMaxHealth;
    public int currentHealth;
    public bool canMove;
    public float verticalSpeed;
    public float horizontalSpeed;
}

public interface IAttackable
{
    void IsAttacked(GameObject _attacker, WeaponData _weaponData);
}