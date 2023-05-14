using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPointer : MonoBehaviour
{
    public GameObject gunBarrel;
    public GameObject gunPivot;
    public GameObject gunEnd;
    public bool isFunctional;
    public PlayerManager playerManager;
    public LineRenderer lineRenderer;

    public void Awake()
    {
        playerManager = PlayerManager.Instance;
    }
    // Start is called before the first frame update
    public void Start()
    {
        playerManager.InputManger.OnShoot += FireGun;
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
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg-90; // removing 90 because my current sprite isn't offset

            gunPivot.transform.eulerAngles = new Vector3(0, 0, angle);

        }
    }

    public void FireGun()
    {
        float angle = gunPivot.transform.eulerAngles.z+90; // adding 90 because my current sprite isn't offset

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
