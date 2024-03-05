using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("Component Refs")]
    [Tooltip("Ref to Input reader Scriptable Object")]
    [SerializeField]
    private PlayerInpuReader inputReader;
    [SerializeField]
    private PlayerMovement playerMovementRef;
    [SerializeField]
    PlayerMana playerMana;

    [Header("Animation Refs")]
    [SerializeField]
    private PlayerAnimations playerAnimations;

    [Header("Weapon Config")]
    [SerializeField] WeaponSO initialWeapon;


    [Header("Shooting properties")]
    [Tooltip("How long to stay in shooting animation")]
    [SerializeField]
    private float shootTime;

    [Header("Projectile Spawn Transforms")]
    [SerializeField] private Transform upSpawnPos;
    [SerializeField] private Transform downSpawnPos;
    [SerializeField] private Transform rightSpawnPos;
    [SerializeField] private Transform leftSpawnPos;

    //reference to the selected enemy
    private EnemyBrain selectedEnemy;

    //reference to the current running attack coroutine
    private Coroutine currentAttackRoutine;



    #region Mono Callbacks

    private void OnEnable()
    {
        SelectionManager.OnEnemySelected += HandleEnemySelected;
        SelectionManager.OnNotEnemySelected += HandleEnemyNotSelected;
        inputReader.OnShootAction += HandleShootFireball;

    }
    private void OnDisable()
    {
        SelectionManager.OnEnemySelected -= HandleEnemySelected;
        SelectionManager.OnNotEnemySelected -= HandleEnemyNotSelected;
        inputReader.OnShootAction -= HandleShootFireball;

    }
    #endregion

    #region Enemy Selection Callbacks
    private void HandleEnemyNotSelected()
    {
        selectedEnemy = null;
    }

    private void HandleEnemySelected(EnemyBrain brain)
    {
        selectedEnemy = brain;
    }
    #endregion

    #region Input Callbacks
    private void HandleShootFireball()
    {

        if (selectedEnemy != null)
        {
            if (playerMana.CurrentMana < initialWeapon.requiredMana)
            {
                return;
            }
            if (currentAttackRoutine != null)
            {
                StopCoroutine(currentAttackRoutine);
            }

            StartCoroutine(AttackRoutine());
        }

    }


    #endregion

    private IEnumerator AttackRoutine()
    {
        playerAnimations.SetShootAnimation(true);
        FireProjectile();
        yield return new WaitForSeconds(shootTime);
        playerAnimations.SetShootAnimation(false);
    }

    private void FireProjectile()
    {
        bool facingRight = playerMovementRef.LastDirection.x > 0;
        bool facingLeft = playerMovementRef.LastDirection.x < 0;
        bool facingUp = playerMovementRef.LastDirection.y > 0;
        bool facingDown = playerMovementRef.LastDirection.y < 0;

        //Determine direction of facing
        if (facingRight)
        {
            SpawnProjectile(rightSpawnPos);
        }
        else if (facingLeft)
        {
            SpawnProjectile(leftSpawnPos);
        }
        else if (facingUp)
        {
            SpawnProjectile(upSpawnPos);
        }
        else if (facingDown)
        {
            SpawnProjectile(downSpawnPos);
        }



    }

    private void SpawnProjectile(Transform directionTransform)
    {
        var projectile = Instantiate(initialWeapon.projectilePrefab);
        projectile.transform.position = directionTransform.position;
        projectile.transform.rotation = directionTransform.rotation;
        projectile.FireDirection = Vector2.up;
        projectile.Damage = initialWeapon.damage;
        playerMana.UseMana(initialWeapon.requiredMana);
    }
}
