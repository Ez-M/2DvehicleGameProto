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

    public GameObject VehiclePrefab;
    public GameObject VehicleBody;
    [SerializeField]
    private Health health;
    public Health Health { get => health; }
    public EnemyManager enemyManager;

    public bool isPlayerControlled;
    public bool isFriendly;
    public bool canMove;
    public Faction faction;

    public GameObject currentTarget;

    
    private Player player;
    private PlayerManager playerManager;
    public GameObject targetMCV;///probably always the player's main vehicle. MCV = main convoy vehicle

    public Vector2 moveTarget;

    public float verticalSpeed;
    public float horizontalSpeed;
    public float minAvoidRange;
    
    void Awake()
    {
        playerManager = PlayerManager.Instance;
        enemyManager = EnemyManager.Instance;
        player = playerManager.Player;
    }

    private void Update()
    {
        Think();
        Do();
    }

    public void Think()
    {
        CalcualteMoveTarget();
        // CalculateAttackTarget();
    }


    public void Do()
    {
        if(canMove==true){MoveTo();}
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

    public void CalculateEnemyVehicles()
    {
        Dictionary< GameObject, Faction> _AllVehicles = enemyManager.getAllVehicles();
        foreach( KeyValuePair<GameObject, Faction> KvP in _AllVehicles)
        {
            if(KvP.Value != this.faction) 
            {   List<GameObject> _EnemyVehicles = new List<GameObject>();
                _EnemyVehicles.Add(KvP.Key);
            }
        }
    }

    private void Attack()
    {
        throw new NotImplementedException();
    }



    public void IsAttacked(GameObject _attacker, WeaponData _weaponData)
    {
        var damage = _weaponData.baseDamage;
        Health.DamageHealth(damage);
        if(Health.canDie && Health.CurrentHealth <= 0)
        {
            StopCoroutine(PainEffect());
            StartCoroutine(PainEffect());
            string deathMessage =_attacker.name + " has destroyed"  + this.gameObject.name + " with " + damage + " damage";
            OnDeath(deathMessage);
        } else 
        {
            StopCoroutine(PainEffect());
            StartCoroutine(PainEffect());
            Debug.Log(_attacker.name + " has done " + damage + " damage to " + this.gameObject.name + "current health is now " + Health.CurrentHealth);
        }

    }




    public void OnDeath(string _deathMessage)
    {
        Debug.Log(_deathMessage);
        Destroy(this.gameObject);
    }

    // public void PainEffect()
    // {


    // }

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

}

public interface IAttackable
{
    public void IsAttacked(GameObject _attacker, WeaponData _weaponData);
}