using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionDetectPlayer : FSMDecision
{
    [SerializeField] private float detectRange;
    [SerializeField] private LayerMask playerLayerMask;
    EnemyBrain enemyBrain;


    private void Awake()
    {
        enemyBrain = GetComponent<EnemyBrain>();
    }
    public override bool Decide()
    {
        return DetectPlayer();
    }

    private bool DetectPlayer()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position,
            detectRange, playerLayerMask);

        if (playerCollider != null)
        {
            
            enemyBrain.Player = playerCollider.transform;

            if (playerCollider.GetComponent<PlayerHealth>().GetHealth() <= 0)
            {
                return  false;
            }
            else
            {
               return true;
            }
        }
        else
        {
            enemyBrain.Player = null;
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,detectRange);
    }
}
