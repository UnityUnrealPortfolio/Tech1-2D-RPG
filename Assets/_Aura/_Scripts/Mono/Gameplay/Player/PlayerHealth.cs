using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [Header("Health Stats")]
    [SerializeField] private PlayerStatsSO playerStats;
    private PlayerAnimations playerAnimations;

    [Header("Damage UI visual")]
    [SerializeField] private Transform damageTextPrefab;
    [Tooltip("Position offset to spawn damageText Prefab")]
    [SerializeField] private float xOffset;
    [SerializeField] private float yOffset;
    private void Awake()
    {
        playerAnimations = GetComponent<PlayerAnimations>();
    }
    private void OnEnable()
    {
        playerStats.OnHealthZero += HandleZeroHealth; 
    }


    private void HandleZeroHealth()
    {
        Debug.Log("zero health");
        HandlePlayerDeath();
    }

    public float GetHealth() => playerStats.Health;

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.P))//ToDo:Here for testing only
        //{
        //    TakeDamage(2f);
        //}
    }
    public void TakeDamage(float _amount)
    {
        playerStats.HealthProp -= _amount;
        //spawn damage text etc
        SpawnDamageText(_amount);
    }

    private void SpawnDamageText(float _amount)
    {
        if (playerStats.HealthProp <= 0) return;
        var damageText = Instantiate(damageTextPrefab);
        damageText.SetParent(transform);
        damageText.transform.position = new Vector3(transform.position.x + xOffset, transform.position.y + yOffset, transform.position.z);
        damageText.GetComponent<DamageText>().ShowDamageText(_amount);
    }

    private void HandlePlayerDeath()
    {
        playerAnimations.SetDeathAnimation();
      
    }
}
