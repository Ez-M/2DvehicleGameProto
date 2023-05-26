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
    public float weaponRange;

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
        float angle = gunPivot.transform.rotation.eulerAngles.z;
        Quaternion rotationAngle = Quaternion.Euler(0f, 0f, angle);

        Vector2 targetDir = gunPivot.transform.right.normalized;
        RaycastHit2D hit = Physics2D.Raycast(gunEnd.transform.position, targetDir, weaponRange);
        lineRenderer.SetPosition(0, gunPivot.transform.position);
        if (hit.collider != null)
        {
            Debug.Log("Hit target! " + hit.collider.gameObject);
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            lineRenderer.SetPosition(1, (Vector2)gunEnd.transform.position + targetDir * weaponRange);
        }
    }
    
    

}

