using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Input Ref")]
    [SerializeField]
    private PlayerInpuReader inputReader;

    [Header("Config")]
    [SerializeField] private float speed;


    #region private fields
    private Player playerRefs;
    private PlayerAnimations playerAnimations;
    private Rigidbody2D playerRb;
    private Vector2 moveDirection;
    public Vector2 LastDirection{get;private set;}
    //private InputActions actions;
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        Initialize();
    }
    private void OnEnable()
    {    
        inputReader.OnMoveAction += HandleOnMove;
    }


    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void OnDisable()
    {
        inputReader.OnMoveAction -= HandleOnMove;
    } 
    #endregion

    #region Input Callbacks
    private void HandleOnMove(Vector2 _value)
    {
       
        if (playerRefs.Stats.Health <= 0) 
        {
            return; 
        }
        moveDirection = _value;
        if(moveDirection != Vector2.zero)
        {
            LastDirection = moveDirection;
        }
    }

    #endregion

    #region Event Callbacks

    #endregion

    #region Utitily
    private void MovePlayer()
    {
        playerRb.MovePosition(playerRb.position + moveDirection * speed * Time.fixedDeltaTime);

        if (moveDirection == Vector2.zero) 
        {
            playerAnimations.SetBoolTransitionToMove(false);
            return;
        }

        //update animation parameters
        playerAnimations.SetBoolTransitionToMove(true);
        playerAnimations.SetMovingAnimation(moveDirection);

       
    }
    private void Initialize()
    {
        playerRefs = GetComponent<Player>();
        playerAnimations = GetComponent<PlayerAnimations>();
        playerRb = GetComponent<Rigidbody2D>();
        
    }

 
    #endregion
}
