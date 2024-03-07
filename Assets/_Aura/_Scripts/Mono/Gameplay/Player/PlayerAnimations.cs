using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class handles all animations
/// </summary>
public class PlayerAnimations : MonoBehaviour
{
    private Animator playerAnimator;
    private readonly int moveXHash = Animator.StringToHash("MoveX");
    private readonly int moveYHash = Animator.StringToHash("MoveY");
    private readonly int isMovingHash = Animator.StringToHash("IsMoving");
    private readonly int deathHash = Animator.StringToHash("Death");
    private readonly int reviveHash = Animator.StringToHash("Revive");
    private readonly int isShootingHash = Animator.StringToHash("IsShooting");
    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
    }
    public void SetBoolTransitionToMove(bool _value)
    {
        playerAnimator.SetBool(isMovingHash, _value);
    }

    public void SetMovingAnimation(Vector2 _direction) 
    {
        playerAnimator.SetFloat(moveXHash, _direction.x);
        playerAnimator.SetFloat(moveYHash, _direction.y);
    }    

    public void SetDeathAnimation()
    {

        playerAnimator.SetTrigger(deathHash);
    }

    internal void ResetPlayer(Vector2 down)
    {
        SetMovingAnimation(down);
        playerAnimator.SetTrigger(reviveHash);

    }

    internal void SetShootAnimation(bool _value)
    {
        playerAnimator.SetBool(isShootingHash, _value);
    }
}
