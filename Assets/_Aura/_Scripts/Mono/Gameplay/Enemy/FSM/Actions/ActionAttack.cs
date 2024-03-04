using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAttack : FSMAction
{
    [Tooltip("Max attack cooldown")]
    [SerializeField]
    private float coolDown;

    [Tooltip("Amount of damage to deliver pa attack")]
    [SerializeField]
    private float damage;


    private EnemyBrain enemyBrain;
    private float coolDownTimer;


    private void Awake()
    {
        enemyBrain = GetComponent<EnemyBrain>();
    }
    public override void Act()
    {
        AttackPlayer();
    }

    private void AttackPlayer()
    {
        
        coolDownTimer -= Time.deltaTime;
        if(coolDownTimer <= 0f)
        {
            //ToDo:refactor to cache the damageable component here
            IDamageable damageable = enemyBrain.Player.GetComponent<IDamageable>();
            if(damageable != null)
            {
                damageable.TakeDamage(damage);

                //reset timer
              
            }
            coolDownTimer = coolDown;
        }
    }
}
