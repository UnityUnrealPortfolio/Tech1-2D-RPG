using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLoot : MonoBehaviour
{
    [Header("Loot Config")]
    [Tooltip("Amount of EXP dropped on death")]
    [SerializeField] private float expDrop;
    public float ExpDrop => expDrop;
}
