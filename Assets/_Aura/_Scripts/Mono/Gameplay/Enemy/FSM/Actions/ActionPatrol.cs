using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPatrol : FSMAction
{
    [Header("Patrol Configs")]
    [Space(10)]
    [Tooltip("How fast the NPC/Enemy moves from point to point")]
    [SerializeField] private float moveSpeed;

    [Tooltip("How close to get to a waypoint before moving on to next waypoint")]
    [SerializeField] private float minDistToTarget;

    //reference to Waypoint component on NPC/Enemy
    private Waypoint waypoint;

    //current waypoint position we are moving towards
    private int pointIndex;
    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
    }
    public override void Act()
    {
        //move towards next target
        transform.position = Vector3.MoveTowards(transform.position, waypoint.EntityPosition +
            GetNextPos(), moveSpeed * Time.deltaTime);
        
        //are we close to the target?if so, update point index
        float distToTarget = Vector3.Distance(transform.position, waypoint.EntityPosition+GetNextPos());
        if(distToTarget < minDistToTarget)
        {
            UpdatePointIndex();
        }
    }
    private void UpdatePointIndex()
    {
        pointIndex++;
        if(pointIndex > waypoint.Points.Length-1 )
        {
            pointIndex = 0;
        }
    }
    private Vector3 GetNextPos()
    {
       return waypoint.GetPosition(pointIndex);
    }
}
