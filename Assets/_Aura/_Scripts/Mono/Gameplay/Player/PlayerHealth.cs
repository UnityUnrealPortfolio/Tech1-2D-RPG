using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [Header("Health Stats")]
    [SerializeField] private PlayerStatsSO playerStats;
    private PlayerAnimations playerAnimations;

    private void Awake()
    {
        playerAnimations = GetComponent<PlayerAnimations>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))//ToDo:Here for testing only
        {
            TakeDamage(2f);
        }
    }
    public void TakeDamage(float _amount)
    {
      playerStats.Health -= _amount;
        if (playerStats.Health <= 0 )
        {
            playerStats.Health = 0;
            HandlePlayerDeath();
        }
    }
    private void HandlePlayerDeath()
    {
        playerAnimations.SetDeathAnimation();
    }
}
