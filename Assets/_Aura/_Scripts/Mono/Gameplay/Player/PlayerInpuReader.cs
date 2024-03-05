using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInpuReader : MonoBehaviour,InputActions.IMovementActions
{
    public event Action<Vector2> OnMoveAction;
    public event Action OnShootAction;
    public event Action<Vector2> OnLastMoveAction;

    private InputActions actions;
    private void OnEnable()
    {

        actions = new InputActions();
        actions.Enable();
        actions.Movement.SetCallbacks(this);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
       
           OnMoveAction?.Invoke(context.ReadValue<Vector2>());
        
        
    }

    public void OnShootFireball(InputAction.CallbackContext context)
    {
        if (context.performed) { OnShootAction?.Invoke(); }
    }
}
