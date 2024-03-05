using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName ="InputReader",menuName ="Input/InputReader")]
public class InputReaderSO : ScriptableObject, InputActions.IMovementActions
{
    public event Action<Vector2> OnMoveAction;
    public event Action OnShootAction;

    private InputActions actions;
    private void OnEnable()
    {
       
        actions = new InputActions();
        actions.Enable();
        actions.Movement.SetCallbacks(this);
    }
 
    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("Reading Move");
       OnMoveAction?.Invoke(context.ReadValue<Vector2>());  
    }

    public void OnShootFireball(InputAction.CallbackContext context)
    {
     if(context.performed) { OnShootAction?.Invoke(); }
    }
}
