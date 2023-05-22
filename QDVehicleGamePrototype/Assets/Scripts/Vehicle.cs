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

    private GameObject targetMCV;///probably always the player's main vehicle. MCV = main convoy vehicle

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
        CalculateAttackTarget();
    }


    public void Do()
    {
        MoveTo();
        Attack();
    }


    private void CalcualteMoveTarget()
    {
        
    }

    
    private void MoveTo()
    {
        throw new NotImplementedException();
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