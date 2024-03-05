using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Weapon_")]
public class WeaponSO : ScriptableObject
{
    [Header("Weapon config")]
    public Sprite icon;
    public WeaponType weaponType;
    public float damage;

    [Header("Projectile config")]
    public Projectile projectilePrefab;
    public float requiredMana;
}

public enum WeaponType
{
    MAGIC,
    MELEE
}
