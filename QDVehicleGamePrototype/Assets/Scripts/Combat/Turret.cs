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
    public LineRenderer lineRenderer;
    public TurretPointer turretPointer;
    public float gunWeight;
    public int pivotLimit;
    public float initialRotation;

    void Awake()
    {
        playerManager = PlayerManager.Instance;
        turretPointer = new TurretPointer();


        playerManager.InputManger.OnShoot += FireGun;

        

    }

    void Start()
    {
        initialRotation = gunPivot.transform.rotation.eulerAngles.z;
        turretPointer.gunPivot = gunPivot.transform;
    }

    void Update()
    {
        if(isFunctional)
        {
        turretPointer.PointGun(playerManager.InputManger.mouseWorldPosition, gunWeight, pivotLimit, initialRotation);
        }
    }

    public void FireGun()
    {
        float angle = gunPivot.transform.eulerAngles.z;

        Vector3 targetDir = Quaternion.Euler(0f, 0f, angle) * Vector2.right;
        RaycastHit2D hit = Physics2D.Raycast(gunEnd.transform.position, targetDir, 10f);
        lineRenderer.SetPosition(0, gunEnd.transform.position);
        if (hit.collider != null)
        {
            Debug.Log("Hit target! " + hit.collider.gameObject);
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            lineRenderer.SetPosition(1, targetDir*100f);
        }
    }
    
    

}

