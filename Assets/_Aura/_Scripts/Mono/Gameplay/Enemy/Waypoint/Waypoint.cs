using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    #region Header
    [Header("Waypoint COnfig")]
    [Space(10)]
    [Tooltip("Points in the scene that NPC/Enemy can move to")]
    [SerializeField] 
    #endregion
    private Vector3[] points;
    public Vector3[] Points { get => points; }

    //tracks current position of this NPC/Enemy
    public Vector3 EntityPosition { get; set; }

    //flag to keep track of game started condition
    private bool isGameStarted = false;

    public string OwnerName { get;private set; }
    #region Mono Callbacks
    private void Awake()
    {
        
        isGameStarted = true;
        EntityPosition = transform.position;
    }

    private void OnDrawGizmos()
    {
        if(isGameStarted == false && transform.hasChanged)
        {
            OwnerName = gameObject.name;
            EntityPosition = transform.position;
        }
    }

    public Vector3 GetPosition(int pointIndex)
    {
       return Points[pointIndex];
    }
    #endregion
}
