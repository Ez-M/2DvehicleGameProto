using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New WeaponDataObject", menuName = "Vehicle Components/WeaponObject")]
public class WeaponDataObject : ComponentObject
{
    public int WeaponDataObjectID;
    public GameObject projectilePrefab;
    public int baseDamage;
    public float baseFireRate;
    //set to - if none
    public float baseProjectileSpeed;
    public float baseAttackRange;
    public bool isRapidFire;

    public AttackType attackType;

}

public enum AttackType
{
    hitScan,
    projectile,
    area,
    target
}

[Serializable]
public class WeaponData 
{
    public int sourceObjectID;
    public int baseDamage;
    public float baseFireRate;
    public float baseProjectileSpeed;
    public float baseAttackRange;
    public bool isRapidFire;

    public AttackType attackType;

    public WeaponData(WeaponDataObject sourceObject)
    {
        sourceObjectID = sourceObject.WeaponDataObjectID;
        baseDamage = sourceObject.baseDamage;
        baseFireRate = sourceObject.baseFireRate;
        baseProjectileSpeed = sourceObject.baseProjectileSpeed;
        baseAttackRange = sourceObject.baseAttackRange;
        isRapidFire = sourceObject.isRapidFire;

    }

}
