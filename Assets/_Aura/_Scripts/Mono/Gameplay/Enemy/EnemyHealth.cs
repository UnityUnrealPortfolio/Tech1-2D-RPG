using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour,IDamageable
{


    [Header("Health Props")]
    [SerializeField]
    private float MaxHealth;

    [Header("Graphics")]
    [SerializeField]
    private GameObject healthGraphic;


    private Animator enemyAnimator;
    private Selector selector;
    private EnemyHealthBarCB healthBarCB;
    private float currentHealth;
    private EnemyBrain brain;
    private EnemyBrain selectedBrain;
    private int deathHash = Animator.StringToHash("dead");

    public event Action OnHealthZero;
    public float CurrentHealth
    {
        get
        {
            return currentHealth;
        }
        private set
        {
            currentHealth = value;
            if(currentHealth <= 0)
            {
                HandleDeath();
                currentHealth = 0;
            }
      
            healthBarCB.SetHealth(currentHealth/MaxHealth);
        }
    }
    private void Awake()
    {
        brain = GetComponent<EnemyBrain>();
        selector = GetComponent<Selector>();
        enemyAnimator = GetComponent<Animator>();
        healthBarCB = healthGraphic.GetComponent<EnemyHealthBarCB>();   
    }
    private void OnEnable()
    {
        SelectionManager.OnEnemySelected += HandleSelection;
        SelectionManager.OnNotEnemySelected += HandleDeselection;
    }

    private void OnDisable()
    {
        SelectionManager.OnEnemySelected -= HandleSelection;
        SelectionManager.OnNotEnemySelected -= HandleDeselection;
    }

    private void Start()
    {   
        healthGraphic.SetActive(false);
        CurrentHealth = MaxHealth;
    }
    public void TakeDamage(float _damage)
    {
        //ToDo:may have to refactor so only take damage when selected
        CurrentHealth-=_damage;
    }
    public void HandleSelection(EnemyBrain enemyBrain)
    {
        selectedBrain = enemyBrain;
        if(selectedBrain == brain)
        {

            healthGraphic.SetActive(true);
        }
        else
        {
            healthGraphic.SetActive(false);
        }
    }
    private void HandleDeselection()
    {
        healthGraphic.SetActive(false);
    }
    internal void HandleDeath()
    {
        //turn off health animation (or show skull ;-) )
        healthGraphic.SetActive(false);
        //play death animation
        enemyAnimator.SetTrigger(deathHash);
        selector.DisableSelectionGraphic();
        //change layer to avoid interactions
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        //but allow loot interactions
        brain.enabled = false;
    }
}
