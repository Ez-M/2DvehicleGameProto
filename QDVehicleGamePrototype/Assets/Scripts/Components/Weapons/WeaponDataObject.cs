using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New WeaponDataObject", menuName = "Vehicle Components/WeaponObject")]
public class WeaponDataObject : ComponentObject
{

    public int baseDamage;
    public float fireRate;
    public float projectileSpeed;
    public GameObject projectilePrefab;

}

public enum AttackType
{
    Projectile,
    Firearm,
    Explosive,
    Melee,
    Thrown,
    Energy
}

public class WeaponData 
{

}
