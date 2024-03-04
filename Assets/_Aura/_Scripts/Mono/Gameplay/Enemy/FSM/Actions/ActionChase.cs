using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionChase : FSMAction
{
    [Header("Chase Config")]
    [Tooltip("How fast enemy should move towards player")]
    [SerializeField] private float chaseSpeed;

    [Tooltip("how close enemy should get to player before starting to attack")]
    [Range(0.2f,3f)]
    [SerializeField]private float minDistance;

    private EnemyBrain enemyBrain;

    private void Awake()
    {
        enemyBrain = GetComponent<EnemyBrain>();
    }
    public override void Act()
    {
        //chase player up to min distance
        //what's the vector to the player
        if (enemyBrain.Player == null) return;
        Vector3 directionToPlayer = enemyBrain.Player.position - transform.position;
        
        if (directionToPlayer.magnitude >= minDistance)
        {
            transform.Translate(directionToPlayer.normalized * (chaseSpeed * Time.deltaTime));
        }
        
    }
}
