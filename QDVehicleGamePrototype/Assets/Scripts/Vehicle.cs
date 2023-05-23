using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Vehicle : MonoBehaviour
{   //this goes on the empty parent object of the vehicle entity

    public GameObject VehiclePrefab;
    [SerializeField]
    private Health health;
    public Health Health { get => health; }

    public bool isPlayerControlled;
    public bool isFriendly;

    public GameObject currentTarget;

    public Vector2 moveTarget;
    private Player player;
    private PlayerManager playerManager;

    public GameObject targetMCV;///probably always the player's main vehicle. MCV = main convoy vehicle

    public float verticalSpeed;
    public float horizontalSpeed;
    public float minAvoidRange;
    void Awake()
    {
        playerManager = PlayerManager.Instance;
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
        MoveTo();
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
        this.transform.position = Vector2.Lerp(this.transform.position, moveTarget, Time.deltaTime*horizontalSpeed);
    }

    private void CalculateAttackTarget()
    {
        throw new NotImplementedException();
    }

    private void Attack()
    {
        throw new NotImplementedException();
    }



    public void OnDeath()
    {
        Destroy(this.gameObject);
    }


}