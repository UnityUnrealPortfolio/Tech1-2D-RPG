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
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position,
            attackRange, playerLayerMask);

        bool returnValue = false;

        if (playerCollider != null)
        {
            enemyBrain.Player = playerCollider.transform;

            if (playerCollider.GetComponent<PlayerHealth>().GetHealth() <= 0)
            {
                
                returnValue = false;
            }
            else
            {
               
                returnValue = true;
            }
          
        }
        else 
        {
           
            returnValue = false;
        }

        return returnValue;  
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
