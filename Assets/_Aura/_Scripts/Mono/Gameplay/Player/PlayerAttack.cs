using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;
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
    [SerializeField]
    PlayerStatsSO playerStats;

    [Header("Animation Refs")]
    [SerializeField]
    private PlayerAnimations playerAnimations;

    [Header("Weapon Config")]
    [SerializeField] WeaponSO initialWeapon;

    [Header("Melee Config")]
    [SerializeField] private ParticleSystem slashFX;
    [SerializeField] private float minDistanceMeleeAttack;

    [Header("Shooting properties")]
    [Tooltip("How long to stay in shooting animation")]
    [SerializeField]
    private float shootTime;

    [Header("Projectile Spawn Transforms")]
    [SerializeField] private Transform upSpawnPos;
    [SerializeField] private Transform downSpawnPos;
    [SerializeField] private Transform rightSpawnPos;
    [SerializeField] private Transform leftSpawnPos;

    public WeaponSO CurrentWeapon { get; private set; }

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

    private void Start()
    {
        CurrentWeapon = initialWeapon;
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
            switch (CurrentWeapon.weaponType)
            {
                case WeaponType.MAGIC:
                    MagicAttack();
                    break;
                case WeaponType.MELEE:
                    MeleeAttack();
                    break;
            }


        }

    }

    #endregion

    public float GetAttackDamage()
    {
        float damage = playerStats.BaseDamage;
        damage += CurrentWeapon.damage;
        float randomPercentage = Random.Range(0f, 100);
        if(randomPercentage <= playerStats.CriticalChance)
        {
            damage +=  damage * (playerStats.CriticalDamage/100f);
        }
        return damage;
    }
    #region Melee Attack Utility
    private void MeleeAttack()
    {
        if (currentAttackRoutine != null)
        {
            StopCoroutine(currentAttackRoutine);
        }
        StartCoroutine(MeleeAttackRoutine());
    }
    private IEnumerator MeleeAttackRoutine()
    {
        //show slash fx
        SetShowSlashFX();
        //check distance to enemy
        var currentDistToEnemy = Vector3.Distance(transform.position, selectedEnemy.transform.position);
        if (currentDistToEnemy < minDistanceMeleeAttack)
        {
            selectedEnemy.GetComponent<IDamageable>().TakeDamage(GetAttackDamage());
        }
        //damage enemy if current dist is < min dist to enemy prop
        yield return new WaitForSeconds(shootTime);
    }

    private void SetShowSlashFX()
    {
        if (slashFX != null)
        {
            bool facingRight = playerMovementRef.LastDirection.x > 0;
            bool facingLeft = playerMovementRef.LastDirection.x < 0;
            bool facingUp = playerMovementRef.LastDirection.y > 0;
            bool facingDown = playerMovementRef.LastDirection.y < 0;

            //Determine direction of facing
            if (facingRight)
            {
                SpawnMeleeFX(rightSpawnPos);
            }
            else if (facingLeft)
            {
                SpawnMeleeFX(leftSpawnPos);
            }
            else if (facingUp)
            {
                SpawnMeleeFX(upSpawnPos);
            }
            else if (facingDown)
            {
                SpawnMeleeFX(downSpawnPos);
            }
        }
    }

    private void SpawnMeleeFX(Transform _spawnPos)
    {
        slashFX.transform.position = _spawnPos.position;
        slashFX.Play();
    }

    #endregion

    #region Projectile Firing Utility

    private void MagicAttack()
    {
        if (playerMana.CurrentMana < CurrentWeapon.requiredMana)
        {
            return;
        }
        if (currentAttackRoutine != null)
        {
            StopCoroutine(currentAttackRoutine);
        }

        StartCoroutine(MagicAttackRoutine());
    }
    private IEnumerator MagicAttackRoutine()
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
        var projectile = Instantiate(CurrentWeapon.projectilePrefab);
        projectile.transform.position = directionTransform.position;
        projectile.transform.rotation = directionTransform.rotation;
        projectile.FireDirection = Vector2.up;
        projectile.Damage = GetAttackDamage();
        playerMana.UseMana(CurrentWeapon.requiredMana);
    }
    #endregion
}
