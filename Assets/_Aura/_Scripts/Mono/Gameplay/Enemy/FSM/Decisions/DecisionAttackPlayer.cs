using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionAttackPlayer : FSMDecision
{
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask playerLayerMask;
    EnemyBrain enemyBrain;


    private void Awake()
    {
        enemyBrain = GetComponent<EnemyBrain>();
    }
    public override bool Decide()
    {
        return InAttackRange();
    }

    private bool InAttackRange()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(enemyBrain.transform.position,
            attackRange, playerLayerMask);
        if (playerCollider != null)
        {
            enemyBrain.Player = playerCollider.transform;
            return true;
        }
        else
        {
            enemyBrain.Player = null;
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
