using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[Serializable]
public class Health : MonoBehaviour
{
    private int baseMaxHealth;
    public int BaseMaxhealth{get => baseMaxHealth;}

    private int maxHealth;
    public int Maxhealth{get => maxHealth;}

    private int currentHealth;

    public int CurrentHealth{ get => currentHealth;}


    public int SetBaseHealth(int _setTo)
    {
        if(_setTo > 0)
        {baseMaxHealth += _setTo;}
        return baseMaxHealth;
    }

    public void ResetToBase()
    {   // may require a reworked function for calculating effective health vs raw health due to stats and gear
        maxHealth = baseMaxHealth;
        currentHealth = maxHealth;
    }

    #region  maxHealth
    public int SetMaxHealth(int _setTo)
    {
        maxHealth = _setTo;
        if (currentHealth > maxHealth)
        {currentHealth = maxHealth;}
        return maxHealth;
    }
    public int DamageMaxHealth(int _damage)
    {
        if(_damage > 0)
        {maxHealth -= _damage;}
        if (currentHealth > maxHealth)
        {currentHealth = maxHealth;}
        return maxHealth;
    }
    public int HealMaxHealth(int _heal)
    {
        if(_heal > 0)
        {maxHealth += _heal;}
        return maxHealth;
    }
    #endregion

    #region currentHealth
    public int SetCurrentHealth(int _setTo)
    {
        currentHealth = _setTo;
        return currentHealth;
    }
    public int DamageHealth(int _damage)
    {
        if(_damage > 0)
        {currentHealth -= _damage;}
        return currentHealth;
    }
    public int HealHealth(int _heal)
    {
        if(_heal > 0)
        {currentHealth += _heal;}
        return currentHealth;
    }
    #endregion
    

}