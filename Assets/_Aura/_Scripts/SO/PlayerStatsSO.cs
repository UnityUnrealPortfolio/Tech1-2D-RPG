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
    public float MaxHealth;
    public float Health; //ToDo:may refactor this to properties and broadcast death event to listeners

    [Space(10)]
    [Header("Mana")]
    public float MaxMana;
    public float Mana;

    [Space(10)]
    [Header("Exp")]
    public float CurrentExp;
    public float NextLevelExp;
    public float InitialNextLevelExp;//exp needed to reach level 2

    [Tooltip("Percentage over and above current exp needed to go to NextLevelExp")]
    [Range(1f, 100f)] public float ExpMultiplier;
    

    public void ResetPlayer()
    {
        Health = MaxHealth;
        Mana = MaxMana;
        CurrentExp = 0;
        NextLevelExp = InitialNextLevelExp;
        Level = 1;
    }
}
