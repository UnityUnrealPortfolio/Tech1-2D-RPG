using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour,InputActions.IMovementActions
{
    [Header("Config")]
    [SerializeField] private float speed;


    #region private fields
    private Player playerRefs;
    private PlayerAnimations playerAnimations;
    private Rigidbody2D playerRb;
    private Vector2 moveDirection;
    private InputActions actions;
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        Initialize();
    }
    private void OnEnable()
    {
        actions.Enable();
        actions.Movement.SetCallbacks(this);
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void OnDisable()
    {
        actions.Disable();
    } 
    #endregion

    #region Input Callbacks

    public void OnMove(InputAction.CallbackContext context)
    {
        if(playerRefs.Stats.Health <= 0) { return; }
        moveDirection = context.ReadValue<Vector2>().normalized;
    }

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
        actions = new InputActions();
    } 
    #endregion
}
