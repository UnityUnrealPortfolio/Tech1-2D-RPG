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
    

    public void ResetPlayer()
    {
        Health = MaxHealth;
        Mana = MaxMana;
    }
}
