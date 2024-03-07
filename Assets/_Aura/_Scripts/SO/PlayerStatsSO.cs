using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerStats",menuName ="Player Stats")]
public class PlayerStatsSO : ScriptableObject
{
    [Header("Level in Game")]
    [Space(10)]
    public int Level;
    [Header("Health")]
    public float Health; //ToDo:may refactor this to properties and broadcast death event to listeners
    public float MaxHealth;
    
    public float HealthProp
    {
        get
        {
            return Health;
        }
        set
        {
            Health = value;
            OnDamageTaken?.Invoke();
            if(Health <= 0)
            {
                //trigger health zero event
                OnHealthZero?.Invoke();
                Health = 0;
            }
        }
    }

    [Space(10)]
    [Header("Mana")]
    public float Mana;
    public float MaxMana;
   


    [Header("Attack")]
    public float BaseDamage;
    [Tooltip("Likelihood of inflicting Critical damage")]public float CriticalChance;
    [Tooltip("Percentage to add on top of base damage")]public float CriticalDamage;


    [Space(10)]
    [Header("Exp")]
    public float CurrentExp;
    public float NextLevelExp;
    public float InitialNextLevelExp;//exp needed to reach level 2
    [Tooltip("Percentage over and above current exp needed to go to NextLevelExp")]
    [Range(1f, 100f)] public float ExpMultiplier;


    [Space(10)]
    [Header("Attributes")]
    public int strength;
    public int dexterity;
    public int intelligence;
    public int attributePoints;

  

    //stat totals
    public float TotalExp { get; set; } 
    public float TotalDamage { get; set; }  

    public event Action OnHealthZero;
    public event Action OnDamageTaken;
    public event Action OnPlayerReset;
    public void ResetPlayer()
    {
        //OnPlayerReset?.Invoke();
        Health = MaxHealth;
        Mana = MaxMana;
        CurrentExp = 0;
        NextLevelExp = InitialNextLevelExp;
        Level = 1;
        TotalExp = 0;
        BaseDamage = 2;
        CriticalDamage = 50;
        strength = 1;
        dexterity = 1;
        intelligence = 1;
        attributePoints = 0;
    }
}

public enum AttributeType
{
    Strength,
    Dexterity,
    Intelligence
}
