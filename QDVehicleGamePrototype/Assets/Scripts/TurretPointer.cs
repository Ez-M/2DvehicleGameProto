using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPointer : MonoBehaviour
{
    public GameObject gunBarrel;
    public GameObject gunPivot;
    public bool isFunctional;
    public PlayerManager playerManager;

    public void Awake()
    {
        playerManager = PlayerManager.Instance;
    }
    // Start is called before the first frame update
    public void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        PointGun();
    }

    public void PointGun()
    {
        if(isFunctional)
        {
            Vector3 targetPos = playerManager.InputManger.mouseWorldPosition;

            Vector3 direction = (targetPos - gunPivot.transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg-90;

            gunPivot.transform.eulerAngles = new Vector3(0, 0, angle);

        }
    }
}
